using TNRD.CustomDrawers.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class BoolDrawer : IDrawer
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
                return EditorGUI.Toggle(rect, label, (bool)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.Toggle(rect, (bool)instance);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.Toggle(rects[1], (bool)instance);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (compact)
                return EditorGUILayout.Toggle(label, (bool)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.Toggle((bool)instance);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.Toggle((bool)instance);
            }
        }
    }
}
