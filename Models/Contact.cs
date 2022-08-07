using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Contact
    {
        [Key]
        public string Email { get; set; }
        public string FirstNme { get; set; }
        public string LastName { get; set; }
        
    }
}
