using WebApi.Services.Extensions;
using WebApi.Services.Sorters.Interfaces;

namespace WebApi.Services.Sorters
{
    public class QuickSort : ISorter
    {
        public double[] Sort(double[] numbersArray)
        {
            RecursiveQuickSort(numbersArray, 0, numbersArray.Length - 1);
            return numbersArray;
        }

        private void RecursiveQuickSort(double[] numbersArray, int lowerIndex, int higherIndex)
        {
            if (lowerIndex < higherIndex)
            {
                int partitioningIndex = Partition(numbersArray, lowerIndex, higherIndex);

                RecursiveQuickSort(numbersArray, lowerIndex, partitioningIndex - 1);
                RecursiveQuickSort(numbersArray, partitioningIndex + 1, higherIndex);
            }
        }

        private int Partition(double[] numbersArray, int lowerIndex, int higherIndex)
        {
            double pivot = numbersArray[higherIndex];
            int i = lowerIndex - 1;

            for (int h = lowerIndex; h <= higherIndex - 1; h++)
            {
                if (numbersArray[h] < pivot)
                {
                    i++;
                    numbersArray.SwapValues(i, h);
                }
            }
            numbersArray.SwapValues(i + 1, higherIndex);
            return (i + 1);
        }
    }
}
