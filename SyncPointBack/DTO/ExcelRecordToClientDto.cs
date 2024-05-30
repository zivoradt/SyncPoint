namespace SyncPointBack.DTO
{
    public class ExcelRecordToClientDto : ExcelRecordDto
    {
        public string RecordID { get; set; }
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
}