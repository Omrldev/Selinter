﻿using AutoMapper;
using SalesService.Dtos;
using SalesService.Enitities;

namespace SalesService.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sale, SaleDto>().IncludeMembers(x => x.Product);
            CreateMap<Product, SaleDto>();
            CreateMap<CreateSaleDto, Sale>()
                .ForMember(d => d.Product, opt => opt.MapFrom(p => p));
            CreateMap<CreateSaleDto, Product>();
        }
    }
}
