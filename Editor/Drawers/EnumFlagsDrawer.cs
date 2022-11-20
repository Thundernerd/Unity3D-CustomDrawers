using System;
using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class EnumFlagsDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            return Utilities.GetDefaultHeight(hasLabel, compact);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            Enum selected = (Enum)instance;

            if (compact)
                return EditorGUI.EnumFlagsField(rect, label, selected);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.EnumFlagsField(rect, selected);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.EnumFlagsField(rects[1], selected);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            Enum selected = (Enum)instance;

            if (compact)
                return EditorGUILayout.EnumFlagsField(label, selected);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.EnumFlagsField(selected);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.EnumFlagsField(selected);
            }
        }
    }
}
