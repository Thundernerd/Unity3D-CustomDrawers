using System;
using TNRD.CustomDrawers.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class GameObjectDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            return Utilities.GetDefaultHeight(hasLabel, compact);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            GameObject gameObject = (GameObject)instance;
            Type type = typeof(GameObject);

            if (compact)
                return EditorGUI.ObjectField(rect, label, gameObject, type, true);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.ObjectField(rect, gameObject, type, true);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.ObjectField(rects[1], gameObject, type, true);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            GameObject gameObject = (GameObject)instance;
            Type type = typeof(GameObject);

            if (compact)
                return EditorGUILayout.ObjectField(label, gameObject, type, true);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.ObjectField(gameObject, type, true);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.ObjectField(gameObject, type, true);
            }
        }
    }
}
