using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Staffs).WithOne(x => x.Department).HasForeignKey(x => x.DepartmentId);
            builder.HasMany(x => x.Documents).WithOne(x => x.Department).HasForeignKey(x => x.DepartmentId);
            builder.HasData(
                new Department
                {
                    Id = "9a07d60f--4930-8e8b1629",
                    DepartmentName = "Backend",
                    Acronym = "BCK",
                }
                );
        }
    }
}
