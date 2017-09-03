using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetStock.Contract
{
    public class RoleRights : IContract
    {
        // Constructor 
        public RoleRights() { }

        // Public Members 

        [DisplayName("RoleCode")]
        public string RoleCode { get; set; }

        [DisplayName("SecurableItem")]
        public string SecurableItem { get; set; }


    }
}
