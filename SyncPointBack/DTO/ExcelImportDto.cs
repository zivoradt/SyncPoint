namespace SyncPointBack.DTO
{
    public class ExcelImportDto : ExcelRecordDto
    {
        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public string TicketID { get; set; }

        public string? StaticPageCreation { get; set; }

        public string? StaticPageModification { get; set; }

        public string PDRegistration { get; set; }

        public string? PDModification { get; set; }

        public string? PIM { get; set; }

        public string? GNB { get; set; }

        public string Other { get; set; }

        public int NoOfPages { get; set; }

        public string Description { get; set; }

        public DateTime FinishTime { get; set; }

        public string ProductionTime { get; set; }
    }
}