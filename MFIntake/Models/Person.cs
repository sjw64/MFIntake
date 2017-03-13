using System.Collections.Generic;

namespace MFIntake.Models
{

    public class Person : Contacts
    {
        public string discriminator { get; set; }

    }
   
}
