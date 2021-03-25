using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICoreDemo.Models
{
    public class CustomerDataModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
    }
}
