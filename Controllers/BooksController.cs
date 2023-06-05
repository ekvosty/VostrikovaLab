using Microsoft.AspNetCore.Mvc;
using VostrikovaLab.Interfaces;
using VostrikovaLab.Models;

namespace VostrikovaLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private static IStorage<BookModel> _memCache = new MemCache();
        [HttpGet]
        public ActionResult<IEnumerable<BookModel>> Get()
        {
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<BookModel> Get(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("Книги с такой id нет");

            return _memCache[id];
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookModel value)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            _memCache.Add(value);


            return Ok($"{value.ToString()} добавлено");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] BookModel value)
        {
            if (!_memCache.Has(id)) return NotFound("Книги с такой id нет");

            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            var previousValue = _memCache[id];
            _memCache[id] = value;

            return Ok($"{previousValue.ToString()} было изменено на {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("Книги с такой id нет");

            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} была успешно удалена!");

        }
    }
}
