using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class LongDrawer : IDrawer
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
                return EditorGUI.LongField(rect, label, (long)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.LongField(rect, (long)instance);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.LongField(rects[1], (long)instance);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (compact)
                return EditorGUILayout.LongField(label, (long)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.LongField((long)instance);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.LongField((long)instance);
            }
        }
    }
}
