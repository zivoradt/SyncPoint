using SyncPointBack.DTo;

namespace SyncPointBack.DTO
{
    public class CreateExcelRecordDto
    {
        public string UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TicketId { get; set; }

        public StaticPageCreationDto? StaticPageCreation { get; set; }

        public StaticPageModificationDto? StaticPageModification { get; set; }

        public PDRegistrationDto? PDRegistration { get; set; }

        public PDModificationDto? PDModification { get; set; }

        public PIMDto? PIM { get; set; }

        public GNBDto? GNB { get; set; }

        public int? NumOfPages { get; set; }

        public int? NumOfChanges { get; set; }

        public string? Description { get; set; }

        public string? Other { get; set; }

        public DateTime ProductionTime { get; set; }
    }

    public class StaticPageCreationDto
    {
        public List<StaticPageCreationListDto> StaticPageCreation { get; set; } = new List<StaticPageCreationListDto>();
    }

    public class StaticPageModificationDto
    {
        public List<StaticPageModificationListDto> StaticPageModification { get; set; } = new List<StaticPageModificationListDto>();
    }

    public class PDRegistrationDto
    {
        public List<PDRegistrationListDto> PDRegistration { get; set; } = new List<PDRegistrationListDto>();
    }

    public class PDModificationDto
    {
        public List<PDModificationListDto> PDModification { get; set; } = new List<PDModificationListDto>();
    }

    public class PIMDto
    {
        public List<PIMListDto> PIM { get; set; } = new List<PIMListDto>();
    }

    public class GNBDto
    {
        public List<GNBListDto> GNB { get; set; } = new List<GNBListDto>();
    }
}