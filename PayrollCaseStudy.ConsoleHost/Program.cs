﻿using PayrollCaseStudy.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCaseStudy.ConsoleHost {
    class Program {
        static void Main(string[] args) {
            var reader = new StreamReader(new FileStream("TestTransactions.txt",FileMode.Open,FileAccess.Read));
            var parser = new TextParserTransactionSource(reader);
            var app = new PayrollApplication(parser);
            app.Process();
            return;
        }
    }
}
