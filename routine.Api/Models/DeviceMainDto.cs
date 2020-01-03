using routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Models
{
    public class DeviceMainDto
    {
      
        public long? id { get; set; }
     
        public long? deviceStatusId { get; set; }
      
        public string deviceStatus { get; set; }

   
        public long? deviceTypeId { get; set; }

        public string deviceTypeName { get; set; }

        public long? belongProjectId { get; set; }
      
        public string belongProjectName { get; set; }
       
        public int? belongDeptId { get; set; }
    
        public string belongDeptName { get; set; }

        public string lnglat { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string typeno { get; set; }
        public string no { get; set; }
        public string brand { get; set; }
        public string manufactor { get; set; }

       
        public DateTime? buyDate { get; set; }
    
        public DateTime? createdOn { get; set; }

        /**
        * 0：无效；1：有效
        */
        //  public bool flag { get; set; }

        // public DeviceType DeviceType { get; set; }
        public  List<DeviceFileDto> DeviceFiles { get; set; }
        public string img { get; set; }
    }
}
