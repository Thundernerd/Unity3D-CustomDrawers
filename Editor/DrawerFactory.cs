using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using TNRD.CustomDrawers.Drawers;
using TNRD.CustomDrawers.Drawers.Special;
using UnityEngine;

namespace TNRD.CustomDrawers
{
    /// <summary>
    /// A factory class where you can register/override drawers and get instances of said drawers
    /// </summary>
    public static partial class DrawerFactory
    {
        static DrawerFactory()
        {
            RegisterDrawer<AnimationCurve, AnimationCurveDrawer>(false);
            RegisterDrawer<bool, BoolDrawer>(false);
            RegisterDrawer<Bounds, BoundsDrawer>(false);
            RegisterDrawer<BoundsInt, BoundsIntDrawer>(false);
            RegisterDrawer<Color, ColorDrawer>(false);
            RegisterDrawer<Component, ComponentDrawer>(true);
            RegisterDrawer<double, DoubleDrawer>(false);
            // Enum Drawer
            // Enum Flags Drawer
            RegisterDrawer<float, FloatDrawer>(false);
            RegisterDrawer<GameObject, GameObjectDrawer>(false);
            RegisterDrawer<Gradient, GradientDrawer>(false);
            RegisterDrawer<int, IntDrawer>(false);
            RegisterDrawer<long, LongDrawer>(false);
            RegisterDrawer<Quaternion, QuaternionDrawer>(false);
            RegisterDrawer<Rect, RectDrawer>(false);
            RegisterDrawer<RectInt, RectIntDrawer>(false);
            RegisterDrawer<string, StringDrawer>(false);
            RegisterDrawer<Vector2, Vector2Drawer>(false);
            RegisterDrawer<Vector3, Vector3Drawer>(false);
            RegisterDrawer<Vector4, Vector4Drawer>(false);
            RegisterDrawer<Vector2Int, Vector2IntDrawer>(false);
            RegisterDrawer<Vector3Int, Vector3IntDrawer>(false);
        }

        private static readonly Dictionary<Type, Type> overrideValueToDrawer = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, Type> overrideInheritedValueToDrawer = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, Type> valueToDrawer = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, Type> inheritedValueToDrawer = new Dictionary<Type, Type>();

        private static Type GetDrawerType(Type valueType, out bool isSpecialDrawer)
        {
            isSpecialDrawer = true;

            if (TryGetSpecialDrawer(valueType, out Type drawerType))
                return drawerType;

            isSpecialDrawer = false;

            if (TryGetOverrideDrawer(valueType, out drawerType))
                return drawerType;

            if (TryGetDrawer(valueType, out drawerType))
                return drawerType;

            return typeof(UnsupportedDrawer);
        }

        private static bool TryGetSpecialDrawer(Type valueType, out Type drawerType)
        {
            if (valueType.IsArray)
            {
                drawerType = valueType.GetElementType().IsArray
                    ? typeof(UnsupportedDrawer)
                    : typeof(ArrayDrawer);
                return true;
            }

            if (typeof(IList).IsAssignableFrom(valueType) && valueType.IsGenericType)
            {
                drawerType = typeof(ListDrawer);
                return true;
            }

            drawerType = null;
            return false;
        }

        private static bool TryGetOverrideDrawer(Type valueType, out Type drawerType)
        {
            if (overrideValueToDrawer.TryGetValue(valueType, out drawerType))
                return true;

            foreach (KeyValuePair<Type, Type> kvp in overrideInheritedValueToDrawer)
            {
                if (!valueType.IsSubclassOf(kvp.Key))
                    continue;

                drawerType = kvp.Value;
                return true;
            }

            drawerType = null;
            return false;
        }

        private static bool TryGetDrawer(Type valueType, out Type drawerType)
        {
            if (valueType.IsEnum)
            {
                drawerType = valueType.GetCustomAttribute(typeof(FlagsAttribute)) == null
                    ? typeof(EnumDrawer)
                    : typeof(EnumFlagsDrawer);
                return true;
            }

            if (valueToDrawer.TryGetValue(valueType, out drawerType))
                return true;

            foreach (KeyValuePair<Type, Type> kvp in inheritedValueToDrawer)
            {
                if (valueType.IsSubclassOf(kvp.Key))
                {
                    drawerType = kvp.Value;
                    return true;
                }
            }

            drawerType = null;
            return false;
        }

        /// <summary>
        /// Tries to create a drawer
        /// </summary>
        /// <param name="fieldInfo">A field info that describes a field</param>
        /// <param name="value">The value that the field info describes</param>
        /// <returns><see cref="IDrawer"/></returns>
        [PublicAPI]
        public static IDrawer CreateDrawer(FieldInfo fieldInfo, object value)
        {
            return CreateDrawer(fieldInfo.FieldType, value);
        }

