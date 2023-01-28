namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(byte genraId=0);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie Movie);
        Movie Update(Movie Movie);
        Movie Delete(Movie Movie);
    }
}
