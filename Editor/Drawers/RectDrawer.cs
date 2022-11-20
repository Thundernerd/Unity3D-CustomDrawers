using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class RectDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            if (!compact)
            {
                return EditorGUI.GetPropertyHeight(SerializedPropertyType.Rect,
                    hasLabel ? new GUIContent("dummy") : GUIContent.none);
            }

            return EditorGUI.GetPropertyHeight(SerializedPropertyType.Rect, GUIContent.none);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUI.RectField(rect, label, (Rect)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.RectField(rect, (Rect)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.RectField(rects[1], string.Empty, (Rect)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.RectField(label, (Rect)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.RectField((Rect)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Rect, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.RectField(rects[1], string.Empty, (Rect)instance);
        }
    }
}
