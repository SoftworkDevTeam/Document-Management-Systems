using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagementSystem.ViewModels
{
    public class PageViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        public string Area { get; set; }
        public string Data { get; set; }
        public IEnumerable<ProfileViewModel> ProfileList { get; set; }
        public IEnumerable<DocumentViewModel> NewDocList { get; set; }
    }
}
