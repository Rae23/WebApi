using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Services.FileServices.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// Sorts numbers and stores them on the server
        /// </summary>
        /// <param name="numbers">Array of numbers to be sorted</param>
        /// <returns>Stored data id</returns>
        public Guid SortAndStoreNumbers(double[] numbers);

        /// <summary>
        /// Gets stored data by id
        /// </summary>
        /// <param name="id">file id (equivalent to fileName)</param>
        /// <returns>Data bytes</returns>
        public byte[] GetSortedNumbersDataById(Guid id);

        /// <summary>
        /// Gets last stored data
        /// </summary>
        /// <returns>Data bytes</returns>
        public byte[] GetLastSortedNumbersData();

        /// <summary>
        /// Gets list of all existing stored data ids
        /// </summary>
        /// <returns>List of stored data ids</returns>
        public IEnumerable<Guid> GetExistingIds();
    }
}
