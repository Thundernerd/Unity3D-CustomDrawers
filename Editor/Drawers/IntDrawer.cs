using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class IntDrawer : IDrawer
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
                return EditorGUI.IntField(rect, label, (int)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.IntField(rect, (int)instance);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.IntField(rects[1], (int)instance);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (compact)
                return EditorGUILayout.IntField(label, (int)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.IntField((int)instance);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.IntField((int)instance);
            }
        }
    }
}
