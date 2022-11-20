using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using UnityEditor;
using UnityEngine;

namespace TNRD.CustomDrawers
{
    public static partial class DrawerFactory
    {
        private class UnsupportedDrawer : IDrawer
        {
            private string readableType;

            private string GetReadableType(Type type)
            {
                if (!string.IsNullOrEmpty(readableType))
                    return readableType;

                readableType = CodeDomProvider.CreateProvider("CSharp").GetTypeOutput(new CodeTypeReference(type));
                return readableType;
            }

            /// <inheritdoc />
            float IDrawer.GetHeight(bool hasLabel, bool compact)
            {
                return 38;
            }

            /// <inheritdoc />
            object IDrawer.OnGUI(Rect rect, string label, object instance, bool compact)
            {
                if (string.IsNullOrEmpty(label))
                {
                    EditorGUI.HelpBox(rect,
                        instance == null
                            ? "No drawer available, no label assigned, and value is null"
                            : $"No drawer available for type '{GetReadableType(instance.GetType())}'",
                        MessageType.Warning);
                }
                else
                {
                    EditorGUI.HelpBox(rect,
                        instance == null
                            ? $"No drawer available for '{label}'"
                            : $"No drawer available for '{label}' ({GetReadableType(instance.GetType())})",
                        MessageType.Warning);
                }

                return instance;
            }

            /// <inheritdoc />
            object IDrawer.OnGUI(string label, object instance, bool compact)
            {
                if (string.IsNullOrEmpty(label))
                {
                    EditorGUILayout.HelpBox(
                        instance == null
                            ? "No drawer available, no label assigned, and value is null"
                            : $"No drawer available for type '{GetReadableType(instance.GetType())}'",
                        MessageType.Warning,
                        true);
                }
                else
                {
                    EditorGUILayout.HelpBox(
                        instance == null
                            ? $"No drawer available for '{label}'"
                            : $"No drawer available for '{label}' ({GetReadableType(instance.GetType())})",
                        MessageType.Warning,
                        true);
                }

                return instance;
            }
        }
    }
}
