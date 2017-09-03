﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetStock.Contract
{
    public class Roles : IContract
    {
        // Constructor 
        public Roles() { }

        // Public Members 

        [DisplayName("RoleCode")]
        public string RoleCode { get; set; }

        [DisplayName("RoleDescription")]
        public string RoleDescription { get; set; }

        [DisplayName("IsActive")]
        public bool IsActive { get; set; }

        [DisplayName("CreatedBy")]
        public string CreatedBy { get; set; }

        [DisplayName("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("ModifiedBy")]
        public string ModifiedBy { get; set; }

        [DisplayName("ModifiedOn")]
        public DateTime ModifiedOn { get; set; }


    }
}
