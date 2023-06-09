using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XIV.XIVEditor.Utils
{
    public static class AssetUtils
    {
        /// <summary>Load asset files via their base class</summary>
        /// <typeparam name="TAsset">Asset Type</typeparam>
        /// <returns>Dictionary that contains the types of assets as key and value as the list of objects</returns>
        public static Dictionary<Type, List<TAsset>> LoadAssetsByBaseClass<TAsset>(string folderPath, SearchOption searchOption = SearchOption.AllDirectories) 
            where TAsset : Object
        {
            Dictionary<Type, List<TAsset>> typeValuePair = new Dictionary<Type, List<TAsset>>();
            string[] assetPaths = Directory.GetFiles(folderPath, "*", searchOption);
            
            for (int i = 0; i < assetPaths.Length; i++)
            {
                TAsset asset = AssetDatabase.LoadAssetAtPath<TAsset>(assetPaths[i]);
                if (asset == null) continue;
                            
                var assetType = asset.GetType();
                if (typeValuePair.ContainsKey(assetType))
                {
                    typeValuePair[assetType].Add(asset);
                }
                else
                {
                    List<TAsset> list = new List<TAsset>();
                    list.Add(asset);
                    typeValuePair.Add(assetType, list);
                }
            }

            return typeValuePair;
        }
        
        /// <summary>Load asset files via their base class</summary>
        /// <typeparam name="TAsset">Asset Type</typeparam>
        /// <returns>List that contains the assets</returns>
        public static List<TAsset> LoadAssetsOfType<TAsset>(string folderPath, SearchOption searchOption = SearchOption.AllDirectories) where TAsset : Object
        {
            string[] assetPaths = Directory.GetFiles(folderPath, "*", searchOption);
            
            List<TAsset> list = new List<TAsset>();
            for (int i = 0; i < assetPaths.Length; i++)
            {
                TAsset asset = AssetDatabase.LoadAssetAtPath<TAsset>(assetPaths[i]);
                if (asset == null) continue;
                list.Add(asset);
            }

            return list;
        }
        
        public static T GetScriptableObject<T>(string scriptableObjectName) where T : ScriptableObject
        {
            List<T> scriptableObjects = AssetUtils.LoadAssetsOfType<T>("Assets/ScriptableObjects");
            scriptableObjectName = scriptableObjectName.ToLower();
            foreach (T scriptableObject in scriptableObjects)
            {
                if (scriptableObject.name.ToLower() == scriptableObjectName)
                {
                    return scriptableObject;
                }
            }

            return null;
        }
        

        public static GameObject FindPrefab(string prefabName)
        {
            string[] guids = AssetDatabase.FindAssets("t:Prefab");
            
            prefabName = prefabName.ToLower();
            for (var i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (asset == null || asset.name.ToLower() != prefabName) continue;
                
                return asset;
            }

            return null;
        }
        
        public static void OpenInspectorForAsset(Object asset)
        {
            Type inspectorType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            EditorWindow inspectorWindow = (EditorWindow)ScriptableObject.CreateInstance(inspectorType);
            MethodInfo targetMethod = inspectorType.GetMethod("SetObjectsLocked", BindingFlags.NonPublic | BindingFlags.Instance);
            targetMethod.Invoke(inspectorWindow, new object[] { new List<Object>() { asset } });
            inspectorWindow.Show();
        }

        public static void SelectAsset(Object asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, asset.GetType());
        }
        
    }
}