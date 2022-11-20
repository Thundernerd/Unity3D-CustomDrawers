using System;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TNRD.CustomDrawers.Drawers.Special
{
    internal class ArrayDrawer : IDrawer
    {
        private readonly IDrawer elementDrawer;
        private readonly Type elementType;
        private readonly ReorderableList reorderableList;

        private IList list;
        private string header;
        private bool drawElementCompact;

        public ArrayDrawer(Type instanceType, object instance)
        {
            list = (IList)instance;
            elementType = instanceType.GetElementType();
            elementDrawer = DrawerFactory.CreateDrawer(elementType, Activator.CreateInstance(elementType));

            reorderableList = new ReorderableList(list, elementType, true, true, true, true);
            reorderableList.drawHeaderCallback += OnDrawHeader;
            reorderableList.drawElementCallback += OnDrawElement;
            reorderableList.elementHeightCallback += OnGetElementHeight;
            reorderableList.onAddCallback += OnAdd;
            reorderableList.onRemoveCallback += OnRemove;
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

        private void OnAdd(ReorderableList rList)
        {
            Array source = (Array)list;
            Array destination = Array.CreateInstance(elementType, source.Length + 1);
            Array.Copy(source, 0, destination, 0, source.Length);
            rList.list = destination;
        }

        private void OnRemove(ReorderableList rList)
        {
            Array source = (Array)list;
            Array destination = Array.CreateInstance(elementType, list.Count - 1);

            if (rList.index == 0)
            {
                Array.Copy(source, 1, destination, 0, source.Length - 1);
            }
            else if (rList.index == list.Count - 1)
            {
                Array.Copy(source, 0, destination, 0, source.Length - 1);
            }
            else
            {
                Array.Copy(source, 0, destination, 0, rList.index);
                Array.Copy(source, rList.index + 1, destination, rList.index, source.Length - rList.index - 1);
            }

            rList.list = destination;
        }
    }
}
