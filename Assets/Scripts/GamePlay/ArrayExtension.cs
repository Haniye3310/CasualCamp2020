public static class ArrayExtension
{
    public static T GetTransformUnityCoord<T>(this T[,] array, int xUnity, int yUnity) 
    {
        int yArray = xUnity;
        int xArray = array.GetLength(0) - yUnity - 1;
        return array[xArray, yArray];
    }

    public static void SetTransformUnityCoord<T>(this T[,] array, int xUnity, int yUnity, T value)
    {
        int yArray = xUnity;
        int xArray = array.GetLength(0) - yUnity - 1;
        array[xArray, yArray] = value;
    }
}