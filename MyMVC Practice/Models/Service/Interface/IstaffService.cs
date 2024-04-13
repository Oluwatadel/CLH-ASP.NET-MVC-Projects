using MyMVC_Practice.Context;
using MyMVC_Practice.Repository.Implementations;
using MyMVC_Practice.Repository.Interfaces;

namespace MyMVC_Practice.Models.Service.Interface
{
    public interface IstaffService
    {
        public Staff Login(string staffNumber, string password);
        public Staff Register(Staff staff);
        public bool DeleteStaff(Staff staff);
        public Staff EditDetails(Staff staff);

    }
}
