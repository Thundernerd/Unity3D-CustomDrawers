using TNRD.RectEx;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers
{
    internal class AnimationCurveDrawer : IDrawer
    {
        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            return Utilities.GetDefaultHeight(hasLabel, compact);
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            if (compact)
                return EditorGUI.CurveField(rect, label, (AnimationCurve)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUI.CurveField(rect, (AnimationCurve)instance);

            Rect[] rects = rect.Column(2);
            EditorGUI.LabelField(rects[0], label);

            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUI.CurveField(rects[1], (AnimationCurve)instance);
            }
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            if (compact)
                return EditorGUILayout.CurveField(label, (AnimationCurve)instance);

            if (string.IsNullOrEmpty(label))
                return EditorGUILayout.CurveField((AnimationCurve)instance);

            EditorGUILayout.LabelField(label);
            using (new EditorGUI.IndentLevelScope())
            {
                return EditorGUILayout.CurveField((AnimationCurve)instance);
            }
        }
    }
}
