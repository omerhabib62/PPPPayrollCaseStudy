﻿using PayrollCaseStudy.CommonTypes;
using PayrollCaseStudy.PayrollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollCaseStudy.PayrollImplementation {
    public class MonthlySchedule : PaymentSchedule{
        public bool IsPayDate(Date date) {
            return IsLastDayOfMonth(date);
        }

        private bool IsLastDayOfMonth(Date date) {
            return date.AddDays(1).Month != date.Month;
        }


        public Date GetPayPeriodStartDate(Date payPeriod) {
            return payPeriod.AddMonth(-1).AddDays(1);
        }
    }
}
