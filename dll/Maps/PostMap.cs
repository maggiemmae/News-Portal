using dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dal.Maps
{
    public class PostMap
    {
        public void Build(EntityTypeBuilder<Post> modelBuilder)
        {
            modelBuilder
                .HasIndex(b => b.Title).IsUnique();

            modelBuilder
                .Property(p => p.PostId)
                .ValueGeneratedOnAdd();

            modelBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.AuthorId);
        }
    }
}
