using System.Collections.Generic;

namespace SAPPricing
    {
    public class ConditionRecordsEntity
    {
        public string ConditionRecordNumber { get; set; }
        public string ConditionValidFromDate { get; set; }
        public string ConditionValidToDate { get; set; }
        public List<ConditionItemsEntity> E1KONP { get; set; }
    }
}
