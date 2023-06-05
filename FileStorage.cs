using Newtonsoft.Json;
using VostrikovaLab.Interfaces;
using VostrikovaLab.Models;

namespace VostrikovaLab
{
    public class FileStorage : MemCache, IStorage<BookModel>
    {
        private Timer _timer;

        public string FileName { get; }
        public int FlushPeriod { get; }

        public FileStorage(string fileName, int flushPeriod)
        {
            FileName = fileName;
            FlushPeriod = flushPeriod;

            Load();

            _timer = new Timer((x) => Flush(), null, flushPeriod, flushPeriod);
        }

        private void Load()
        {
            if (File.Exists(FileName))
            {
                var allLines = File.ReadAllText(FileName);

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<List<BookModel>>(allLines);

                    if (deserialized != null)
                    {
                        foreach (var labData in deserialized)
                        {
                            base[labData.Id] = labData;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new FileLoadException($"Не получилось загрузить файл {FileName}:\r\n{ex.Message}");
                }
            }
        }

        private void Flush()
        {
            var serializedContents = JsonConvert.SerializeObject(All);

            File.WriteAllText(FileName, serializedContents);
        }
    }

}
