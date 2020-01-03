using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Entities
{
    [Table("device_file")]
    public class DeviceFile
    {
        [Key]
        public long id{get;set;}

        [ForeignKey("DeviceMain")]
        [Column("belong_device_id")]
        public long belongDeviceId{get;set;}
        public string name{get;set;}
        [Column("created_by")]
        public string createdBy{get;set;}
        [Column("created_on")]
        public DateTime ? createdOn{get;set;}
        public string key{get;set;}
        public string extend{get;set;}
        public float size{get;set;}

      
        public virtual  DeviceMain DeviceMain { get; set; }

        public string url{get;set;}
    }
}
