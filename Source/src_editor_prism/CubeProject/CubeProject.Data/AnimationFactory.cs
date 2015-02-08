using CubeProject.Data.Converters;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using CubeProject.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeProject.Data
{
    public class AnimationFactory
    {
        public Animation CreateAnimation(byte[] animationData, string fileExtension)
        {
            IBinarySerializer<Animation> serializer;
            switch(fileExtension)
            {
                case ".pma":
                    serializer = new AnimationSerializer();
                    return serializer.Deserialize(animationData);
                case ".pmz":
                    serializer = new ZippedAnimationSerializer();
                    return serializer.Deserialize(animationData);
                case ".gif":
                    return new GifConverter().Convert(animationData);
                default:
                    throw new NotSupportedException(String.Format("AnimationFactory doesn't support {0}", fileExtension));
            }
        }
    }
}
