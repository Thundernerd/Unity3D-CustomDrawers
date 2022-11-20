using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class Vector4Drawer : IDrawer
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
                return EditorGUI.Vector4Field(rect, label, (Vector4)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.Vector4Field(rect, string.Empty, (Vector4)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector4Field(rects[1], string.Empty, (Vector4)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.Vector4Field(label, (Vector4)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.Vector4Field(string.Empty, (Vector4)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector4, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector4Field(rects[1], string.Empty, (Vector4)instance);
        }
    }
}
