using UnityEditor;
using XIV.Core;
using XIV.XIVEditor.Utils;
using Object = UnityEngine.Object;

namespace XIV.XIVEditor
{
    [CustomEditor(typeof(Object), true, isFallback = true), CanEditMultipleObjects]
    public class XIVDefaulEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorUtils.DrawMethods(target, typeof(ButtonAttribute));
        }
    }
}