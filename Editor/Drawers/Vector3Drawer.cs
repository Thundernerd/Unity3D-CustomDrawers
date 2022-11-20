using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class Vector3Drawer : IDrawer
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
                return EditorGUI.Vector3Field(rect, label, (Vector3)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.Vector3Field(rect, string.Empty, (Vector3)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector3Field(rects[1], string.Empty, (Vector3)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.Vector3Field(label, (Vector3)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.Vector3Field(string.Empty, (Vector3)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector3Field(rects[1], string.Empty, (Vector3)instance);
        }
    }
}
