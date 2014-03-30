using ComputationalPhysics.DataSets;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics {
    public class CSVDataSource : DataSource {
        public CSVDataSource(string filepath)
            : base(filepath) {
                this.ranges = new List<Range>();
        }

        private List<Range> ranges;

        public void Normalize() {
            this.normalize = true;
        }

        public void Unnormalize() {
            this.normalize = false;
        }

        public void AddRange(int idx, string name = "") {
            var newRange = new Range(name);
            this.ranges.Add(newRange);
            foreach (var l in this.lines) {
                var split = l.Split(',');
                try {
                    var val = double.Parse(split[idx]);
                    newRange.Add(val);
                } catch {
                    continue;
                }
            }
        }

        private int getIndexOfColumn(string columnName) {
            var split = this.lines.First().Split(',').ToList();
            var matched = split.Where(i => i.ToLower().Contains(columnName.ToLower()));
            var match = matched.OrderBy(i => Math.Abs(i.Length - columnName.Length)).First();
            return split.IndexOf(match);////CONTINUE HERE..
            
        }

        public override void AddRange(string s) {
            //var idx = indexWhere(this.lines.First().Split(',').ToList(), i => i.Contains(s));
            var idx = getIndexOfColumn(s);
            this.AddRange(idx, s);
        }

        private domainType getDomainType(string val) {
            DateTime t;
            TimeSpan t2;
            double d;
            if (DateTime.TryParse(val, out t)) {
                return domainType.datetime;
            } else if(TimeSpan.TryParse(val, out t2)){
                return domainType.timespan;
            } else if (double.TryParse(val, out d)) {
                return domainType.real;
            } else {
                throw new Exception("Unable to determine field type");
            }
        }

        private Domain domain;

        public PlotModel Chart(string d, string r) {
            this.SetDomain(d);
            this.AddRange(r);
            return Charting.Chart(this.domain.GetVals(false),
                this.ranges.Last().GetVals(this.normalize), 
                this.domain.IsTimeDomain);
        }

        public PlotModel Chart(string d, params string[] ranges) {
            this.SetDomain(d);
            foreach (var r in ranges) {
                this.AddRange(r);
            }
            var added = ranges.Count();
            var toPlot = this.ranges.Skip(this.ranges.Count() - added).Select(i => i.GetVals(this.normalize));
            return Charting.Chart(this.domain.GetVals(false), 
                this.domain.IsTimeDomain, toPlot);
        }

        public PlotModel Chart(string d, string r1, string r2) {
            this.SetDomain(d);
            this.AddRange(r1);
            this.AddRange(r2);
            var rangeCount = this.ranges.Count();
            return Charting.Chart(this.domain.GetVals(false), 
                this.ranges[rangeCount - 1].GetVals(this.normalize),
                this.ranges[rangeCount - 2].GetVals(this.normalize), 
                this.domain.IsTimeDomain);
        }

        private double parse(string val, domainType type) {
            switch (type) {
                case domainType.datetime:
                    return OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Parse(val));
                case domainType.timespan:
                    return OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.Date + TimeSpan.Parse(val));
                default:
                    return double.Parse(val);
            }
        }

        public void SetDomain(int idx, string s) {
            domainType domainType = this.getDomainType(this.lines[1].Split(',')[idx]);
            var newDomain = new Domain(s, domainType);
            foreach (var l in this.lines) {
                var split = l.Split(',');
                try {
                    var val = parse(split[idx], domainType);
                    newDomain.Add(val);
                } catch {
                    continue;
                }
            }
            this.domain = newDomain;
        }

        //private int indexWhere(List<string> s, Func<string, bool> a) {
        //    for (int i = 0; i < s.Count(); i++) {
        //        if (a(s[i])) {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

        public override void SetDomain(string s) {
            //var idx = indexWhere(this.lines.First().Split(',').ToList(), i => i.Contains(s));
            var idx = getIndexOfColumn(s);
            this.SetDomain(idx, s);
        }
    }
}
