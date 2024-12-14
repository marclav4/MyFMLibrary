using AutoMapper;
using RadioBrowser.Models;

namespace MyFMLibrary.DTOs
{
    public class RadioStationDTO
    {
        public Guid StationUuid { get; set; }
        public string Name { get; set; }
        public string Homepage { get; set; }
        public string Url { get; set; }
        public bool IsFavorite { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<StationInfo, RadioStationDTO>();
            }
        }
    }
}
