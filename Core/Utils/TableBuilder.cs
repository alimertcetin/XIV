using System;
using System.Collections.Generic;
using XIV.Core.XIVMath;

namespace XIV.Core.Utils
{
    public delegate string TableFormatter(string[] headers, IReadOnlyList<Dictionary<string, string>> rows);


    public class TableBuilder
    {
        private string[] _headers = Array.Empty<string>();
        private readonly List<Dictionary<string, string>> _rows = new();
        private readonly List<int> _columnWidths = new();

        public void SetColumns(params string[] headers)
        {
            _headers = headers;
            _columnWidths.Clear();
            foreach (var header in headers)
            {
                _columnWidths.Add(header?.Length ?? 0);
            }
        }

        public void AddRow(params string[] row)
        {
            var entry = new Dictionary<string, string>();

            for (int i = 0; i < _headers.Length; i++)
            {
                string key = _headers[i];
                string value = i < row.Length ? row[i] ?? string.Empty : string.Empty;
                entry[key] = value;
                if (value.Length > _columnWidths[i])
                {
                    _columnWidths[i] = value.Length;
                }
            }

            // Handle overflow if more values than headers
            if (row.Length > _headers.Length)
            {
                string lastKey = _headers[^1];
                entry[lastKey] = "...";
                _columnWidths[^1] = XIVMathInt.Max(_columnWidths[^1], 3);
            }

            _rows.Add(entry);
        }

        public string Build(TableFormatter formatter)
        {
            var allRows = new List<string[]>
            {
                _headers
            };

            foreach (var rowDict in _rows)
            {
                var row = new string[_headers.Length];
                for (int i = 0; i < _headers.Length; i++)
                {
                    string key = _headers[i];
                    row[i] = rowDict.TryGetValue(key, out var val) ? val : string.Empty;
                }
                allRows.Add(row);
            }

            return formatter(_headers, _rows);
        }
    }
}
