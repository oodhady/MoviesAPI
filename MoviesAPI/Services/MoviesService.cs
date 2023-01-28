using MoviesAPI.Model;

namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;
        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAll(byte genraId = 0)
        {
            return await _context.Movies
                   .Where(x => x.GenraId == genraId || genraId == 0)
                   .OrderByDescending(r => r.Rate)
                   .Include(g => g.Genra)
                   .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return _context.Movies.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Movie> Add(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public Movie Update(Movie Movie)
        {
            _context.Update(Movie);
            _context.SaveChanges();
            return Movie;
        }

        public Movie Delete(Movie Movie)
        {
            _context.Remove(Movie);
            _context.SaveChanges();
            return Movie;
        }
    }
}
