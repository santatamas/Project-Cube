namespace CubeProject.Infrastructure.Interfaces
{
    /// <summary>
    /// Handles user interaction and rendering tasks for an <see cref="IFrame"/> object.
    /// </summary>
    public interface IFrameViewModel<T>
    {
        IFrame<T> Frame { get; set; }
    }
}
