using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Models
{
    public class DeviceFileDto
    {
       
        public long id { get; set; }
       
        public long belongDeviceId { get; set; }

        //public string belongDeviceName { get; set; }
        public string name { get; set; }
        
        public string createdBy { get; set; }
        
        public DateTime? createdOn { get; set; }
        public string key { get; set; }
        public string extend { get; set; }
        public float size { get; set; }
      //  public string url { get; set; }
    }
}
