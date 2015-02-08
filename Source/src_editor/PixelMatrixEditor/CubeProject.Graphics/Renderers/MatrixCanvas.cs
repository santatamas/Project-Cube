using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CubeProject.Graphics.Renderers
{
    public class MatrixCanvas : Canvas
    {

        private Brush _backGroundBrush;
        private Brush _pixelOnBrush;
        private Brush _pixelOffBrush;

        public MatrixCanvas()
        {
            _backGroundBrush = new SolidColorBrush(Color.FromRgb(125,140,115));
            _pixelOffBrush = new SolidColorBrush(Color.FromRgb(116, 129, 107));
            _pixelOnBrush = new SolidColorBrush(Color.FromRgb(53, 53, 53));
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            dc.DrawRectangle(_backGroundBrush, null,new Rect(0,0,this.ActualWidth, this.ActualHeight));

            for (int i = 0; i < 72; i++)
            {
                for (int j = 0; j < 72; j++)
                {
                    dc.DrawRectangle(_pixelOnBrush, null, new Rect(i * 10, j * 10, 8, 8));
                }
            }
        }
    }
}
