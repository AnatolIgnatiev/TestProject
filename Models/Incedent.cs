using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Incedent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IncedentName { get; set; }

        public string IncedentDescription { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
