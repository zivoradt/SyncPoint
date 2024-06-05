using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using SyncPointBack.Model.Excel;
using SyncPointBack.Persistance.Interface;

namespace SyncPointBack.Persistance
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SyncPointDb _dbContext;

        private bool disposed = false;

        private GenericRepository<ExcelRecord> _excelRepository;

        public UnitOfWork(SyncPointDb context)
        {
            _dbContext = context;
        }

        public GenericRepository<ExcelRecord> ExcelRepository
        {
            get
            {
                if (_excelRepository == null)
                {
                    this._excelRepository = new GenericRepository<ExcelRecord>(_dbContext);
                }
                return _excelRepository;
            }
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }

                disposed = true;
            }
        }
    }
}