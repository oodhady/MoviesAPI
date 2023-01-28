namespace MoviesAPI.Services
{
    public interface IGenraService
    {
        Task<IEnumerable<Genra>> GetAll();
        Task<Genra> GetById(byte id);
        Task<Genra> Add(Genra genra);
        Genra Update(Genra genra);
        Genra Delete(Genra genra);
        Task<bool> IsValidGenra(byte id);

    }
}
