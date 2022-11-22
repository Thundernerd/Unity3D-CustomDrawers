using TNRD.CustomDrawers.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class Vector2Drawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            return Utilities.GetDefaultHeight(hasLabel, compact);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUI.Vector2Field(rect, label, (Vector2)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.Vector2Field(rect, string.Empty, (Vector2)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector2Field(rects[1], string.Empty, (Vector2)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.Vector2Field(label, (Vector2)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.Vector2Field(string.Empty, (Vector2)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector2, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector2Field(rects[1], string.Empty, (Vector2)instance);
        }
    }
}
