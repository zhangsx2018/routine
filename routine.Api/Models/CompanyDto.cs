using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Models
{
    public class CompanyDto
    {
        [Key]
        public Guid id { get; set; }

        public string name { get; set; }

    }
}
