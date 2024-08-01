using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen_Use_Case {
    // Class to represent the structure of each line in the JSON response
    public class Line {
        public string Locale { get; set; }
        public string Description { get; set; }
        public BoundingPoly BoundingPoly { get; set; }
    }
}
