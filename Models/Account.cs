using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Account
    {
        [Key]
        public string AccountName { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
