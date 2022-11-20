using System;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers.Special
{
    internal class ListDrawer : IDrawer
    {
        private readonly IDrawer elementDrawer;
        private readonly ReorderableList reorderableList;

        private IList list;
        private string header;
        private bool drawElementCompact;

        public ListDrawer(Type instanceType, object instance)
        {
            Type elementType = instanceType.GetGenericArguments()[0];
            list = (IList)instance;
            elementDrawer = DrawerFactory.CreateDrawer(elementType, Activator.CreateInstance(elementType));

            reorderableList = new ReorderableList(list, elementType, true, true, true, true);
            reorderableList.drawHeaderCallback += OnDrawHeader;
            reorderableList.drawElementCallback += OnDrawElement;
            reorderableList.elementHeightCallback += OnGetElementHeight;
        }

        /// <inheritdoc />
        float IDrawer.GetHeight(bool hasLabel, bool compact)
        {
            if (hasLabel != reorderableList.GetDisplayHeader())
                reorderableList.SetDisplayHeader(hasLabel);

            return reorderableList.GetHeight();
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
        {
            header = label;
            drawElementCompact = compact;

            if (!string.IsNullOrEmpty(label) != reorderableList.GetDisplayHeader())
                reorderableList.SetDisplayHeader(!string.IsNullOrEmpty(label));

            list = (IList)instance;
            reorderableList.list = list;
            reorderableList.DoList(rect);
            return reorderableList.list;
        }

        /// <inheritdoc />
        object IDrawer.OnGUI(string label, object instance, bool compact)
        {
            header = label;
            drawElementCompact = compact;

            if (!string.IsNullOrEmpty(label) != reorderableList.GetDisplayHeader())
                reorderableList.SetDisplayHeader(!string.IsNullOrEmpty(label));

            list = (IList)instance;
            reorderableList.list = list;
            reorderableList.DoLayoutList();
            return reorderableList.list;
        }

        private float OnGetElementHeight(int index)
        {
            return elementDrawer.GetHeight(true, drawElementCompact);
        }

        private void OnDrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, header);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.height -= EditorGUIUtility.standardVerticalSpacing;
            list[index] = elementDrawer.OnGUI(rect, $"Element {index}", list[index], drawElementCompact);
        }
    }
}
