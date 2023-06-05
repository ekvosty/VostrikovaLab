using VostrikovaLab.Interfaces;
using VostrikovaLab.Models;

namespace VostrikovaLab
{
    public class MemCache : IStorage<BookModel>
    {
        private object _sync = new object();
        private List<BookModel> _memCache = new List<BookModel>();
        public BookModel this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new Exception($"Нет книги с данным {id}");

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new Exception("Нельзя получить книгу по пустому id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<BookModel> All => _memCache.Select(x => x).ToList();

        public void Add(BookModel value)
        {
            if (value.Id != Guid.Empty) throw new Exception($"Нельзя добавить книгу с заданным id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
    }
}
