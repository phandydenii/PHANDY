
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class RegisterstudentViewModel
    {
        public IEnumerable<grade> grads { get; set; }

        public IEnumerable<student> students { get; set; }
        public IEnumerable<studylanguage> studylanguages { get; set; }
        public IEnumerable<studyperiod> studyperiods { get; set; }
        public IEnumerable<shifts> shifts { get; set; }


    }
}