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
    public class AverageIndicatorRepository : IAverageIndicatorRepository
    {
        private readonly TestAppContext _appContext;
        public AverageIndicatorRepository(TestAppContext context)
        {
            _appContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(AverageIndicator entity)
        {
            _appContext.AverageIndicators.Add(entity);
            _appContext.SaveChanges();
        }

        public void Delete(AverageIndicator entity)
        {
            _appContext.AverageIndicators.Remove(entity);
            _appContext.SaveChanges();
        }

        public AverageIndicator? Get(Guid id)
        {
            return _appContext.AverageIndicators.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<AverageIndicator>> GetAll()
        {
            return await _appContext.AverageIndicators.Select(e => e).ToListAsync();
        }

        public List<AverageIndicator> GetByPerformanceIndicatorId(Guid performanceIndicatorId)
        {
            return _appContext.AverageIndicators.Where(e => e.PerformanceIndicatorId == performanceIndicatorId).ToList();
        }

        public void Save(List<AverageIndicator> entities)
        {
            _appContext.AverageIndicators.AddRange(entities);
            _appContext.SaveChanges();
        }

        public void Update(AverageIndicator entity)
        {
            _appContext.AverageIndicators.Update(entity);
            _appContext.SaveChanges();
        }
    }
}
