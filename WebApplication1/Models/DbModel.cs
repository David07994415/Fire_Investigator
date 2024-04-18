using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security;

namespace WebApplication1.Models
{
    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
        }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Business> Business { get; set; }
        public virtual DbSet<BusinessCategory> BusinessCategory { get; set; }
        public virtual DbSet<Directory> Directory { get; set; }
        public virtual DbSet<WebContent> WebContent { get; set; }
        public virtual DbSet<Master> Master { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
