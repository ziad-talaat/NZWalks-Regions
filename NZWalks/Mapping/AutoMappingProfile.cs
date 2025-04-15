using AutoMapper;
using REPO.Core.DTO;
using REPO.Core.Models;

namespace NZ.Walks.Mapping
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();
            CreateMap<Walk,WalkRequestDTO>().ReverseMap();
         //   CreateMap<Walk,WalkResponseDTO>().ReverseMap();

            CreateMap<Walk, WalkResponseDTO>()
                .ForMember(x => x.DifficultyName, x => x.MapFrom(x => x.Difficulty.Name))
                .ForMember(x => x.RegionName, x => x.MapFrom(x => x.Region.Name)).ReverseMap();
                
        }
    }
}
