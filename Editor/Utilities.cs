using System.Reflection;
using UnityEditor;
using UnityEditorInternal;

namespace TNRD.CustomDrawers
{
    internal static class Utilities
    {
        private static readonly FieldInfo reorderableListDisplayHeaderFieldInfo =
            typeof(ReorderableList).GetField("m_DisplayHeader", BindingFlags.Instance | BindingFlags.NonPublic);

        internal static float GetDefaultHeight(bool hasLabel, bool compact)
        {
            return compact || !hasLabel
                ? EditorGUIUtility.singleLineHeight
                : EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
        }

        internal static bool GetDisplayHeader(this ReorderableList reorderableList)
        {
            return (bool)reorderableListDisplayHeaderFieldInfo.GetValue(reorderableList);
        }

        internal static void SetDisplayHeader(this ReorderableList reorderableList, bool value)
        {
            reorderableListDisplayHeaderFieldInfo.SetValue(reorderableList, value);
        }
    }
}
