using DocumentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.ViewModels
{
    public class UserAccessRequestViewModel
    {
        public long UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<AccessType> AccessList { get; set; }
    }
}
