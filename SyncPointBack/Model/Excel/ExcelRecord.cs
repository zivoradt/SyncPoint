using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SyncPointBack.Model.Excel
{
    public class ExcelRecord
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string TicketId { get; set; }

        public StaticPageCreation? StaticPageCreation { get; set; }

        public StaticPageModification? StaticPageModification { get; set; }

        public PDRegistration? PDRegistration { get; set; }

        public PDModification? PDModification { get; set; }

        public PIM? PIM { get; set; }
        public GNB? GNB { get; set; }

        public string? Other { get; set; }
        public int? NumOfPages { get; set; }
        public int? NumOfChanges { get; set; }
        public string? Description { get; set; }
        public DateTime ProductionTime { get; set; }
    }

    public class StaticPageCreation
    {
        public int Id { get; set; }

        public ExcelRecord ExcelRecord { get; set; }

        public int ExcelRecordId { get; set; }

        public List<StaticPageCreationList> staticPageCreation { get; set; } = new List<StaticPageCreationList>();
    }

    public class StaticPageModification
    {
        public int Id { get; set; }

        public ExcelRecord ExcelRecord { get; set; }

        public int ExcelRecordId { get; set; }

        public List<StaticPageModificationList> staticPageModification { get; set; } = new List<StaticPageModificationList>();
    }

    public class PDRegistration
    {
        public int Id { get; set; }

        public ExcelRecord ExcelRecord { get; set; }

        public int ExcelRecordId { get; set; }

        public List<PDRegistrationList> pdRegistration { get; set; } = new List<PDRegistrationList>();
    }

    public class PDModification
    {
        public int Id { get; set; }

        public ExcelRecord ExcelRecord { get; set; }

        public int ExcelRecordId { get; set; }

        public List<PDModificationList> pdMOdification { get; set; } = new List<PDModificationList>();
    }

    public class PIM
    {
        public int Id { get; set; }
        public ExcelRecord ExcelRecord { get; set; }

        public int ExcelRecordId { get; set; }

        public List<PIMList> pim { get; set; } = new List<PIMList>();
    }

    public class GNB
    {
        public int Id { get; set; }

        public int ExcelRecordId { get; set; }
        public ExcelRecord ExcelRecord { get; set; }

        public List<GNBList> gnb { get; set; } = new List<GNBList>();
    }
}