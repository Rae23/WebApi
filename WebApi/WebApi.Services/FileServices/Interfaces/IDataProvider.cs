using System;
using System.Collections.Generic;

namespace WebApi.Services.FileServices.Interfaces
{
    public interface IDataProvider
    {
        /// <summary>
        /// Returns a collection of all stored data ids
        /// </summary>
        /// <returns>Array of stored data ids</returns>
        public IEnumerable<Guid> GetDataIds();

        /// <summary>
        /// Stores data and returns it's unique id
        /// </summary>
        /// <param name="data">data to store</param>
        /// <returns>id of newly stored data</returns>
        public Guid StoreData(string data);

        /// <summary>
        /// Finds the stored data by id or the last data stored if id is null
        /// </summary>
        /// <param name="id">Id of stored data</param>
        /// <returns>Stored data</returns>
        public byte[] GetData(Guid? id = null);
    }
}
