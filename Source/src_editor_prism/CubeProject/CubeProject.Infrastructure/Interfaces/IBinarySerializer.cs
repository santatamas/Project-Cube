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
        byte[] Serialize(T data);
        T Deserialize(byte[] stream);
        bool SupportsFileVersion(FileVersion version);
    }
}
