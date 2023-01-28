
namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenrasController : ControllerBase
    {
        private readonly IGenraService _genraService;

        public GenrasController(IGenraService genraService)
        {
            _genraService = genraService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genras = await _genraService.GetAll();

            return Ok(genras);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenraDto genraDto)
        {
            var genra = new Genra() { Name = genraDto.Name };
            await _genraService.Add(genra);
            return Ok(genra);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] GenraDto genraDto)
        {
            var genra = await _genraService.GetById((byte)id);
            if (genra is null)
            {
                return NotFound($"No Genra was found with Id :{id}");
            }
            genra.Name = genraDto.Name;
            _genraService.Update(genra);
            return Ok(genra);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genra = await _genraService.GetById((byte)id);
            if (genra is null)
            {
                return NotFound($"No Genra was found with Id :{id}");
            }
            _genraService.Delete(genra);
            return Ok(genra);
        }
    }
}
