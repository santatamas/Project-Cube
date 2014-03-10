using System;
using CubeProject.Infrastructure.Enums;
using CubeProject.Infrastructure.Interfaces;

namespace CubeProject.Data.Entities
{
    /// <summary>
    /// Represents a generic animation frame.
    /// </summary>
    /// <typeparam name="T">The datatype of each stored pixel.</typeparam>
    [Serializable]
    public class Frame<T>: IFrame<T>
    {
        /// <summary>
        /// Gets the width of the pixel matrix.
        /// </summary>
        /// <value>
        /// The width of the pixel matrix.
        /// </value>
        public Int16 Width { get; private set; }
        /// <summary>
        /// Gets the height of the pixel matrix.
        /// </summary>
        /// <value>
        /// The height of the pixel matrix.
        /// </value>
        public Int16 Height { get; private set; }
        /// <summary>
        /// Gets or sets the duration of the frame.
        /// </summary>
        /// <value>
        /// The duration of the frame.
        /// </value>
        public Int16 Duration { get; set; }

        /// <summary>
        /// Gets the color depth.
        /// </summary>
        /// <value>
        /// The color depth.
        /// </value>
        public ColorDepth ColorDepth { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame{T}"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="colorDepth">The color depth.</param>
        public Frame(Int16 width, Int16 height, ColorDepth colorDepth)
        {
            Width = width;
            Height = height;
            _data = new T[width, height];
            ColorDepth = colorDepth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Frame{T}"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Frame(Int16 width, Int16 height)
        {
            Width = width;
            Height = height;
            _data = new T[width,height];
        }

        private T[,] _data;
        /// <summary>
        /// Gets or sets the frame (matrix) data.
        /// </summary>
        /// <value>
        /// The frame (matrix) data.
        /// </value>
        public T[,] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="T"/> by the specified coordinate.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="i">The i coordinate.</param>
        /// <param name="j">The j coordinate</param>
        /// <returns></returns>
        public T this[int i, int j]
        {
            get { return _data[i, j]; }
            set { _data[i, j] = value; }
        }
    }
}
