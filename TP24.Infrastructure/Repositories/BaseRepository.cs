using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24.Core.Interfaces.Repositories;
using TP24.Data;

namespace TP24.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly TP24DbContext _dbContext;

        public BaseRepository(TP24DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
