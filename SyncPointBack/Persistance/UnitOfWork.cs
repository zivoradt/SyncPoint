using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using SyncPointBack.Auth.Users;
using SyncPointBack.Model.Excel;
using SyncPointBack.Persistance.Interface;

namespace SyncPointBack.Persistance
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SyncPointDb _dbContext;
        private readonly AuthDbContext _authContext;

        private bool disposed = false;

        private GenericRepository<ExcelRecord> _excelRepository;

        public UnitOfWork(SyncPointDb context, AuthDbContext authDbContext)
        {
            _dbContext = context;
            _authContext = authDbContext;
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

        public AuthDbContext AuthRepository
        {
            get
            {
                return this._authContext;
            }
        }

        public async Task Save()
        {
            if (_dbContext.ChangeTracker.HasChanges())
            {
                await _dbContext.SaveChangesAsync();
            }
            if (_authContext.ChangeTracker.HasChanges())
            {
                await _authContext.SaveChangesAsync();
            }
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