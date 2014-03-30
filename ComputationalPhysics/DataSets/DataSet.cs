using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics.DataSets {
    internal abstract class DataSet {
        protected string name;
        public DataSet(string name) {
            this.name = name;
            this.vals = new List<double>();
        }
        protected List<double> vals { get; set; }

        internal void Add(double val) {
            this.vals.Add(val);
        }

        public List<double> GetVals(bool normalize) {
            if (normalize) {
                return this.Normalize();
            } else {
                return this.vals;
            }
        }

        internal List<double> Normalize() {
            double max = double.MinValue;
            double min = double.MaxValue;
            foreach (var v in this.vals) {
                if (v < min) {
                    min = v;
                }
                if (v > max) {
                    max = v;
                }
            }
            double range = max - min;
            return this.vals.Select(i => 1.0 - ((max - i) / range)).ToList();
        }
    }
}
