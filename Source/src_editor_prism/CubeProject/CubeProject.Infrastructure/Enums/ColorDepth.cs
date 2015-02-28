namespace CubeProject.Infrastructure.Enums
{
    /// <summary>
    /// Indicates the colordepth of a frame.
    /// </summary>
    public enum ColorDepth
    {
        /// <summary>
        /// The frame only contains 0 or 1 values.
        /// </summary>
        Onebit = 1,

        /// <summary>
        /// The frame is capable to store the color value in a byte [255 possible shades]
        /// </summary>
        GrayScale = 8,

        /// <summary>
        /// The frame is capable to store true colors with alpha channel
        /// </summary>
        Color = 16
    }
}
