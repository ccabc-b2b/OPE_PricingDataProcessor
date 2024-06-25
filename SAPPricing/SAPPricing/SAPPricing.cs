using System.Collections.Generic;

namespace SAPPricing
    {
    public class SAPPricing
    {
        public string type { get; set; }
        public string message { get; set; }
        public List<FilterSegmentsEntity> payload { get; set; }
        public string status { get; set; }
        public string id { get; set; }
    }
}
