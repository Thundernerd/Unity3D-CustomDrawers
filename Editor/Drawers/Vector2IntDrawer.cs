using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class Vector2IntDrawer : IDrawer
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
                return EditorGUI.Vector2IntField(rect, label, (Vector2Int)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.Vector2IntField(rect, string.Empty, (Vector2Int)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector2IntField(rects[1], string.Empty, (Vector2Int)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.Vector2IntField(label, (Vector2Int)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.Vector2IntField(string.Empty, (Vector2Int)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector2Int, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector2IntField(rects[1], string.Empty, (Vector2Int)instance);
        }
    }
}
