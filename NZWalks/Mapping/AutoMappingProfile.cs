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
        }
    }
}
