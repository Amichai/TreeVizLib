using ComputationalPhysics;
using SessionNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester {
    class Program {
        static void Main(string[] args) {
            //var r = Url.Gist("https://gist.github.com/Amichai/6686304");
            var r = new CSVDataSource(@"C:\Data\regressionData - 12-26-13.txt");
            
            r.Chart("Timestamp", "speed", "drivespeed");

            //List<ITest> t1 = new List<ITest>();
            //t1.Add(new test());
            //t1.Add(new test());
            //t1.Add(new test());

            //List<test> t2 = (List<test>)t1;
        }
    }

    interface ITest {
        int t { get; set; }
    }

    struct test : ITest {
        public int t { get; set; }
    }
}
