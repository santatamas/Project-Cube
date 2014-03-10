namespace CubeProject.Infrastructure.Enums
{
    /// <summary>
    /// Information about the drawing mode.
    /// </summary>
    public enum ToggleMode
    {
        /// <summary>
        /// The renderer should turn ON the pixel
        /// </summary>
        On,

        /// <summary>
        /// The renderer should turn OFF the pixel
        /// </summary>
        Off,

        /// <summary>
        /// The renderer should negate the pixel's value
        /// </summary>
        Inverse
    }
}
