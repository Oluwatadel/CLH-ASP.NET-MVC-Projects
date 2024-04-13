using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Staff).WithOne(x => x.User);
            builder.HasData(
                new User
                {
                    Id = "9a07d60f--4930-8e8b1629",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    Gender = GenderEnum.Male,
                    Password = "123456",
                    PhoneNumber = "1234567890",
                });
        }
    }
}
