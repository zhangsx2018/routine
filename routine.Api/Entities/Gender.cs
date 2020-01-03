using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Entities
{
    public class Gender
    {
        [Key]
        public Guid id { get; set; }
    }
}
