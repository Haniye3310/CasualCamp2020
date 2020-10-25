public class ArrayUtil
{
    public static T[,] TransformArrayToUnityCoordinate<T> (T[,] array) 
    {
        T[,] transformedArray = new T[array.GetLength(1), array.GetLength(0)];

        for (int i=0; i<array.GetLength(0); ++i)
            for (int j=0; j<array.GetLength(1); ++j)
            {
                transformedArray[i, j] = array[array.GetLength(0) - j - 1, i];
            }

        return transformedArray;
    }
}