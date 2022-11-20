using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class BoundsDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            if (!compact)
            {
                return EditorGUI.GetPropertyHeight(SerializedPropertyType.Bounds,
                    hasLabel ? new GUIContent("dummy") : GUIContent.none);
            }

            return EditorGUI.GetPropertyHeight(SerializedPropertyType.Bounds, GUIContent.none);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUI.BoundsField(rect, label, (Bounds)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.BoundsField(rect, (Bounds)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.BoundsField(rects[1], string.Empty, (Bounds)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.BoundsField(label, (Bounds)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.BoundsField((Bounds)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Bounds, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.BoundsField(rects[1], string.Empty, (Bounds)instance);
        }
    }
}
