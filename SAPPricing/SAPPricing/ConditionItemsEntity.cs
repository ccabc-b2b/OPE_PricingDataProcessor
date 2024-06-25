using System;
using System.Collections.Generic;

namespace SAPPricing
    {
    public class ConditionItemsEntity
    {
        public Int64 Id { get; set; }
        public string ConditionRecordNumber { get; set; }
        public string ConditionType { get; set; }
        public string ScaleType { get; set; }
        public string VariableKey { get; set; }
        public string ConditionValidFromDate { get; set; }
        public string ConditionValidToDate { get; set; }
        public string ConditionScaleQuantity { get; set; }
        public string ScaleValue { get; set; }
        public string CalculationTypeForCondition { get; set; }
        public string ConditionAmountOrPercentageRate { get; set; }
        public string CurrencyOrPercentageRateUnit { get; set; }
        public string ConditionPricingUnit { get; set; }
        public string ConditionUnit { get; set; }
        public string NumeratorForUnitConversion { get; set; }
        public string DenomineratorForUnitConversion { get; set; }
        public string ConditionAmountLowerLimit { get; set; }
        public string ConditionAmountUpperLimit { get; set; }
        public string ConditionCurrencyForCumulationFields { get; set; }
        public string DeletionIndicatorForConditionItem { get; set; }
        public string ConditionItemIndex { get; set; }
        public string AccrualAmount { get; set; }
        public string AdditionalValueDays { get; set; }
        public string FixedValueDate { get; set; }
        public string MaxNumOfSalesOrdersPerConditionRecord { get; set; }
        public string ConditionBaseValueMin { get; set; }
        public string ConditionBaseValueMax { get; set; }
        public string MaximumConditionValue { get; set; }
        public string NumberOfIncrementalScale { get; set; }
        public string ScaleNumberForPricing { get; set; }
        public string IsDeleted { get; set; }
        public string ConditionScaleValue { get; set; }
        public string ScaleBasis { get; set; }
        public List<ConditionScaleEntity> E1KONW { get; set; }

    }
}
