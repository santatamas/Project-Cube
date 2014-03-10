using System;

namespace CubeProject.Infrastructure.Interfaces
{
    /// <summary>
    /// ViewModel of the 'Change duration' dialog.
    /// </summary>
    public interface IChangeDurationViewModel
    {
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        Int16 Duration { get; set; }
    }
}
