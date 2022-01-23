using WebApi.Services.Extensions;
using WebApi.Services.Sorters.Interfaces;

namespace WebApi.Services.Sorters
{
    public class BubbleSort : ISorter
    {
        public double[] Sort(double[] numbersArray)
        {
            bool wasSwapped = true;

            while (wasSwapped)
            {
                wasSwapped = false;
                for (int i = 0; i < numbersArray.Length - 1; i++)
                {
                    if (numbersArray[i] > numbersArray[i + 1])
                    {
                        numbersArray.SwapValues(i, i + 1);
                        wasSwapped = true;
                    }
                }
            }

            return numbersArray;
        }
    }
}
