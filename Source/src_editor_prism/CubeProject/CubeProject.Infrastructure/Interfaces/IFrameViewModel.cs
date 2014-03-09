namespace CubeProject.Infrastructure.Interfaces
{
    public interface IFrameViewModel
    {
        IFrame<byte> Frame { get; set; }
        void ReDraw();
    }
}
