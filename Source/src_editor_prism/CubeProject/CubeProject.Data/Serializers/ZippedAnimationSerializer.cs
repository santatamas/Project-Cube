using System.IO;
using System.IO.Compression;
using CubeProject.Data.Entities;
using CubeProject.Infrastructure.Utility;

namespace CubeProject.Data.Serializers
{
    public class ZippedAnimationSerializer : AnimationSerializer
    {
        public override Animation Deserialize(byte[] data)
        {

            using (Stream file = new MemoryStream(data))
            using (Stream gzip = new GZipStream(file, CompressionMode.Decompress))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                StreamUtility.CopyStream(gzip, memoryStream);
                return base.Deserialize(memoryStream.ToArray());
            }       
        }

        public override byte[] Serialize(Animation data)
        {
            var serializationResult = base.Serialize(data);
            byte[] result;
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (Stream outGzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    StreamUtility.CopyStream(new MemoryStream(serializationResult), outGzipStream);
                }
                result = outputStream.ToArray();
            }

            return result;
        }
    }
}
