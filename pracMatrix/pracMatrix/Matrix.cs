namespace pracMatrix
{
    public static class Matrix
    {
        public static char[,] BuildFromString(string s)
        {
            string[] lines = s.Split("/");
            int size = lines[0].Length;
            char[,] matrix = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = char.Parse(lines[i].Substring(j, 1));
                }
            }
            return matrix;
        }
        public static string MatrixToString(char[,] matrix)
        {
            return String.Join("/", matrix.OfType<char>()
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / matrix.GetLength(1), x => x.value,
                (i, chars) => $"{string.Join("", chars)}"));
        }
        public static bool Compare(char[,] matrix1, char[,] matrix2)
        {
            bool result = true;
            int size = matrix1.GetLength(0);
            if (matrix1.GetLength(0) != matrix2.GetLength(0))
            {
                return false;
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix1[i, j] != matrix2[i, j]) result = false;
                }
            }
            return result;
        }
        public static char[,] Rotate(char[,] matrix)
        {
            int size = matrix.GetLength(0);
            char[,] changedMatrix = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    changedMatrix[j, size - i - 1] = matrix[i, j];
                }
            }
            return changedMatrix;
        }
        public static char[,] InverseHorizontal(char[,] matrix)
        {
            int size = matrix.GetLength(0);
            char[,] changedMatrix = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    changedMatrix[i, size - 1 - j] = matrix[i, j];
                }
            }
            return changedMatrix;
        }
        public static char[,] InverseVertical(char[,] matrix)
        {
            int size = matrix.GetLength(0);
            char[,] changedMatrix = new char[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    changedMatrix[size - 1 - i, j] = matrix[i, j];
                }
            }
            return changedMatrix;
        }
        public static char[,] InsertMatrixIntoMatrix(char[,] bigMatrix, char[,] smallMatrix, int x, int y)
        {
            for (int i = 0; i < smallMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < smallMatrix.GetLength(0); j++)
                {
                    bigMatrix[i + x, j + y] = smallMatrix[i, j];
                }
            }
            return bigMatrix;
        }
        public static char[,] JoinMatrices(List<char[,]> matrices)
        {
            int matrixPerRow = Convert.ToInt32(Math.Sqrt(matrices.Count));
            int sizeSmallMatrix = matrices[0].GetLength(0);
            char[,] newMatrix = new char[matrixPerRow * sizeSmallMatrix, matrixPerRow * sizeSmallMatrix];
            for (int i = 0; i < matrices.Count; i++)
            {
                int x = i / matrixPerRow * sizeSmallMatrix;
                int y = i % matrixPerRow * sizeSmallMatrix;
                newMatrix = Matrix.InsertMatrixIntoMatrix(newMatrix, matrices[i], x, y);
            }
            return newMatrix;
        }
        public static List<char[,]> SplitMatrix(char[,] matrix, int rowColCount)
        {
            List<char[,]> matrices = new List<char[,]>();

            int rows = matrix.GetLength(0) / rowColCount;
            char[,] newRaster;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    newRaster = new char[rowColCount, rowColCount];
                    for (int x = 0; x < rowColCount; x++)
                    {
                        for (int y = 0; y < rowColCount; y++)
                        {
                            newRaster[x, y] = matrix[x + i * rowColCount, y + j * rowColCount];
                        }
                    }
                    matrices.Add(newRaster);
                }
            }
            return matrices;
        }
    }
}
