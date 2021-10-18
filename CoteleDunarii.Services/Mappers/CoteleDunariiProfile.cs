using AutoMapper;
using CoteleDunarii.Data.Models;
using CoteleDunarii.Services.Dtos;

namespace CoteleDunarii.Services.Mappers
{
    public class CoteleDunariiProfile : Profile
    {
        public CoteleDunariiProfile()
        {
            // entity to model
            CreateMap<WaterInfo, WaterInfoDto>(MemberList.Destination);
            CreateMap<WaterEstimations, WaterEstimationDto>(MemberList.Destination);
            CreateMap<City, CityDto>(MemberList.Destination);

            // model to entity
            CreateMap<CityDto, City>(MemberList.Source);
            CreateMap<WaterInfoDto, WaterInfo>(MemberList.Source);
            CreateMap<WaterEstimationDto, WaterEstimations>(MemberList.Source);
        }
    }
}
