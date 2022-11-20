using UnityEngine;

namespace TNRD.CustomDrawers
{
    /// <summary>
    /// An interface that can be implemented and registered with the <see cref="DrawerFactory"/>
    /// </summary>
    public interface IDrawer
    {
        /// <summary>
        /// Calculates the height of this element. To be used in combination with <see cref="OnGUI(UnityEngine.Rect,string,object,bool)"/>
        /// </summary>
        /// <param name="hasLabel">Will this element have a label</param>
        /// <param name="compact">Will this element be drawn in compact mode</param>
        /// <returns>The calculated height</returns>
        float GetHeight(bool hasLabel, bool compact);

        /// <summary>
        /// Draws the element positioned with the given rect
        /// </summary>
        /// <param name="rect">The position where to draw the element</param>
        /// <param name="label">The label for the element</param>
        /// <param name="instance">The value to draw</param>
        /// <param name="compact">Should this be drawn in compact mode</param>
        /// <returns>The value</returns>
        object OnGUI(Rect rect, string label, object instance, bool compact);

        /// <summary>
        /// Draws the element
        /// </summary>
        /// <param name="label">The label for the element</param>
        /// <param name="instance">The value to draw</param>
        /// <param name="compact">Should this be drawn in compact mode</param>
        /// <returns></returns>
        object OnGUI(string label, object instance, bool compact);
    }
}
