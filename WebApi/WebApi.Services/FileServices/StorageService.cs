﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Services.FileServices.Interfaces;
using WebApi.Services.Sorters;
using WebApi.Services.Sorters.Interfaces;

namespace WebApi.Services.FileServices
{
    public class StorageService : IStorageService
    {
        private readonly IDataProvider fileProvider;      

        public StorageService(IDataProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public Guid SortAndStoreNumbers(double[] numbers)
        {
            var builder = new StringBuilder();
            builder.AppendLine(GetSortedNumbersAndExecutionTime<OrdinarySorter>(numbers));
            builder.AppendLine(GetSortedNumbersAndExecutionTime<BubbleSort>(numbers));
            builder.AppendLine(GetSortedNumbersAndExecutionTime<QuickSort>(numbers));
            builder.AppendLine(GetSortedNumbersAndExecutionTime<HeapSort>(numbers));

            return fileProvider.StoreData(builder.ToString());
        }

        public byte[] GetSortedNumbersDataById(Guid id)
        {
            return fileProvider.GetData(id);
        }

        public byte[] GetLastSortedNumbersData()
        {
            return fileProvider.GetData();
        }

        public IEnumerable<Guid> GetExistingIds()
        {
            return fileProvider.GetDataIds();
        }

        private string GetSortedNumbersAndExecutionTime<TSorter>(double[] numbers)
            where TSorter : ISorter, new()
        {
            var builder = new StringBuilder();
            var watch = new Stopwatch();
            var sorter = new TSorter();

            builder.AppendLine(sorter.SorterName + " Start");

            watch.Start();
            double[] numbersTemp = new double[numbers.Length];
            Array.Copy(numbers, numbersTemp, numbers.Length); ///ref type, prepare for reuse for other sorting algorithms
            builder.AppendLine(string.Join(", ", sorter.Sort(numbersTemp)));
            watch.Stop();

            builder.AppendLine(sorter.SorterName + " End");
            builder.AppendLine(string.Format("Time ellapsed: {0} ticks", watch.ElapsedTicks.ToString()));
            builder.AppendLine();

            return builder.ToString();
        }
    }
}
