using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using MySql.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace  routine.Api.Entities
{
    [Table("device_type")]
    public partial class DeviceType
    {
       

        [Key]
        public long id{ get; set; }
        public string name{ get; set; }
        public string img{ get; set; }
        [Column("created_by")]
        public string createdBy{ get; set; }
        [NotMapped]
        public string createdName{ get; set; }
        [Column("created_on")]
        public DateTime ? createdOn{ get; set; }
        [Column("modified_by")]
        public string modifiedBy{ get; set; }
        [Column("modified_on")]
        public DateTime ? modifiedOn{ get; set; }
        public bool flag{ get; set; }
        [Column("company_id")]
        public int companyId{ get; set; }
        public string remark{ get; set; }
        [Column("svg_class_name")]
        public string svgClassName{ get; set; }
        [Column("svg_color")]
        public string svgColor{ get; set; }

     


    }
}
