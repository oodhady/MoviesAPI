using MoviesAPI.DTOs;

namespace MoviesAPI.Helper
{
    public class Helper : Profile
    {
        public Helper()
        {

            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
            //CreateMap<Movie, MovieDetailsDto>()
            //    .ForMember(z => z.GenraName, op => op.MapFrom(c => c.Genra.Name));
            //CreateMap<MovieDto, Movie>()
            //    .ForMember(src => src.Poster, opt => opt.Ignore())
            //    .ForMember(s => s.Rate, opt => opt.MapFrom(c => c.Rate))
            //    .ForMember(s => s.Year, opt => opt.MapFrom(c => c.Year))
            //    .ForMember(s => s.StrongLine, opt => opt.MapFrom(c => c.StrongLine))
            //    .ForMember(s => s.GenraId, opt => opt.MapFrom(c => c.GenraId))
            ;
        }
    }
}
