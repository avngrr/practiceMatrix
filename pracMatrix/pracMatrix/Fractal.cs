namespace pracMatrix
{
    public class Fractal
    {
        public char[,] CurrentMatrix { get; set; }
        Dictionary<string, char[,]> _extensions;
        public Fractal(string matrix)
        {
            CurrentMatrix = Matrix.BuildFromString(matrix);
            _extensions = new Dictionary<string, char[,]>();
        }
        public void RunExtensions()
        {
            int size = CurrentMatrix.GetLength(0);
            List<char[,]> splitMatrices;
            List<char[,]> toMatrices = new List<char[,]>();
            if (size % 2 == 0)
            {
                splitMatrices = Matrix.SplitMatrix(CurrentMatrix, 2);
            }
            else
            {
                splitMatrices = Matrix.SplitMatrix(CurrentMatrix, 3);
            }
            foreach (char[,] matrix in splitMatrices)
            {
                toMatrices.Add(FindUpgradedMatrix(matrix));
            }
            CurrentMatrix = Matrix.JoinMatrices(toMatrices);
        }
        private char[,] FindUpgradedMatrix(char[,] matrix)
        {
            string matrixString = Matrix.MatrixToString(matrix);
            //matrixString = "../..";
            if (_extensions.ContainsKey(matrixString))
            {
                return _extensions[matrixString];
            }
            return matrix;
        }
        public void AddExtension(string extensionString)
        {
            string[] from = extensionString.Split(new[] { "=>" }, StringSplitOptions.TrimEntries);
            char[,] origFromMatrix = Matrix.BuildFromString(from[0]);
            char[,] toMatrix = Matrix.BuildFromString(from[1]);

            //Insert every matrix 
            //Original
            AddExtension(origFromMatrix, toMatrix);

            //Inverses vertical and horizontal
            char[,] fromMatrix = Matrix.InverseHorizontal(origFromMatrix);
            AddExtension(fromMatrix, toMatrix);
            fromMatrix = Matrix.InverseVertical(origFromMatrix);
            AddExtension(fromMatrix, toMatrix);

            //All rotations
            fromMatrix = Matrix.Rotate(origFromMatrix);
            AddExtension(Matrix.InverseVertical(fromMatrix), toMatrix);
            AddExtension(Matrix.InverseHorizontal(fromMatrix), toMatrix);
            AddExtension(fromMatrix, toMatrix);
            fromMatrix = Matrix.Rotate(fromMatrix);
            AddExtension(Matrix.InverseVertical(fromMatrix), toMatrix);
            AddExtension(Matrix.InverseHorizontal(fromMatrix), toMatrix);
            AddExtension(fromMatrix, toMatrix);
            fromMatrix = Matrix.Rotate(fromMatrix);
            AddExtension(Matrix.InverseVertical(fromMatrix), toMatrix);
            AddExtension(Matrix.InverseHorizontal(fromMatrix), toMatrix);
            AddExtension(fromMatrix, toMatrix);
        }
        private void AddExtension(char[,] fromMatrix, char[,] toMatrix)
        {
            string matrixString = Matrix.MatrixToString(fromMatrix);
            if (!_extensions.ContainsKey(matrixString))
            {
                _extensions.Add(matrixString, toMatrix);
            }
        }
        public int CountLightsOn()
        {
            int value = 0;
            for (int i = 0; i < CurrentMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentMatrix.GetLength(0); j++)
                {
                    if (CurrentMatrix[i, j] == '#') value++;
                }
            }
            return value;
        }
    }
}
