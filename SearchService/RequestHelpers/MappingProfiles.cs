using AutoMapper;
using Contracts;
using SearchService.Dto;

namespace SearchService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SalesCreated, Product>();
            CreateMap<SalesUpdated, Product>();
            
        }
    }
}
