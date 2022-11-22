using TNRD.CustomDrawers.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class QuaternionDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            return Utilities.GetDefaultHeight(hasLabel, compact);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            Vector3 euler = ((Quaternion)instance).eulerAngles;

            if (!compact)
                return Quaternion.Euler(EditorGUI.Vector3Field(rect, label, euler));

            if (string.IsNullOrEmpty(label))
                return Quaternion.Euler(EditorGUI.Vector3Field(rect, string.Empty, euler));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return Quaternion.Euler(EditorGUI.Vector3Field(rects[1], string.Empty, euler));
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            Vector3 euler = ((Quaternion)instance).eulerAngles;

            if (!compact)
                return Quaternion.Euler(EditorGUILayout.Vector3Field(label, euler));

            if (string.IsNullOrEmpty(label))
                return Quaternion.Euler(EditorGUILayout.Vector3Field(string.Empty, euler));

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return Quaternion.Euler(EditorGUI.Vector3Field(rects[1], string.Empty, euler));
        }
    }
}
