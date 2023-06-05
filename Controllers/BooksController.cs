using Microsoft.AspNetCore.Mvc;
using VostrikovaLab.Models;

namespace VostrikovaLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController
    {
        private static List<BookModel> _memCache = new List<BookModel>();

        [HttpGet]
        public ActionResult<IEnumerable<BookModel>> Get()
        {
            return _memCache;
        }

        [HttpGet("{id}")]
        public ActionResult<BookModel> Get(int id)
        {
            if (_memCache.Count <= id) throw new IndexOutOfRangeException("Книга не найдена");

            return _memCache[id];
        }

        [HttpPost]
        public void Post([FromBody] BookModel value)
        {
            _memCache.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BookModel value)
        {
            if (_memCache.Count <= id) throw new IndexOutOfRangeException("Книга не найдена");

            _memCache[id] = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (_memCache.Count <= id) throw new IndexOutOfRangeException("Книга не найдена");

            _memCache.RemoveAt(id);
        }
    }
}
