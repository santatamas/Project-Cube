using System.Windows;

namespace CubeProject.UIExtensions
{
	public class CloseButton : CaptionButton
	{
		static CloseButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseButton), new FrameworkPropertyMetadata(typeof(CloseButton)));
		}

		protected override void OnClick()
		{
			base.OnClick();
			SystemCommands.CloseWindow(Window.GetWindow(this));
		}
	}
}
