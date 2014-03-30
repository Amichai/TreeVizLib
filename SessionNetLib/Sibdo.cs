using SessionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionNetLib {
    public class Sibdo : IGenerateHtml {
        public string Url { get; private set; }
        public Sibdo(string url) {
            this.Url = url;
        }

        public string Html() {
            return @"<div class=""sibdo"" data-url=""data.csv""></div>";
        }
    }
}
