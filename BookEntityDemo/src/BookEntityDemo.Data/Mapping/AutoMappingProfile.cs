using AutoMapper;
using BookEntityDemo.Data.Models;
using BookEntityDemo.Models.Response;

namespace BookEntityDemo.Data.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Books, BookResponseModel>();
        }
    }
}
