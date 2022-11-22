using System;
using TNRD.CustomDrawers.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class ComponentDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            return Utilities.GetDefaultHeight(hasLabel, compact);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            Component component = (Component)instance;
            Type type = typeof(Component);

            if (compact)
                return EditorGUI.ObjectField(rect, label, component, type, true);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.ObjectField(rect, component, type, true);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.ObjectField(rects[1], component, type, true);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            Component component = (Component)instance;
            Type type = typeof(Component);

            if (compact)
                return EditorGUILayout.ObjectField(label, component, type, true);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.ObjectField(component, type, true);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.ObjectField(component, type, true);
            }
        }
    }
}
