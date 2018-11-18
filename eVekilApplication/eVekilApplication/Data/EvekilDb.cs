using eVekilApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Data
{
    public class EvekilDb : IdentityDbContext<User>
    {
        public EvekilDb(DbContextOptions<EvekilDb> dbContextOptions) : base(dbContextOptions) { }

        public virtual DbSet<Advocate> Advocates { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CreatedDocument> CreatedDocuments { get; set; }
        public virtual DbSet<PropertySubcategory> PropertySubcategories { get; set; }
        public virtual DbSet<PropertyValue> PropertyValues { get; set; }
        public virtual DbSet<PurchasedDocument> PurchasedDocuments { get; set; }

    }
}
