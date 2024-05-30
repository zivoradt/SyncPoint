using AutoMapper;
using SyncPointBack.Model.Excel;

namespace SyncPointBack.DTO.Mapper
{
    public class MapperExcelRecord : Profile
    {
        public MapperExcelRecord()
        {
            CreateMap<StaticPageCreationDto, StaticPageCreation>().ReverseMap();
            CreateMap<StaticPageModificationDto, StaticPageModification>().ReverseMap();
            CreateMap<PDRegistrationDto, PDRegistration>().ReverseMap();
            CreateMap<PDModificationDto, PDModification>().ReverseMap();
            CreateMap<PIMDto, PIM>().ReverseMap();
            CreateMap<GNBDto, GNB>().ReverseMap();

            CreateMap<ExcelRecordToClientDto, ExcelRecord>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecordID))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
                .ForMember(dest => dest.StaticPageCreation, opt => opt.MapFrom(src => src.StaticPageCreation))
                .ForMember(dest => dest.StaticPageModification, opt => opt.MapFrom(src => src.StaticPageModification))
                .ForMember(dest => dest.PDRegistration, opt => opt.MapFrom(src => src.PDRegistration))
                .ForMember(dest => dest.PDModification, opt => opt.MapFrom(src => src.PDModification))
                .ForMember(dest => dest.PIM, opt => opt.MapFrom(src => src.PIM))
                .ForMember(dest => dest.GNB, opt => opt.MapFrom(src => src.GNB))
                .ForMember(dest => dest.NumOfChanges, opt => opt.MapFrom(src => src.NumOfChanges))
                .ForMember(dest => dest.NumOfPages, opt => opt.MapFrom(src => src.NumOfPages))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ProductionTime, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Other, opt => opt.MapFrom(src => src.Other)).ReverseMap();

            CreateMap<CreateExcelRecordDto, ExcelRecord>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
                .ForMember(dest => dest.StaticPageCreation, opt => opt.MapFrom(src => src.StaticPageCreation))
                .ForMember(dest => dest.StaticPageModification, opt => opt.MapFrom(src => src.StaticPageModification))
                .ForMember(dest => dest.PDRegistration, opt => opt.MapFrom(src => src.PDRegistration))
                .ForMember(dest => dest.PDModification, opt => opt.MapFrom(src => src.PDModification))
                .ForMember(dest => dest.PIM, opt => opt.MapFrom(src => src.PIM))
                .ForMember(dest => dest.GNB, opt => opt.MapFrom(src => src.GNB))
                .ForMember(dest => dest.NumOfChanges, opt => opt.MapFrom(src => src.NumOfChanges))
                .ForMember(dest => dest.NumOfPages, opt => opt.MapFrom(src => src.NumOfPages))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ProductionTime, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Other, opt => opt.MapFrom(src => src.Other));
        }
    }
}