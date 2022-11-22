using TNRD.CustomDrawers.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class BoundsIntDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            if (!compact)
            {
                return EditorGUI.GetPropertyHeight(SerializedPropertyType.BoundsInt,
                    hasLabel ? new GUIContent("dummy") : GUIContent.none);
            }

            return EditorGUI.GetPropertyHeight(SerializedPropertyType.BoundsInt, GUIContent.none);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUI.BoundsIntField(rect, label, (BoundsInt)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.BoundsIntField(rect, (BoundsInt)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.BoundsIntField(rects[1], string.Empty, (BoundsInt)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.BoundsIntField(label, (BoundsInt)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.BoundsIntField((BoundsInt)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.BoundsInt, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0].Column(2)[0], label);
            return EditorGUI.BoundsIntField(rects[1], string.Empty, (BoundsInt)instance);
        }
    }
}
