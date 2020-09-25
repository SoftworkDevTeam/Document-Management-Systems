using System;
using System.Collections.Generic;

namespace DocumentManagementSystem.Models
{
    public partial class AccessType
    {
        public AccessType()
        {
            DocumentAccessRight = new HashSet<DocumentAccessRight>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<DocumentAccessRight> DocumentAccessRight { get; set; }
    }
}
