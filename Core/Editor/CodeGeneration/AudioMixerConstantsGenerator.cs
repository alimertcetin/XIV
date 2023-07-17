using System;
using UnityEditor;
using UnityEngine.Audio;
using XIV.Core.Utils;

namespace XIV.XIVEditor.CodeGeneration
{
    public static class AudioMixerConstantsGenerator
    {
        const string CLASS_NAME = "AudioMixerConstants";

        public static string GetClassString()
        {
            var guids = AssetDatabase.FindAssets("t: AudioMixer", new[] { "Assets" });
            
            ClassGenerator generator = new ClassGenerator(CLASS_NAME, classModifier: "static");
            generator.Use(nameof(UnityEngine));

            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var audioMixer = AssetDatabase.LoadAssetAtPath<AudioMixer>(path);
                
                // https://forum.unity.com/threads/get-list-of-snapshots-and-groups-from-an-audio-mixer.454656/
                Array parameters = (Array)audioMixer.GetType().GetProperty("exposedParameters").GetValue(audioMixer, null);
                
                int paramatersLength = parameters.Length;
                if (paramatersLength == 0) continue;

                var audioMixerClassName = audioMixer.name.Replace(" ", "")
                    .Replace("-", "_")
                    .Replace("/", "_")
                    .Replace("(","")
                    .Replace(")","");
                
                ClassGenerator innerClass = new ClassGenerator(audioMixerClassName, classModifier: "static", isInnerClass: true);
                
                string paramaterNames = "";
                for (int j = 0; j < paramatersLength; j++)
                {
                    var o = parameters.GetValue(j);
                    
                    var propertyType = o.GetType();
                    var propertyName = (string)propertyType.GetField("name").GetValue(o);
                    var fieldValue = FormatStringFieldValue(propertyName);
                    var fieldName = propertyName;

                    innerClass.AddField(fieldName, fieldValue, "string", "const");
                    paramaterNames += "\t" + fieldName + "," + Environment.NewLine;
                }

                paramaterNames = Environment.NewLine + "{" + Environment.NewLine + paramaterNames + Environment.NewLine + "}";
                innerClass.AddField("exposedParameters", paramaterNames, "string[]", "static");
                
                generator.AddInnerClass(innerClass);
            }

            return generator.EndClass();
        }

        static string FormatStringFieldValue(string value) => $"\"{value}\"";
    }
}