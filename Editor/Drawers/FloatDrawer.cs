using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class FloatDrawer : IDrawer
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
                return EditorGUI.FloatField(rect, label, (float)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.FloatField(rect, (float)instance);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.FloatField(rects[1], (float)instance);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (compact)
                return EditorGUILayout.FloatField(label, (float)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.FloatField((float)instance);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.FloatField((float)instance);
            }
        }
    }
}
