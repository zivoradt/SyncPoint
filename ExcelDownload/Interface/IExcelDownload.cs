using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDownload.Interface
{
    public interface IExcelDownload
    {
        MemoryStream GenerateExcelFile<T>(string fileName, IEnumerable<T> data, IEnumerable<string> headers);
    }
}