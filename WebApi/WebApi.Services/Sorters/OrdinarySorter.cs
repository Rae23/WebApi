using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Services.Sorters.Interfaces;

namespace WebApi.Services.Sorters
{
    /// <summary>
    /// This is just for comparisons with other sorters
    /// </summary>
    public class OrdinarySorter : ISorter
    {
        public double[] Sort(double[] numbersArray)
        {
            Array.Sort(numbersArray);
            return numbersArray;
        }
    }
}