using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Entities
{
    [Table("device_status")]
    public class DeviceStatus
    {
        [Key]
        public long id{ get; set; }
        public string name{ get; set; }
        [Column("created_by")]
        public string createdBy{ get; set; }
        [Column("created_on")]
        public DateTime ?createdOn{ get; set; }
        [Column("modified_by")]
        public string modifiedBy{ get; set; }
        [Column("modified_on")]
        public DateTime ?modifiedOn{ get; set; }
        public Boolean flag{ get; set; }
        [Column("company_id")]
        public int companyId{ get; set; }



    }
}
