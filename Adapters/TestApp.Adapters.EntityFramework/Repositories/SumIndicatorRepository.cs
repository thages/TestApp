using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models.DailyIndicator;
using TestApp.Domain.Repositories;

namespace TestApp.Adapters.EntityFramework.Repositories
{
    public class SumIndicatorRepository : ISumIndicatorRepository
    {
        private readonly TestAppContext _appContext;

        public SumIndicatorRepository(TestAppContext context)
        {
            _appContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(SumIndicator entity)
        {
            _appContext.SumIndicators.Add(entity);
            _appContext.SaveChanges();
        }

        public void Delete(SumIndicator entity)
        {
            _appContext.SumIndicators.Remove(entity);
            _appContext.SaveChanges();
        }

        public SumIndicator? Get(Guid id)
        {
            return _appContext.SumIndicators.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<SumIndicator>> GetAll()
        {
            return await _appContext.SumIndicators.Select(e => e).ToListAsync();
        }

        public List<SumIndicator> GetByPerformanceIndicatorId(Guid performanceIndicatorId)
        {
            return _appContext.SumIndicators.Where(e => e.PerformanceIndicatorId == performanceIndicatorId).ToList();
        }

        public void Save(List<SumIndicator> entities)
        {
            _appContext.SumIndicators.AddRange(entities);
            _appContext.SaveChanges();
        }

        public void Update(SumIndicator entity)
        {
            _appContext.SumIndicators.Update(entity);
            _appContext.SaveChanges();
        }
    }
}
