using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTMS.Models
{
    public class WorkAssignmentViewModel
    {
        public Work Work { get; set; }
        public IEnumerable<SelectListItem> AllStudents { get; set; }
        private List<string> _selectedstudents;
        public List<string> SelectedStudents
        {
            get
            {
                if (_selectedstudents == null)
                {
                    _selectedstudents = Work.Assignedstudents.Select(m => m.Id).ToList();
                }
                return _selectedstudents;
            }
            set { _selectedstudents = value; }
        }
    }
}