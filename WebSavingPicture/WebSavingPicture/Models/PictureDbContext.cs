namespace WebSavingPicture.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PictureDbContext : DbContext
    {
        public PictureDbContext()
            : base("name=PictureDbContext")
        {
        }

        public virtual DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
