using System.Collections.Generic;

namespace SAPPricing
    {
    public class FilterSegmentsEntity
    {
        public string ConditionRecordNumber { get; set; }
        public string UsageOfTheConditionTable { get; set; }
        public string ConditionTable { get; set; }
        public string Application { get; set; }
        public string ConditionType { get; set; }
        public string VariableKey { get; set; }
        public string SalesOrganization { get; set; }
        public string DistributionChannel { get; set; }
        public string Division { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerGroup { get; set; }
        public string PriceListType { get; set; }
        public string CustomerPriceGroup { get; set; }
        public string MaterialPricingGroup { get; set; }
        public string SDDocumentCurrency { get; set; }
        public string MaterialNumber { get; set; }
        public string VolumeRebateGroup { get; set; }
        public string DepartureCountry { get; set; }
        public string PlantLocation_Region { get; set; }
        public string PlantLocation_Country { get; set; }
        public string PlantLocation_City { get; set; }
        public string DestinationCountry { get; set; }
        public string Region { get; set; }
        public string CountyCode { get; set; }
        public string CityCode { get; set; }
        public string TaxClassification1ForMaterial { get; set; }
        public string TaxClassification2ForMaterial { get; set; }
        public string TaxClassification3ForMaterial { get; set; }
        public string TaxClassification1ForCustomer { get; set; }
        public string TaxClassification2ForCustomer { get; set; }
        public string TaxClassification3ForCustomer { get; set; }
        public string AccountNumberOfVenderOrCreditor { get; set; }
        public string MaterialGroup_MARA { get; set; }
        public string Plant { get; set; }
        public string TaxOnSales_PurchaseCode { get; set; }
        public string SalesUnitOfMeasure { get; set; }
        public string InternationalArticleNumber { get; set; }
        public string IncotermsPart1 { get; set; }
        public string IncotermsPart2 { get; set; }
        public string CompanyCode { get; set; }
        public string TaxIndicatorForMaterialPurchasing { get; set; }
        public string TaxJurisdiction { get; set; }
        public string SalesOrganizationOfSalesOrder { get; set; }
        public string Customer { get; set; }
        public string VariantCondition { get; set; }
        public string CountryKey { get; set; }
        public string PrefernceZone { get; set; }
        public string SalesAndDistributionDocumentNumber { get; set; }
        public string ItemNumberOfSDDocument { get; set; }
        public string PricingReferenceMaterialOfMainItem { get; set; }
        public string MaterialPricingGroupofMainItem { get; set; }
        public string NumberOfSerialNumbers { get; set; }

        //Added for Myanmar
        public string ProductHierarchy { get; set; }
        public string BaseUnit { get; set; }
        public string VendorSubrange { get; set; }
        public string VendorMaterialGroup { get; set; }
        public string MaterialGroupHierarchy { get; set; }
        public string BillingType { get; set; }
        public string Promotion { get; set; }
        public string ExportingVendor { get; set; }
        public string MaterialOriginCountry { get; set; }
        public string ExportBaseCountry { get; set; }
        public string ForeignTradeCode { get; set; }
        public string ManufacturerNumber { get; set; }
        //Added for Myanmar

        public string ProductHierarchy1 { get; set; }
        public string ProductHierarchy2 { get; set; }
        public string ProductHierarchy3 { get; set; }
        public string SalesDistrict { get; set; }
        public string SalesGroup { get; set; }
        public string IndustryKey { get; set; }
        public string SalesOffice { get; set; }
        public string CustomerAttributeForConditionGroups { get; set; }
        public string VariableKeyLong { get; set; }
        public string CommodityCode { get; set; }
        public string CustomerGroupSalesOrg { get; set; }
        public string CustomerGroupDistributionChannel { get; set; }
        public string CustomerGroupDivision { get; set; }
        public string ConsumerTradeChannel { get; set; }
        public string SoldToParty { get; set; }
        public string Payer { get; set; }
        public string ShipToParty { get; set; }
        public string IndustryCode1 { get; set; }
        public string PricingRefMatl { get; set; }
        //
        public string MaterialGroup { get; set; }
        public string MaterialType { get; set; }
        public string ItemCategory { get; set; }
        public string DeliveryCountry { get; set; }
        //
        public List<ConditionRecordsEntity> E1KONH { get; set; }
    }
}
