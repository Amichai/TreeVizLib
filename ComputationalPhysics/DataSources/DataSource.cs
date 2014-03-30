using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics {
    public abstract class DataSource {
        protected string[] lines;
        protected bool normalize;
        public DataSource(string filepath, bool normalize = false) {
            this.lines = System.IO.File.ReadAllLines(filepath);
            this.normalize = normalize;
        }

        public abstract void AddRange(string s);


        public abstract void SetDomain(string s);
    }
}
