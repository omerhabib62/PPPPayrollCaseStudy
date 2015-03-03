﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PayrollCaseStudy.Domain.Tests {
    [TestClass]
    public class PayrollTest {
        [TestInitialize]
        public void Init() {
            PayrollDatabase.Instance.Clear();
        }

        [TestMethod]
        public void TestAddSalariedEmployee() {
            int empId = 1;
            var t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.0M);

            t.Execute();

            var e = PayrollDatabase.Instance.GetEmployee(empId);

            Assert.AreEqual("Bob", e.Name);

            PaymentClassification pc = e.GetClassification();

            SalariedClassification sc = (SalariedClassification)pc;

            Assert.AreEqual(sc.Salary, 1000.00M);

            PaymentSchedule ps = e.GetSchedule();

            Assert.IsInstanceOfType(ps, typeof(MonthlySchedule));

            PaymentMethod pm = e.GetMethod();

            Assert.IsInstanceOfType(pm, typeof(HoldMethod));
        }

        [TestMethod]
        public void TestAddHourlyEmployee() {
            int empId = 1;
            var t = new AddHourlyEmployee(empId, "Bob", "Home", 10.0M);

            t.Execute();

            var e = PayrollDatabase.Instance.GetEmployee(empId);

            Assert.AreEqual("Bob", e.Name);

            PaymentClassification pc = e.GetClassification();

            HourlyClassification sc = (HourlyClassification)pc;

            Assert.AreEqual(sc.HourlyRate, 10.00M);

            PaymentSchedule ps = e.GetSchedule();

            Assert.IsInstanceOfType(ps, typeof(WeeklySchedule));

            PaymentMethod pm = e.GetMethod();

            Assert.IsInstanceOfType(pm, typeof(HoldMethod));
        }

        [TestMethod]
        public void TestAddCommisionedEmployee() {
            int empId = 1;
            var t = new AddCommissionedEmployee(empId, "Bob", "Home", 1000.0M, 50.0M);

            t.Execute();

            var e = PayrollDatabase.Instance.GetEmployee(empId);

            Assert.AreEqual("Bob", e.Name);

            PaymentClassification pc = e.GetClassification();

            CommissionedClassification sc = (CommissionedClassification)pc;

            Assert.AreEqual(sc.Salary, 1000.00M);
            Assert.AreEqual(sc.CommissionRate, 50.0M);

            PaymentSchedule ps = e.GetSchedule();

            Assert.IsInstanceOfType(ps, typeof(BiweeklySchedule));

            PaymentMethod pm = e.GetMethod();

            Assert.IsInstanceOfType(pm, typeof(HoldMethod));
        }

        [TestMethod]
        public void TestDeleteEmployee() {
            int empId = 3;
            var addTx = new AddCommissionedEmployee(empId,"Lance", "Home", 2500, 3.2M);
            addTx.Execute();

            var employee = PayrollDatabase.Instance.GetEmployee(empId);

            Assert.IsNotNull(employee);

            var deleteTx = new DeleteEmployeeTransaction(empId);
            deleteTx.Execute();

            employee = PayrollDatabase.Instance.GetEmployee(empId);

            Assert.IsNull(employee);
        }

        [TestMethod]
        public void TestTimeCardTransaction() {
            int empId = 2;
            var addTx = new AddHourlyEmployee(empId,"Bill", "Home", 15.25M);
            addTx.Execute();

            
            var timeCardTransaction = new TimeCardTransaction(20011031, 8.0M, empId);
            timeCardTransaction.Execute();

            var employee = PayrollDatabase.Instance.GetEmployee(empId);
            Assert.IsNotNull(employee);

            PaymentClassification classification = employee.GetClassification();
            var hourlyClassification = (HourlyClassification)classification;

            var timeCard = hourlyClassification.GetTimeCard(20011031);
            Assert.IsNotNull(timeCard);
            Assert.AreEqual(8.0M, timeCard.Hours);
        }
    }
}
