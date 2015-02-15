using System;
using CubeProject.Data.Converters;
using CubeProject.Data.Entities;
using CubeProject.Data.Serializers;
using CubeProject.Infrastructure.Interfaces;

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
                    return GifConverter.Convert(animationData);
                default:
                    throw new NotSupportedException(String.Format("AnimationFactory doesn't support {0}", fileExtension));
            }
        }
    }
}
