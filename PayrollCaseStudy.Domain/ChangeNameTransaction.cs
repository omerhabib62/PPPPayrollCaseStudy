﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCaseStudy.Domain {
    public class ChangeNameTransaction : ChangeEmployeeTransaction{
        private string _newName;

        public ChangeNameTransaction(int empId,string newName)  :base(empId){
            _newName = newName;
        }

        

        protected override void Change(Employee employee) {
            employee.Name = _newName;
        }
    }
}
