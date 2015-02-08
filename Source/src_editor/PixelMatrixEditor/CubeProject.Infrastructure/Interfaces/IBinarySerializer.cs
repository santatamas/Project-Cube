using System.IO;
using CubeProject.Infrastructure.Enums;

namespace CubeProject.Infrastructure.Interfaces
{
    public interface IBinarySerializer<T>
    {
        Stream Serialize(T data);
        T Deserialize(Stream stream);
        bool SupportsFileVersion(FileVersion version);
    }
}
