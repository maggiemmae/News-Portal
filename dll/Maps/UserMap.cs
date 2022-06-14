using dal.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dal.Maps
{
    public class UserMap
    {
        public void Build(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasIndex(b => b.UserName).IsUnique();
        }
    }
}
