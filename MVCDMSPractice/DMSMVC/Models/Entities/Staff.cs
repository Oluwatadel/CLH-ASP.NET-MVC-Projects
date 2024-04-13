using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Models.Entities
{
    public class Staff : Base
    {
        public string StaffNumber { get; set; } = default!;
        public string Level { get; set; }
        public string Position { get; set; } = "Staff";
        public Department Department { get; set; }
        public string DepartmentId { get; set; }
        public ICollection<Document> Documents { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }



    }
}
