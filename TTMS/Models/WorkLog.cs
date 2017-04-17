using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class WorkLog
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public DateTime CretionDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        public double TimeSpend
        {
            get;
            //{
            //    return (EndTime - StartTime).TotalHours;
            //}
            set;
            //{
            //    this.TimeSpend = (EndTime - StartTime).TotalHours;
            //}
        }
        public virtual Work work { get; set; }
        public virtual User user { get; set; }
    }
}