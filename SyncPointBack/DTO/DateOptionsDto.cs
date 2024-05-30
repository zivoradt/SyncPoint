namespace SyncPointBack.DTO
{
    public class DateOptionsDto
    {
        private int defaultDate { get; }

        private DateOptionsDto()
        {
            this.defaultDate = DateTime.Now.Month;
        }

        public int DefaultDate
        { get { return this.defaultDate; } }
    }
}