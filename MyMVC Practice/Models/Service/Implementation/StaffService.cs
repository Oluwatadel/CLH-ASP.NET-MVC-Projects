using System;
using MyMVC_Practice.Context;
using MyMVC_Practice.Models.Service.Interface;
using MyMVC_Practice.Repository.Implementations;
using MyMVC_Practice.Repository.Interfaces;

namespace MyMVC_Practice.Models.Service.Implementation
{
    public class StaffService : IstaffService
    {
        IStaffRepository _staffRepo = new StaffRepository();
        

        public Staff Register(Staff staff)
        {
            _staffRepo.CreateStaff(staff);
            return staff;
        }

        public bool DeleteStaff(Staff staff)
        {
            _staffRepo.DeleteStaff(staff);
            if(_staffRepo.GetAll().Contains(staff))
            {
                return false;
            }
            return true;
        }

        public Staff EditDetails(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Staff Login(string email, string password)
        { 
            var staff = _staffRepo.GetStaff(email, password);
            return staff;
        }
    }
}
