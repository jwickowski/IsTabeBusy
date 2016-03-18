using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace IsTableBusy.Device.Core.Logic
{
    public sealed class ConfigStorageImp : ConfigStorage
    {
        private string fileName;
        public ConfigStorageImp(string fileName)
        {
            this.fileName = fileName;
        }

        public void Save(ConfigData data)
        {
            var saveTask = SaveAsync(this.fileName, data);
            saveTask.Wait();
        }

        private static async Task SaveAsync(string FileName, ConfigData _Data) 
        {
            MemoryStream _MemoryStream = new MemoryStream();
            
            var Serializer = new System.Xml.Serialization.XmlSerializer(typeof(ConfigData));
            Serializer.Serialize(_MemoryStream, _Data);

            Task.WaitAll();

            StorageFile _File = await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

            using (Stream fileStream = await _File.OpenStreamForWriteAsync())
            {
                _MemoryStream.Seek(0, SeekOrigin.Begin);
                await _MemoryStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Dispose();
            }
        }

        public ConfigData Load()
        {
            var saveTask = LoadAsync(this.fileName);
            return saveTask.Result;
        }

        private static async Task<ConfigData> LoadAsync(string FileName)
        {
            StorageFolder _Folder = ApplicationData.Current.LocalFolder;
            StorageFile _File;
            ConfigData Result;

            try
            {
                Task.WaitAll();
                _File = await _Folder.GetFileAsync(FileName);

                using (Stream stream = await _File.OpenStreamForReadAsync())
                {
                    var Serializer = new System.Xml.Serialization.XmlSerializer(typeof(ConfigData));

                    Result = (ConfigData)Serializer.Deserialize(stream);
                }
                return Result;
            }
            catch (Exception ex)
            {
                return new ConfigData();
            }
        }
    }
}
