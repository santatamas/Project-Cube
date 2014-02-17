namespace PixelMatrixEditor.Models
{
    public class Matrix
    {
        private bool[,] _data;

        public Matrix(int sizeX, int sizeY)
        {
            _data = new bool[sizeX, sizeY];
        }

        public bool[,] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public bool this[int i, int j]
        {
            get { return _data[i, j]; }
            set { _data[i, j] = value; }
        }
    }
}
