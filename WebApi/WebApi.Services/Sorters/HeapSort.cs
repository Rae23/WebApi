using System;
using WebApi.Services.Extensions;
using WebApi.Services.Sorters.Interfaces;

namespace WebApi.Services.Sorters
{
    public class HeapSort : ISorter
    {
        public double[] Sort(double[] numbersArray)
        {
            BuildHeap(numbersArray);
            int j = numbersArray.Length - 1;

            while (j > 0)
            {
                numbersArray.SwapValues(0, j);
                j--;
                SiftDown(numbersArray, 0, j);
            }

            return numbersArray;
        }

        private void BuildHeap(double[] numbersArray)
        {
            int i = Convert.ToInt32(Math.Floor((double)((numbersArray.Length - 1) / 2)));

            while(i >= 0)
            {
                SiftDown(numbersArray, i, numbersArray.Length - 1);
                i--;
            }
        }

        private void SiftDown(double[] numbersArray, int startIndex, int endIndex)
        {
            int root = startIndex;
            var calculateLeftChild = new Func<int, int>(x => 2 * x + 1);

            while (calculateLeftChild(root) <= endIndex)
            {
                int child = calculateLeftChild(root);
                int swap = root;

                if (numbersArray[swap] < numbersArray[child])
                {
                    swap = child;
                }
                if ((child + 1 <= endIndex) && (numbersArray[swap] < numbersArray[child + 1]))
                {
                    swap = child + 1;
                }

                if (swap == root)
                {
                    break;
                }
                else
                {
                    numbersArray.SwapValues(root, swap);
                    root = swap;
                }
            }
        }
    }
}