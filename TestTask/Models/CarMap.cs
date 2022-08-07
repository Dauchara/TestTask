using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.Models
{
    public class CarMap: ClassMap<CarModel>
    {
        public CarMap()
        {
            Table("Car");
            Id(x => x.ID, "Id").GeneratedBy.Identity().UnsavedValue(0);
            Map(x => x.GovNumber);
            Map(x => x.Balance);
            Map(x => x.CreatedDate);
        }
    }
}