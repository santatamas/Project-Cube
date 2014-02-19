using System;
using System.IO;
using CubeProject.Data.Entities;
using PixelMatrixEditor.Data;

namespace CubeProject.Data.Serializers
{
    public class AnimationSerializer : IBinarySerializer<Animation>
    {
        public Stream Serialize(Animation data)
        {
            MemoryStream ms = new MemoryStream();
            
            // Write out file version - 1 byte
            ms.WriteByte((byte)FileVersion.V1);

            return null;

        }

        public Animation Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }

        public bool SupportsFileVersion(FileVersion version)
        {
            return version == FileVersion.V1;
        }
    }
}
