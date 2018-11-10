using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Data
{
    public class EvekilDb : DbContext
    {
        public EvekilDb(DbContextOptions<EvekilDb> dbContextOptions) : base(dbContextOptions) { }
    }
}