        /// <summary>
        /// Tries to create a drawer
        /// </summary>
        /// <param name="propertyInfo">A property info that describes a property</param>
        /// <param name="value">The value that the property info describes</param>
        /// <returns><see cref="IDrawer"/></returns>
        [PublicAPI]
        public static IDrawer CreateDrawer(PropertyInfo propertyInfo, object value)
        {
            return CreateDrawer(propertyInfo.PropertyType, value);
        }

        /// <summary>
        /// Tries to create a drawer
        /// </summary>
        /// <param name="value">The value itself</param>
        /// <typeparam name="TValue">The type of the value you want the drawer for</typeparam>
        /// <returns><see cref="IDrawer"/></returns>
        [PublicAPI]
        public static IDrawer CreateDrawer<TValue>(TValue value)
        {
            return CreateDrawer(typeof(TValue), value);
        }

        /// <summary>
        /// Tries to create a drawer
        /// </summary>
        /// <param name="valueType">The type of the value you want the drawer for</param>
        /// <param name="value">The value itself</param>
        /// <returns><see cref="IDrawer"/></returns>
        [PublicAPI]
        public static IDrawer CreateDrawer(Type valueType, object value)
        {
            Type drawerType = GetDrawerType(valueType, out bool isSpecialDrawer);
            return isSpecialDrawer
                ? (IDrawer)Activator.CreateInstance(drawerType, valueType, value)
                : (IDrawer)Activator.CreateInstance(drawerType);
        }

        /// <summary>
        /// Registers a drawer
        /// </summary>
        /// <param name="inherited">Should this drawer be used for classes that inherit from TValue</param>
        /// <typeparam name="TValue">The type of the value this drawer is for</typeparam>
        /// <typeparam name="TDrawer">The type of the drawer</typeparam>
        [PublicAPI]
        public static void RegisterDrawer<TValue, TDrawer>(bool inherited)
            where TDrawer : IDrawer
        {
            RegisterDrawer(typeof(TValue), typeof(TDrawer), inherited);
        }

        /// <summary>
        /// Registers a drawer
        /// </summary>
        /// <param name="valueType">The type of the value this drawer is for</param>
        /// <param name="drawerType">The type of the drawer</param>
        /// <param name="inherited">Should this drawer be used for classes that inherit from the valueType</param>
        [PublicAPI]
        public static void RegisterDrawer(Type valueType, Type drawerType, bool inherited)
        {
            if (!typeof(IDrawer).IsAssignableFrom(drawerType))
                throw new Exception();

            if (inherited && inheritedValueToDrawer.ContainsKey(valueType))
                throw new Exception();
            if (!inherited && valueToDrawer.ContainsKey(valueType))
                throw new Exception();

            if (inherited)
                inheritedValueToDrawer[valueType] = drawerType;
            else
                valueToDrawer[valueType] = drawerType;
        }

        /// <summary>
        /// Overrides an existing drawer
        /// </summary>
        /// <param name="inherited">Should this drawer be used for classes that inherit from TValue</param>
        /// <typeparam name="TValue">The type of the value this drawer is for</typeparam>
        /// <typeparam name="TDrawer">The type of the drawer</typeparam>
        [PublicAPI]
        public static void OverrideDrawer<TValue, TDrawer>(bool inherited)
            where TDrawer : IDrawer
        {
            OverrideDrawer(typeof(TValue), typeof(TDrawer), inherited);
        }

        /// <summary>
        /// Overrides an existing drawer 
        /// </summary>
        /// <param name="valueType">The type of the value this drawer is for</param>
        /// <param name="drawerType">The type of the drawer</param>
        /// <param name="inherited">Should this drawer be used for classes that inherit from the valueType</param>
        [PublicAPI]
        public static void OverrideDrawer(Type valueType, Type drawerType, bool inherited)
        {
            if (!typeof(IDrawer).IsAssignableFrom(drawerType))
                throw new Exception();

            if (inherited && overrideInheritedValueToDrawer.TryGetValue(valueType, out Type inheritedDrawer))
                Debug.LogWarning($"Another override drawer already exists and will be overridden ({inheritedDrawer})");
            if (!inherited && overrideValueToDrawer.TryGetValue(valueType, out Type drawer))
                Debug.LogWarning($"Another override drawer already exists and will be overridden ({drawer})");

            if (inherited)
                overrideInheritedValueToDrawer[valueType] = drawerType;
            else
                overrideValueToDrawer[valueType] = drawerType;
        }

        /// <summary>
        /// Creates a drawer that can be used for displaying types that are not supported
        /// </summary>
        [PublicAPI]
        public static IDrawer GetUnsupportedDrawer()
        {
            return new UnsupportedDrawer();
        }
    }
}
