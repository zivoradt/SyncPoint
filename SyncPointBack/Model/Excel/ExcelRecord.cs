namespace SyncPointBack.Model.Excel
{
    public class ExcelRecord
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TicketId { get; set; }

        public int StaticPageCreationId { get; set; }

        public StaticPageCreation StaticPageCreation { get; set; }

        public int StaticPageModictionId { get; set; }

        public StaticPageModification StaticPageModification { get; set; }

        public int PDRegistrationId { get; set; }

        public PDRegistration PDRegistration { get; set; }

        public int PDModificationId { get; set; }

        public PDModification PDModification { get; set; }

        public int PIMId { get; set; }

        public PIM PIM { get; set; }

        public int GNBId { get; set; }

        public GNB GNB { get; set; }

        public string Other { get; set; }

        public int NumOfPages { get; set; }

        public int NumOfChanges { get; set; }

        public string Description { get; set; }

        public DateTime ProductionTime { get; set; }
    }

    public class StaticPageCreation
    {
        public int Id { get; set; }

        public int ExcelRecordId { get; set; }

        public List<StaticPageCreationList> staticPageCreation { get; set; } = new List<StaticPageCreationList>();
    }

    public class StaticPageModification
    {
        public int Id { get; set; }

        public int ExcelRecordId { get; set; }

        public List<StaticPageModificationList> staticPageModification { get; set; } = new List<StaticPageModificationList>();
    }

    public class PDRegistration
    {
        public int Id { get; set; }

        public int ExcelRecordId { get; set; }

        public List<PDRegistrationList> staticRegistration { get; set; } = new List<PDRegistrationList>();
    }

    public class PDModification
    {
        public int Id { get; set; }

        public int ExcelRecordId { get; set; }

        public List<PDModificationList> staticRegistration { get; set; } = new List<PDModificationList>();
    }

    public class PIM
    {
        public int Id { get; set; }
        public int ExcelRecordId { get; set; }

        public List<PIMList> staticRegistration { get; set; } = new List<PIMList>();
    }

    public class GNB
    {
        public int Id { get; set; }
        public int ExcelRecordId { get; set; }

        public List<GNBList> staticRegistration { get; set; } = new List<GNBList>();
    }
}