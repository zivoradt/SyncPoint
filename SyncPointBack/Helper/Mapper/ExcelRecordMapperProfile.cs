using AutoMapper;
using SyncPointBack.DTO;
using SyncPointBack.Model.Excel;

namespace SyncPointBack.Helper.Mapper
{
    public class ExcelRecordMapperProfile : Profile
    {
        public ExcelRecordMapperProfile()
        {
            CreateMap<StaticPageCreationDto, StaticPageCreation>();

            CreateMap<StaticPageModificationDto, StaticPageModification>();

            CreateMap<PDRegistrationDto, PDRegistration>();

            CreateMap<PDModificationDto, PDModification>();

            CreateMap<PIMDto, PIM>();

            CreateMap<GNBDto, GNB>();

            CreateMap<CreateExcelRecordDto, ExcelRecord>();
        }
    }
}