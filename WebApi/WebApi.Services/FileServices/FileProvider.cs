using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Services.FileServices.Interfaces;

namespace WebApi.Services.FileServices
{
    public class FileProvider : IDataProvider
    {
        private const string fileNameFormat = "{0}{1}.txt";
        private const string directoryPath = @"C:\temp\";
        private readonly DirectoryInfo filesDirectory;

        public FileProvider()
        {
            filesDirectory = new DirectoryInfo(directoryPath);
        }

        public IEnumerable<Guid> GetDataIds()
        {
            return filesDirectory.GetFiles().Select(x => Guid.Parse(Path.GetFileNameWithoutExtension(x.FullName)));
        }

        public Guid StoreData(string data)
        {
            var id = Guid.NewGuid();
            string filePath = string.Format(fileNameFormat, filesDirectory, id);
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(data);
            }

            return id;
        }

        public byte[] GetData(Guid? id = null)
        {
            FileInfo file = id.HasValue
                ? filesDirectory.GetFiles().Where(x => x.Name == string.Format(fileNameFormat, string.Empty, id.Value.ToString())).FirstOrDefault()
                : filesDirectory.GetFiles().OrderByDescending(x => x.CreationTime).FirstOrDefault();

            return file != null ? File.ReadAllBytes(file.FullName) : null;
        }
    }
}
