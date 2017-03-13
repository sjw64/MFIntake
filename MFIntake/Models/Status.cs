using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MFIntake.Models
{
    public class Status
    {
        public int ID { get; set; }

        public string StatusType { get; set; }

        public string StatusName { get; set; }

        public string Description { get; set; }
    }
}