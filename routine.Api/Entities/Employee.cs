using System;
using System.ComponentModel.DataAnnotations;

namespace routine.Api.Entities
{
    public class Employee {

        public Guid id { get; set; }
        public Guid CompanyId { get; set; }
        [Required]
        [MaxLength(10)]
        public string EmployeeNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string lastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Company Company { get; set; }

    }

}
