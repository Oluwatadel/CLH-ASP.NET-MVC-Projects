using MyMVC_Practice.Context;
using MyMVC_Practice.Models;
using MyMVC_Practice.Repository.Interfaces;

namespace MyMVC_Practice.Repository.Implementations
{
    public class StaffRepository : IStaffRepository
    {
        public Staff CreateStaff(Staff staff)
        {
            DataBase.staffs.Add(staff);
            return staff;
        }

        public bool DeleteStaff(Staff staff)
        {
            DataBase.staffs.Remove(staff);
            if(DataBase.staffs.Contains(staff))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Staff> GetAll()
        {
            var staff = DataBase.staffs.AsEnumerable();
            return staff;
        }

        public Staff GetStaff(string email, string password)
        {
            var staff = DataBase.staffs.SingleOrDefault(a => a.Email == email && a.Password == password);
            return staff;
        }
    }
}
