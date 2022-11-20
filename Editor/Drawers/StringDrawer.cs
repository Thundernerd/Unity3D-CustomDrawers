using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class StringDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            return Utilities.GetDefaultHeight(hasLabel, compact);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            if (compact)
                return EditorGUI.TextField(rect, label, (string)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.TextField(rect, (string)instance);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.TextField(rects[1], (string)instance);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (compact)
            {
                return EditorGUILayout.TextField(label, (string)instance);
            }

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.TextField((string)instance);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.TextField((string)instance);
            }
        }
    }
}
