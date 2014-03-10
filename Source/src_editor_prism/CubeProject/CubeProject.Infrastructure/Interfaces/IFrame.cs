using System;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Infrastructure.Interfaces
{
    /// <summary>
    /// Represents a generic animation frame.
    /// </summary>
    /// <typeparam name="T">The datatype of each stored pixel.</typeparam>
    public interface IFrame<T>
    {
        /// <summary>
        /// Gets the color depth.
        /// </summary>
        /// <value>
        /// The color depth.
        /// </value>
        ColorDepth ColorDepth { get; }
        /// <summary>
        /// Gets the height of the pixel matrix.
        /// </summary>
        /// <value>
        /// The height of the pixel matrix.
        /// </value>
        Int16 Height { get; }
        /// <summary>
        /// Gets the width of the pixel matrix.
        /// </summary>
        /// <value>
        /// The width of the pixel matrix.
        /// </value>
        Int16 Width { get; }
        /// <summary>
        /// Gets or sets the duration of the frame.
        /// </summary>
        /// <value>
        /// The duration of the frame.
        /// </value>
        Int16 Duration { get; set; }
        /// <summary>
        /// Gets or sets the frame (matrix) data.
        /// </summary>
        /// <value>
        /// The frame (matrix) data.
        /// </value>
        T[,] Data { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T"/> by the specified coordinate.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="i">The i coordinate.</param>
        /// <param name="j">The j coordinate</param>
        /// <returns></returns>
        T this[int i,int j]
        { get; set; }
    }
}
