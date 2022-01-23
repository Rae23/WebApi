namespace WebApi.Services.Extensions
{
    public static class ArrayExtensions
    {
        public static void SwapValues(this double[] numbersArray, int firstIndex, int secondIndex)
        {
            var swappedValue = numbersArray[firstIndex];
            numbersArray[firstIndex] = numbersArray[secondIndex];
            numbersArray[secondIndex] = swappedValue;
        }
    }
}
