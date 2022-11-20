using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class RectIntDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            if (!compact)
            {
                return EditorGUI.GetPropertyHeight(SerializedPropertyType.RectInt,
                    hasLabel ? new GUIContent("dummy") : GUIContent.none);
            }

            return EditorGUI.GetPropertyHeight(SerializedPropertyType.RectInt, GUIContent.none);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUI.RectIntField(rect, label, (RectInt)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.RectIntField(rect, (RectInt)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.RectIntField(rects[1], string.Empty, (RectInt)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.RectIntField(label, (RectInt)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.RectIntField((RectInt)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.RectInt, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.RectIntField(rects[1], string.Empty, (RectInt)instance);
        }
    }
}
