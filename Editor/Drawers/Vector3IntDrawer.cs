using TNRD.CustomDrawers.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class Vector3IntDrawer : IDrawer
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
                return EditorGUI.Vector3IntField(rect, label, (Vector3Int)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.Vector3IntField(rect, string.Empty, (Vector3Int)instance);

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector3IntField(rects[1], string.Empty, (Vector3Int)instance);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (!compact)
                return EditorGUILayout.Vector3IntField(label, (Vector3Int)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.Vector3IntField(string.Empty, (Vector3Int)instance);

            Rect rect = EditorGUILayout.GetControlRect(false,
                EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3Int, GUIContent.none));

            Rect[] rects = rect.Row(new float[] { 0f, 1f }, new float[] { EditorGUIUtility.labelWidth, 0 });
            EditorGUI.LabelField(rects[0], label);
            return EditorGUI.Vector3IntField(rects[1], string.Empty, (Vector3Int)instance);
        }
    }
}
