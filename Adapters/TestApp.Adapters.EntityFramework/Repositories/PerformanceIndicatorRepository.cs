using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models.PerformanceIndicator;
using TestApp.Domain.Repositories;
using TestApp.Domain.Repositories.Common;

namespace TestApp.Adapters.EntityFramework.Repositories
{
    public class PerformanceIndicatorRepository : IPerformanceIndicatorRepository
    {
        private readonly TestAppContext _appContext;

        public PerformanceIndicatorRepository(TestAppContext context)
        {
            _appContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(PerformanceIndicator entity)
        {
            _appContext.PerformanceIndicators.Add(entity);
            _appContext.SaveChanges();
        }

        public void Delete(PerformanceIndicator entity)
        {
            _appContext.PerformanceIndicators.Remove(entity);
            _appContext.SaveChanges();
        }

        public PerformanceIndicator? Get(Guid id)
        {
            return _appContext.PerformanceIndicators.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<PerformanceIndicator>> GetAll()
        {
            return await _appContext.PerformanceIndicators.Select(e => e).ToListAsync();
        }

        public void Save(List<PerformanceIndicator> entities)
        {
            _appContext.PerformanceIndicators.AddRange(entities);
            _appContext.SaveChanges();
        }

        public void Update(PerformanceIndicator entity)
        {
            _appContext.PerformanceIndicators.Update(entity);
            _appContext.SaveChanges();
        }

    }
}
