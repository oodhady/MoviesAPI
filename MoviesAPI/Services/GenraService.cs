using MoviesAPI.DTOs;

namespace MoviesAPI.Services
{
    public class GenraService : IGenraService
    {
        private readonly ApplicationDbContext _context;
        public GenraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genra>> GetAll()
        {
            var genras = await _context.Genras.OrderBy(o => o.Name).ToListAsync();
            return genras;
        }

        public async Task<Genra> GetById(byte Id)
        {
            var genra = await _context.Genras.FirstOrDefaultAsync(o => o.Id == Id);
            return genra;
        }

        public async Task<Genra> Add(Genra genra)
        {
            await _context.AddAsync(genra);
            _context.SaveChanges();
            return genra;
        }

        public Genra Update(Genra genra)
        {
            _context.Update(genra);
            _context.SaveChanges();
            return genra;
        }

        public Genra Delete(Genra genra)
        {
            _context.Remove(genra);
            _context.SaveChanges();
            return genra;
        }

        public Task<bool> IsValidGenra(byte id)
        {
           return _context.Genras.AnyAsync(x => x.Id == id);
        }
    }
}
