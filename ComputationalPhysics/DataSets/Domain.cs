using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics.DataSets {
    internal class Domain : DataSet {
        public Domain(string name, domainType type) : base(name) {

        }

        internal domainType type { get; private set; }

        public bool IsTimeDomain {
            get {
                return type != domainType.real;
            }
        }
    }
    internal enum domainType { timespan, datetime, real };

}
