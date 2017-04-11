using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTMS.Models
{
    public class Comment
    {
        public Comment()
        {
        }
        public int ID { get; set; }
        public DateTime PostTime { get; set; }
        public string Content { get; set; }
        public virtual Work Work { get; set; }
        public virtual User User { get; set; }

    }
    public class CommentViewModel
    {
        public DateTime PostTime { get; set; }
        public string Content { get; set; }
        public virtual Work Work { get; set; }
        public virtual User User { get; set; }

    }
}