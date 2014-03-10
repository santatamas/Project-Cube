using System.IO;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Infrastructure.Interfaces
{
    /// <summary>
    /// Capable to serialize and deserialize a specific object type in binary format.
    /// Also support file type versioning.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBinarySerializer<T>
    {
        Stream Serialize(T data);
        T Deserialize(Stream stream);
        bool SupportsFileVersion(FileVersion version);
    }
}
