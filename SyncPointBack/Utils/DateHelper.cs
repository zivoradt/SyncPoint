namespace SyncPointBack.Utils
{
    public static class DateHelper
    {
        public static int Today(this DateTime d)
        {
            return d.Date.Day;
        }
    }
}