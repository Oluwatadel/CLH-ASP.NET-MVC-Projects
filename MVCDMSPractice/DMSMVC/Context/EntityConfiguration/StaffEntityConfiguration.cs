using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
    public class StaffEntityConfiguration : IEntityTypeConfiguration<Staff>
    {
        //private readonly IDepartmentRepository _departmentRepository;
        //private readonly IUnitOfWork _unitOfWork;

        //public StaffEntityConfiguration(IStaffRepository departmentRepository, IUnitOfWork unitOfWork)
        //{
        //    _departmentRepository = departmentRepository;
        //    _unitOfWork = unitOfWork;
        //}

        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Department).WithMany(x => x.Staffs).HasForeignKey(x => x.DepartmentId);
            builder.HasMany(x => x.Documents).WithOne(x => x.Staff);
            builder.HasOne(x => x.User).WithOne(x => x.Staff);
            builder.HasData(
                new Staff
                {
                    Id = "8f4667b3-9f21-42b7-80a0",
                    UserId = "9a07d60f--4930-8e8b1629",
                    StaffNumber = "49419",
                    DepartmentId = "9a07d60f--4930-8e8b1629",
                    Level = "12",
                    Position = "Staff",
                });
        }
    }
}
