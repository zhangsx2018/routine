using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace routine.Api.Entities
{
    public class Company
    {
        [Key]
        public Guid id { get; set; }
        
        public string name { get; set; }

        [MaxLength(500)]
        //[NotMapped]
        public string Introduction { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
