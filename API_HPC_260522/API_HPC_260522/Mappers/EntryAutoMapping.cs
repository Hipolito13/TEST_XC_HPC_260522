using API_HPC_260522.Models.Dtos;
using API_HPC_260522.Models.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HPC_260522.Mappers
{
    public class EntryAutoMapping: Profile
    {
        public EntryAutoMapping()
        {
            MapToEntry();
        }

        private void MapToEntry()
        {
            CreateMap<EntryResponse, DtoEntry>().ReverseMap()
                .ForMember(dest => dest.ApiName, a => a.MapFrom(src => src.API))
                .ForMember(dest => dest.AuthType, a => a.MapFrom(src => src.Auth))
                .ForMember(dest => dest.IsHttp, a => a.MapFrom(src => src.HTTPS))
                .ForMember(dest => dest.Description, a => a.MapFrom(src => src.Description))
                .ForMember(dest => dest.Cors, a => a.MapFrom(src => src.Cors))
                .ForMember(dest => dest.CategoryName, a => a.MapFrom(src => src.Category))
                .ForMember(dest => dest.Link, a => a.MapFrom(src => src.Link));
        }
    }
}
