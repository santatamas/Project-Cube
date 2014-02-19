using System.IO;

namespace PixelMatrixEditor.Data
{
    public interface IBinarySerializer<T>
    {
        Stream Serialize(T data);
        T Deserialize(Stream stream);
        bool SupportsFileVersion(FileVersion version);
    }
}
