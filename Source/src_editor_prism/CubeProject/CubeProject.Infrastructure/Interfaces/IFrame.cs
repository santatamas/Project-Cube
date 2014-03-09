using System;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Infrastructure.Interfaces
{
    public interface IFrame<T>
    {
        ColorDepth ColorDepth { get; }
        Int16 Height { get; }
        Int16 Width { get; }
        Int16 Duration { get; set; }
        T[,] Data { get; set; }

        T this[int i,int j]
        { get; set; }
    }
}
