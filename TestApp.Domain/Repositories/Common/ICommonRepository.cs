using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models.Common;

namespace TestApp.Domain.Repositories.Common
{
    public interface ICommonRepository<T> 
        where T : EntityModel
    {
        void Add(T entity);
        void Save(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        T? Get(Guid id);
        Task<IEnumerable<T>> GetAll();
    }
}
