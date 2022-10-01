using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repository
{
    public class ApplicationRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T: class
    {
        private readonly ApplicationDBContext dbContext;

        public ApplicationRepositoryAsync(ApplicationDBContext dBContext) : base(dBContext)
        {
            this.dbContext = dbContext;
        }
    }
}
