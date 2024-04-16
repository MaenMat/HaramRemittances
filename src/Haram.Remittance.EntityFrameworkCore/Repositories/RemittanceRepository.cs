using Haram.Remittance.EntityFrameworkCore;
using Haram.Remittance.Remittances;
using Haram.Remittance.Remmitances;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace Haram.Remittance.Repositories
{
    public class RemittanceRepository : EfCoreRepository<RemittanceDbContext, RemittanceClass, Guid>, IRemittanceRepository
    {
        public RemittanceRepository(IDbContextProvider<RemittanceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<RemittanceClass>> GetPagedIssuedRemittances(int skipCount, int maxResultCount, string sorting)
        {
            var dbSet = await GetDbSetAsync();

            var query = dbSet
                .Where(r => r.HistoryStatus.OrderByDescending(s => s.StatusDate).First().Status == Status.Issued);

            if (!string.IsNullOrWhiteSpace(sorting))
            {
                query = query.OrderBy(sorting);
            }

            var ReadyRemittances = await query
                .Skip(skipCount)
                .Take(maxResultCount)
                .Include(r => r.Sender)
                .ToListAsync();

            return ReadyRemittances;
            //var dbSet = await GetDbSetAsync();
            //var dbContext = await GetDbContextAsync();
            //var IssuedRemittances = await dbSet.Where(r => r.HistoryStatus
            //                                   .OrderByDescending(s => s.StatusDate)
            //                                   .All(x => x.Status == Status.Issued))
            //                                   .Include(r=>r.Sender)
            //                                   .OrderBy(sorting)
            //                                   .Skip(skipCount)
            //                                   .Take(maxResultCount)
            //                                   .ToListAsync();
            //return IssuedRemittances;
        }
        public async Task<int> CountIssuedRemittances()
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Remittances
                                  .Where(r => r.HistoryStatus.Any(s => s.Status == Status.Issued
                                   && s.StatusDate == r.HistoryStatus.Max(h => h.StatusDate)))
                                  .CountAsync();
        }
        public async Task<List<RemittanceClass>> GetPagedReadyRemittances(int skipCount, int maxResultCount, string? sorting)
        {
            var dbSet = await GetDbSetAsync();
            var query = dbSet
                .Where(r => r.HistoryStatus.OrderByDescending(s => s.StatusDate).First().Status == Status.Ready);
            if (!string.IsNullOrWhiteSpace(sorting))
            {
                query = query.OrderBy(sorting);
            }
            var ReadyRemittances = await query
                .Skip(skipCount)
                .Take(maxResultCount)
                .Include(r => r.Sender)
                .ToListAsync();
            return ReadyRemittances;

            //var dbSet = await GetDbSetAsync();
            //var dbContext = await GetDbContextAsync();
            //var ReadyRemittances = await dbSet.Where(r => r.HistoryStatus.OrderByDescending(s => s.StatusDate).First().Status == Status.Ready)
            //    .OrderBy(sorting)
            //    .Skip(skipCount)
            //    .Take(maxResultCount)
            //    .Include(r => r.Sender).ToListAsync();
            //return ReadyRemittances;           
        }
        public async Task<int> CountReadyRemittances()
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Remittances
                                  .Where(r => r.HistoryStatus.Any(s => s.Status == Status.Ready
                                   && s.StatusDate == r.HistoryStatus.Max(h => h.StatusDate)))
                                  .CountAsync();
        }

        public async Task<List<RemittanceClass>> GetPagedApprovedReleasedRemittances(int skipCount, int maxResultCount, string? sorting)
        {
            try
            {
                var dbContext = await GetDbContextAsync();
                var dbSet = await GetDbSetAsync();
                var query = dbSet.AsNoTracking()
                    .Where(r => r.HistoryStatus.OrderByDescending(s => s.StatusDate).First().Status == Status.Approved
                             || r.HistoryStatus.OrderByDescending(s => s.StatusDate).First().Status == Status.Released
                    ).OrderBy(r => r.SerialNumber);
                if (!string.IsNullOrWhiteSpace(sorting))
                {
                    query = query.OrderBy(sorting);
                }
                var ApprovedReleasedRemittances = await query
                    .Include(r => r.HistoryStatus)
                    .Include(r => r.Sender)
                    .Skip(skipCount)
                    .Take(maxResultCount)
                    .ToListAsync();

                return ApprovedReleasedRemittances;
            }
            catch(Exception e) { throw new Exception(e.Message);
            }
    }

        public async Task<int> CountApprovedReleasedRemittances()
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Remittances
                                  .Where(r => r.HistoryStatus.Any(s => s.Status == Status.Approved || s.Status == Status.Released
                                   && s.StatusDate == r.HistoryStatus.Max(h => h.StatusDate)))
                                  .CountAsync();
        }
        public async Task<List<ReportRemittance>> GetReportListAsync(DateTime StrDate, DateTime EndDate, int skipCount, int maxResultCount, string? sorting)
        {
            var dbContext = await GetDbContextAsync();
            var query = dbContext.Remittances
                .Select(r => new ReportRemittance
                {
                    Type = r.Type,
                    Sender = r.Sender,
                    SerialNumber = r.SerialNumber,
                    Amount = r.Amount,
                    TotalAmount = r.TotalAmount,
                    SenderId = r.SenderId,
                    ReceiverId = r.ReceiverId,
                    CurrencyId = r.CurrencyId,
                    LatestStatusInRange = r.HistoryStatus
                    .OrderByDescending(s => s.StatusDate)
                    .FirstOrDefault(s => s.StatusDate.Date >= StrDate.Date && s.StatusDate.Date <= EndDate.Date)
                }).Where(rc => rc.LatestStatusInRange != null);
            if (!string.IsNullOrWhiteSpace(sorting))
                {
                    query = query.OrderBy(sorting);
                }
                var remittances = await query
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            return remittances;
        }
        public async Task<int> GetReportListCountAsync(DateTime startDate, DateTime endDate)
        {
            var dbContext = await GetDbContextAsync();

            var count = await dbContext.Remittances
                .Where(r => r.HistoryStatus.Any(s => s.StatusDate >= startDate && s.StatusDate <= endDate ))
                .CountAsync();

            return count;
        }
        public async Task<RemittanceClass> SearchBySerialNumber(string SerialNumber) 
        {
            var dbSet = await GetDbSetAsync();
            var theRmittance = dbSet.Where(r => r.SerialNumber == SerialNumber).Include(r=>r.HistoryStatus).Include(r => r.Sender).FirstOrDefault();
            return theRmittance;
        }

        public async Task<List<RemittanceClass>> filterByName(string name)
        {
            var dbSet = await GetDbSetAsync();
            var remittances = new List<RemittanceClass>();
            if (!string.IsNullOrEmpty(name))
            {
                var query = dbSet.Include(r => r.HistoryStatus).Include(r => r.Sender).Where(r => r.Sender.FirstName.Contains(name) || r.Sender.LastName.Contains(name));
                remittances = await query.ToListAsync();
            }
            else
            {
                var query = dbSet.Include(r => r.HistoryStatus).Include(r => r.Sender);
                remittances = await query.ToListAsync();
            }

            return remittances;
        }

        public override Task<RemittanceClass> UpdateAsync(RemittanceClass entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            try
            {
                return base.UpdateAsync(entity, autoSave, cancellationToken);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            }
    }
}
