using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySql.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace routine.Api.Entities
{
    [Table("device_main")]
    public class DeviceMain
    {



        [Key]
        public long? id { get; set; }
        [NotMapped]
        public List<long> In_id { get; set; }
        [NotMapped]
        public long? Gt_id { get; set; }
        [ForeignKey("DeviceStatus")]
        [Column("device_status_id")]
        public long? deviceStatusId { get; set; }

        public DeviceStatus DeviceStatus { get; set; }

        [ForeignKey("DeviceType")]
        [Column("device_type_id")]
        public long? deviceTypeId { get; set; }

        [NotMapped]
        public List<long> In_deviceTypeId { get; set; }

        public DeviceType DeviceType { get; set; }

        [Column("belong_project_id")]
        public long? belongProjectId { get; set; }

        [Column("belong_dept_id")]
        public int? belongDeptId { get; set; }

        [NotMapped]
        public List<int> In_belongDeptId { get; set; }

        public string lnglat { get; set; }
        [NotMapped]
        public string Lk_name { get; set; }
      
        [NotMapped]
        public List<string> In_name { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string typeno { get; set; }
        public string no { get; set; }
        public string brand { get; set; }
        public string manufactor { get; set; }

        [Column("buy_date")]
        public DateTime? buyDate { get; set; }
        [Column("created_on")]
        public DateTime? createdOn { get; set; }

        /**
        * 0：无效；1：有效
        */

        public bool? flag { get; set; }


        public string img { get; set; }



        public List<DeviceFile> DeviceFiles { get; set; }

    }
}
