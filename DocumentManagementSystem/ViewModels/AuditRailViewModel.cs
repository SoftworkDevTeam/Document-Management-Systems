using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.ViewModels
{
    public class AuditRailViewModel
    {
        public long Id { get; set; }
        public string ActionName { get; set; }
        public string Remark { get; set; }
        public int ActionId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string WebPage { get; set; }
        public string IpAddress { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
