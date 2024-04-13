using MyMVC_Practice.Models;

namespace MyMVC_Practice.Repository.Interfaces
{
    public interface IStaffRepository
    {
        public Staff CreateStaff(Staff staff);
        public Staff GetStaff(string email, string password);
        public IEnumerable<Staff> GetAll();
        public bool DeleteStaff(Staff staff);
    }
}
