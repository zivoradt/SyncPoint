using Microsoft.Office.Interop.Excel;

namespace SyncPointBack.Services.Excel
{
    public abstract class IExcelApp
    {
        protected Application _xapp;

        public void DownloadExcel()
        { }
    }
}