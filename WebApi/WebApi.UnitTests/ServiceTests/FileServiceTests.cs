using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebApi.Services.FileServices;
using WebApi.Services.FileServices.Interfaces;
using WebApi.Services.Sorters.Interfaces;

namespace WebApi.UnitTests.ServiceTests
{
    public class FileServiceTests
    {
        private Mock<IDataProvider> fileProviderMock;
        private Guid createdFileId;
        private double[] sortingData; 
        private byte[] storedData;
        private IEnumerable<Guid> dataIds;

        [SetUp]
        public void Setup()
        {
            fileProviderMock = new Mock<IDataProvider>(MockBehavior.Loose);
            createdFileId = Guid.NewGuid();
            sortingData = new double[] { 1, 5, -4 };
            storedData = new byte[] { 0, 100, 255 };
            dataIds = new List<Guid>() { Guid.NewGuid(), createdFileId };

            fileProviderMock.Setup(x => x.GetDataIds())
                .Returns(dataIds);
            fileProviderMock.Setup(x => x.GetData(It.IsAny<Guid?>()))
                .Returns(storedData);
            fileProviderMock.Setup(x => x.StoreData(It.IsAny<string>()))
                .Returns(createdFileId);
        }

        [Test]
        public void SortAndStoreNumbers()
        {
            StorageService storageService = new StorageService(fileProviderMock.Object);
            var returnValue = storageService.SortAndStoreNumbers(sortingData);

            fileProviderMock.Verify(x => x.StoreData(It.IsAny<string>()), Times.Once);

            Assert.AreEqual(createdFileId, returnValue);
        }

        [Test]
        public void GetSortedNumbersDataById()
        {
            StorageService storageService = new StorageService(fileProviderMock.Object);
            var returnValue = storageService.GetSortedNumbersDataById(createdFileId);

            fileProviderMock.Verify(x => x.GetData(It.IsAny<Guid?>()), Times.Once);
            Assert.AreEqual(storedData, returnValue);
        }

        [Test]
        public void GetLastSortedNumbersData()
        {
            StorageService storageService = new StorageService(fileProviderMock.Object);
            var returnValue = storageService.GetLastSortedNumbersData();

            fileProviderMock.Verify(x => x.GetData(null), Times.Once);
            Assert.AreEqual(storedData, returnValue);
        }

        [Test]
        public void GetExistingIds()
        {
            StorageService storageService = new StorageService(fileProviderMock.Object);
            var returnValue = storageService.GetExistingIds();

            fileProviderMock.Verify(x => x.GetDataIds(), Times.Once);
            Assert.AreEqual(dataIds, returnValue);
        }
    }
}