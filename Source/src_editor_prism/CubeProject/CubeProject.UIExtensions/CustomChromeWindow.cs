using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Windows.Shell;

namespace CubeProject.UIExtensions
{
	public class CustomChromeWindow: Window, INotifyPropertyChanged
	{
	    public CustomChromeWindow()
	    {
            WindowChrome.SetWindowChrome(this, new WindowChrome
            {
                ResizeBorderThickness = new Thickness(3),
                CaptionHeight = 25,
                CornerRadius = new CornerRadius(3),
                GlassFrameThickness = new Thickness(0)
            });
            this.ResizeMode = ResizeMode.CanResize;
            this.WindowStyle = WindowStyle.None;
	    }

		protected override void OnStateChanged(EventArgs e)
		{
			base.OnStateChanged(e);
			OnPropertyChanged("CaptionButtonMargin");
		}

		public Thickness CaptionButtonMargin
		{
			get
			{
				if (WindowState == System.Windows.WindowState.Maximized)
					return new Thickness(6, 6, 0, 0); //Margin="0,0,12,0"
				else
					return new Thickness(0, 0, 0, 0);
			}
		}

		#region INotifyPropertyChanged
		private void OnPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
