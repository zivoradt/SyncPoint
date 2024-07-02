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
            CreateMap<PDRegistrationDto, PDRegistration>()
                .ForMember(dest => dest.pdRegistration, opt => opt.MapFrom(src => src.PDRegistration))
                .ReverseMap();
            CreateMap<PDModificationDto, PDModification>().ReverseMap();
            CreateMap<PIMDto, PIM>().ReverseMap();
            CreateMap<GNBDto, GNB>().ReverseMap();

            CreateMap<ExcelRecord, ExcelRecordToClientDto>()
            .ForMember(dest => dest.RecordID, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
            .ForMember(dest => dest.StaticPageCreation, opt => opt.MapFrom(src => src.StaticPageCreation))
            .ForMember(dest => dest.StaticPageModification, opt => opt.MapFrom(src => src.StaticPageModification))
            .ForMember(dest => dest.PDRegistration, opt => opt.MapFrom(src => src.PDRegistration))
            .ForMember(dest => dest.PDModification, opt => opt.MapFrom(src => src.PDModification))
            .ForMember(dest => dest.PIM, opt => opt.MapFrom(src => src.PIM))
            .ForMember(dest => dest.GNB, opt => opt.MapFrom(src => src.GNB))
            .ForMember(dest => dest.NumOfPages, opt => opt.MapFrom(src => src.NumOfPages))
            .ForMember(dest => dest.NumOfChanges, opt => opt.MapFrom(src => src.NumOfChanges))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Other, opt => opt.MapFrom(src => src.Other))
            .ForMember(dest => dest.ProductionTime, opt => opt.MapFrom(src => (src.EndDate - src.StartDate).TotalHours));

            // Mapping from ExcelRecordToClientDto to ExcelRecord
            CreateMap<ExcelRecordToClientDto, ExcelRecord>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.RecordID)))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
                .ForMember(dest => dest.StaticPageCreation, opt => opt.MapFrom(src => src.StaticPageCreation))
                .ForMember(dest => dest.StaticPageModification, opt => opt.MapFrom(src => src.StaticPageModification))
                .ForMember(dest => dest.PDRegistration, opt => opt.MapFrom(src => src.PDRegistration))
                .ForMember(dest => dest.PDModification, opt => opt.MapFrom(src => src.PDModification))
                .ForMember(dest => dest.PIM, opt => opt.MapFrom(src => src.PIM))
                .ForMember(dest => dest.GNB, opt => opt.MapFrom(src => src.GNB))
                .ForMember(dest => dest.NumOfPages, opt => opt.MapFrom(src => src.NumOfPages))
                .ForMember(dest => dest.NumOfChanges, opt => opt.MapFrom(src => src.NumOfChanges))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Other, opt => opt.MapFrom(src => src.Other))
                .ForMember(dest => dest.ProductionTime, opt => opt.Ignore());

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

                    .ForMember(dest => dest.ProductionTime, opt => opt.MapFrom(src => (src.EndDate - src.StartDate).TotalHours));

            CreateMap<ExcelRecord, ExcelImportDto>()

                .ForMember(dest => dest.Date, src => src.MapFrom(src => src.StartDate.ToString("yyyy-MM-dd")))

                .ForMember(dest => dest.StartTime, src => src.MapFrom(src => src.StartDate))

                .ForMember(dest => dest.TicketID, src => src.MapFrom(src => src.TicketId))

                .ForMember(dest => dest.StaticPageCreation, src => src.MapFrom(src => src.StaticPageCreation != null ? ConvertListToString(src.StaticPageCreation.staticPageCreation) : ""))

                .ForMember(dest => dest.StaticPageModification, src => src.MapFrom(src => src.StaticPageModification != null ? ConvertListToString(src.StaticPageModification.staticPageModification) : ""))

                .ForMember(dest => dest.PDRegistration, src => src.MapFrom(src => src.PDRegistration != null ? ConvertListToString(src.PDRegistration.pdRegistration) : ""))

                .ForMember(dest => dest.PDModification, src => src.MapFrom(src => src.PDModification != null ? ConvertListToString(src.PDModification.pdMOdification) : ""))

                .ForMember(dest => dest.PIM, src => src.MapFrom(src => src.PIM != null ? ConvertListToString(src.PIM.pim) : ""))

                .ForMember(dest => dest.GNB, src => src.MapFrom(src => src.GNB != null ? ConvertListToString(src.GNB.gnb) : ""))

                .ForMember(dest => dest.Other, src => src.MapFrom(src => src.Other))

                .ForMember(dest => dest.NoOfPages, src => src.MapFrom(src => src.NumOfPages))

                .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Description))

                .ForMember(dest => dest.FinishTime, src => src.MapFrom(src => src.EndDate))

                .ForMember(dest => dest.ProductionTime, opt => opt.MapFrom<ProductionTimeResolver>());
        }

        public class ProductionTimeResolver : IValueResolver<ExcelRecord, ExcelImportDto, string>
        {
            public string Resolve(ExcelRecord source, ExcelImportDto destination, string destMember, ResolutionContext context)
            {
                var timeSpan = source.EndDate - source.StartDate;
                return string.Format("{0:D2}:{1:D2}", (int)timeSpan.TotalHours, timeSpan.Minutes);
            }
        }

        private string ConvertListToString<T>(List<T> list)
        {
            return string.Join(", ", list);
        }
    }
}