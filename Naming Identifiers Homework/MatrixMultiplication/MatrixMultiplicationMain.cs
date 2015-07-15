namespace MatrixMultiplication
{
    using System;

    internal class MatrixMultiplicationMain
    {
        private static void Main()
        {
            var firstMatrix = new double[,] { { 1, 3 }, { 5, 7 } };
            var secondMatrix = new double[,] { { 4, 2 }, { 1, 5 } };
            var multipliedMatricesResult = MatrixMultiplication(firstMatrix, secondMatrix);

            for (int row = 0; row < multipliedMatricesResult.GetLength(0); row++)
            {
                for (int col = 0; col < multipliedMatricesResult.GetLength(1); col++)
                {
                    Console.Write(multipliedMatricesResult[row, col] + " ");
                }

                Console.WriteLine();
            }
        }

        private static double[,] MatrixMultiplication(double[,] firstMatrix, double[,] secondMatrix)
        {
            if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
            {
                throw new ArgumentException("The matrices cannot be multiplied!");
            }

            var firstMatrixCols = firstMatrix.GetLength(1);
            var result = new double[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    for (int i = 0; i < firstMatrixCols; i++)
                    {
                        result[row, col] += firstMatrix[row, i] * secondMatrix[i, col];
                    }
                }
            }

            return result;
        }
    }
}