using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Model;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMoviesService _moviesService;
        private readonly IGenraService _genraService;
        private new List<string> _allowedExtention = new List<string> { ".jpg", ".png" };
        private long _maxPosterSize = 2097152;
        /**********************************/
        private bool CheckFile(IFormFile file, out string? type)
        {
            var filename = file.FileName.ToLower();
            if (!_allowedExtention.Contains(Path.GetExtension(filename)))
            {
                type = "Extention";
                return false;
            }
            if (file.Length > _maxPosterSize)
            {
                type = "Length";
                return false;
            }
            type = "";
            return true;
        }

        /************************************/
        public MoviesController(IMoviesService moviesService, IGenraService genraService, IMapper mapper)
        {
            _moviesService = moviesService;
            _genraService = genraService;
            _mapper = mapper;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {

            var data = await _moviesService.GetAll();
            var dtoMovie = _mapper.Map<IEnumerable<MovieDetailsDto>>(data);
            return Ok(dtoMovie);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
        }


        [HttpGet("GetByGenraId")]
        public async Task<IActionResult> GetByGenraIdAsync(byte genraId)
        {
            var movies = await _moviesService.GetAll(genraId);

            var dtoMovie = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(dtoMovie);

        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto movieDto)
        {
            if (movieDto.Poster is null)
            {
                return BadRequest("Poster Is Requierd");
            }
            string errorType = "";
            var check = CheckFile(movieDto.Poster, out errorType);
            if (!check)
            {
                switch (errorType)
                {
                    case "Length":
                        return BadRequest("Max Size Is 2 Mega");
                    case "Extention":
                        return BadRequest("Invalid Extention");
                }
            }
            var isValidGenra = await _genraService.IsValidGenra(movieDto.GenraId);
            if (!isValidGenra)
            {
                return BadRequest("Invalid Genra Id");
            }
            using var dataStream = new MemoryStream();

            await movieDto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(movieDto);
            movie.Poster = dataStream.ToArray();

            await _moviesService.Add(movie);

            return Ok(movie);
            //return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
        {

            var movie = await _moviesService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            var isValidGenra = await _genraService.IsValidGenra(movieDto.GenraId);
            if (!isValidGenra)
            {
                return BadRequest("Invalid Genra Id");
            }
            if (movieDto.Poster != null)
            {
                string errorType = "";
                var check = CheckFile(movieDto.Poster, out errorType);
                if (!check)
                {
                    switch (errorType)
                    {
                        case "Length":
                            return BadRequest("Max Size Is 2 Mega");
                        case "Extention":
                            return BadRequest("Invalid Extention");
                    }
                }

                using var dataStream = new MemoryStream();
                await movieDto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }
            movie.Title = movieDto.Title;
            movie.Year = movieDto.Year;
            movie.StrongLine = movieDto.StrongLine;
            movie.Rate = movieDto.Rate;
            movie.GenraId = movieDto.GenraId;

            _moviesService.Update(movie);

            return NoContent();
        }


        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            _moviesService.Delete(movie);

            return NoContent();
        }

    }
}
