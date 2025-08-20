using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XIV.Core.Extensions;

namespace XIV.Core.Utils
{
    // TODO : Cleanup
    public interface IJsonSerializer
    {
        string Serialize<T>(T obj, JsonOptions jsonOptions);
        T Deserialize<T>(string json);
    }

    public struct JsonOptions
    {
        public bool Indented;
    }
    
#if UNITY_5_3_OR_NEWER
    // public class NewtonsoftJsonSerializer : IJsonSerializer
    // {
    //     public string Serialize<T>(T obj, JsonOptions jsonOptions)
    //     {
    //         return Newtonsoft.Json.JsonConvert.SerializeObject(obj, jsonOptions.Indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
    //     }
    //
    //     public T Deserialize<T>(string json)
    //     {
    //         return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    //     }
    // }
#endif

#if !UNITY_5_3_OR_NEWER
public class SystemTextJsonSerializer : IJsonSerializer
{
    public string Serialize<T>(T obj, JsonOptions jsonOptions)
    {
        var opt = new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = jsonOptions.Indented
        };
        return System.Text.Json.JsonSerializer.Serialize(obj, opt);
    }

    public T Deserialize<T>(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
}
#endif

    
    public static class TableFormatters
    {
        private static int[] ComputeColumnWidths(string[] headers, IReadOnlyList<Dictionary<string, string>> rows)
        {
            int[] columnWidths = headers.Select(h => h?.Length ?? 0).ToArray();

            foreach (var row in rows)
            {
                for (int i = 0; i < headers.Length; i++)
                {
                    string key = headers[i];
                    string value = row.TryGetValue(key, out var val) ? val ?? "" : "";
                    columnWidths[i] = Math.Max(columnWidths[i], value.Length);
                }
            }

            return columnWidths;
        }

        public static string Plain(string[] headers, IReadOnlyList<Dictionary<string, string>> rows)
        {
            var sb = new StringBuilder();
            int[] widths = ComputeColumnWidths(headers, rows);

            // Header row
            for (int i = 0; i < headers.Length; i++)
            {
                sb.Append(headers[i].PadRight(widths[i]));
                if (i < headers.Length - 1) sb.Append(" | ");
            }
            sb.AppendLine();

            // Separator
            for (int i = 0; i < headers.Length; i++)
            {
                sb.Append(new string('-', widths[i]));
                if (i < headers.Length - 1) sb.Append("-+-");
            }
            sb.AppendLine();

            // Data rows
            foreach (var row in rows)
            {
                for (int i = 0; i < headers.Length; i++)
                {
                    string val = row.TryGetValue(headers[i], out var v) ? v ?? "" : "";
                    sb.Append(val.TruncateWithDots(widths[i]).PadRight(widths[i]));
                    if (i < headers.Length - 1) sb.Append(" | ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string MarkDown(string[] headers, IReadOnlyList<Dictionary<string, string>> rows)
        {
            var sb = new StringBuilder();
            int[] widths = ComputeColumnWidths(headers, rows);

            // Header row
            sb.Append("|");
            for (int i = 0; i < headers.Length; i++)
            {
                sb.Append(" " + headers[i].PadCenter(widths[i]) + " |");
            }
            sb.AppendLine();

            // Separator
            sb.Append("|");
            for (int i = 0; i < headers.Length; i++)
            {
                sb.Append(":" + new string('-', Math.Max(1, widths[i] - 2)) + ":|");
            }
            sb.AppendLine();

            // Rows
            foreach (var row in rows)
            {
                sb.Append("|");
                for (int i = 0; i < headers.Length; i++)
                {
                    string val = row.TryGetValue(headers[i], out var v) ? v ?? "" : "";
                    sb.Append(" " + val.TruncateWithDots(widths[i]).PadCenter(widths[i]) + " |");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string CSV(string[] headers, IReadOnlyList<Dictionary<string, string>> rows)
        {
            var sb = new StringBuilder();

            // Header row
            var escapedHeaders = headers.Select(h => "\"" + (h ?? "").Replace("\"", "\"\"") + "\"");
            sb.AppendLine(string.Join(",", escapedHeaders));

            // Data rows
            foreach (var row in rows)
            {
                var escaped = headers.Select(h => "\"" + (row.TryGetValue(h, out var v) ? (v ?? "").Replace("\"", "\"\"") : "") + "\"");
                sb.AppendLine(string.Join(",", escaped));
            }

            return sb.ToString();
        }

        public static string JSON(string[] headers, IReadOnlyList<Dictionary<string, string>> rows, IJsonSerializer serializer)
        {
            var jsonObjects = new List<Dictionary<string, string>>();

            foreach (var row in rows)
            {
                var obj = new Dictionary<string, string>();
                foreach (var header in headers)
                {
                    obj[header] = row.TryGetValue(header, out var v) ? v ?? "" : "";
                }
                jsonObjects.Add(obj);
            }

            return serializer.Serialize(jsonObjects, new JsonOptions { Indented = true });
        }

        public static string XML(string[] headers, IReadOnlyList<Dictionary<string, string>> rows)
        {
            var root = new XElement("Table");

            foreach (var row in rows)
            {
                var rowElem = new XElement("Row");
                foreach (var header in headers)
                {
                    rowElem.Add(new XElement(header, row.TryGetValue(header, out var v) ? v ?? "" : ""));
                }
                root.Add(rowElem);
            }

            return root.ToString();
        }

        public static string HTML(string[] headers, IReadOnlyList<Dictionary<string, string>> rows)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table border=\"1\">");

            // Header
            sb.AppendLine("  <thead><tr>");
            foreach (var h in headers)
            {
                sb.AppendLine($"    <th>{System.Net.WebUtility.HtmlEncode(h)}</th>");
            }
            sb.AppendLine("  </tr></thead>");

            // Rows
            sb.AppendLine("  <tbody>");
            foreach (var row in rows)
            {
                sb.AppendLine("    <tr>");
                foreach (var h in headers)
                {
                    sb.AppendLine($"      <td>{System.Net.WebUtility.HtmlEncode(row.TryGetValue(h, out var v) ? v ?? "" : "")}</td>");
                }
                sb.AppendLine("    </tr>");
            }
            sb.AppendLine("  </tbody>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }
    }
}
