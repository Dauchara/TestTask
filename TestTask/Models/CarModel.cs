using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.Models
{
    public class CarModel
    {
        public virtual int? ID { get; set; }
        public virtual string GovNumber { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}