using dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dal.Maps
{
    public class CommentMap
    {
        public void Build(EntityTypeBuilder<Comment> modelBuilder)
        {
            modelBuilder
                .Property(p => p.CommentId)
                .ValueGeneratedOnAdd();

            modelBuilder
                .HasOne(a => a.User)
                .WithMany(b => b.Comments)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.AuthorId);

            modelBuilder
                .HasOne(a => a.Post)
                .WithMany(b => b.Comments)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
