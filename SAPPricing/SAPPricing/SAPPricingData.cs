using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SAPPricing
    {
    public class SAPPricingData
    {
        private readonly IConfiguration _configuration;
        public SAPPricingData(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int SaveFilterSegmentsdata(FilterSegmentsEntity filterSegmentsdata)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration["DatabaseConnectionString"]);
                SqlCommand cmd = new SqlCommand("FilterSegments_save", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConditionRecordNumber", filterSegmentsdata.ConditionRecordNumber);
                cmd.Parameters.AddWithValue("@UsageOfTheConditionTable", filterSegmentsdata.UsageOfTheConditionTable);
                cmd.Parameters.AddWithValue("@ConditionTable", filterSegmentsdata.ConditionTable);
                cmd.Parameters.AddWithValue("@Application", filterSegmentsdata.Application);
                cmd.Parameters.AddWithValue("@ConditionType", filterSegmentsdata.ConditionType);
                cmd.Parameters.AddWithValue("@VariableKey", filterSegmentsdata.VariableKey);
                cmd.Parameters.AddWithValue("@SalesOrganization", filterSegmentsdata.SalesOrganization);
                cmd.Parameters.AddWithValue("@DistributionChannel", filterSegmentsdata.DistributionChannel);
                cmd.Parameters.AddWithValue("@Division", filterSegmentsdata.Division);
                cmd.Parameters.AddWithValue("@CustomerNumber", filterSegmentsdata.CustomerNumber);
                cmd.Parameters.AddWithValue("@CustomerGroup", filterSegmentsdata.CustomerGroup);
                cmd.Parameters.AddWithValue("@PriceListType", filterSegmentsdata.PriceListType);
                cmd.Parameters.AddWithValue("@CustomerPriceGroup", filterSegmentsdata.CustomerPriceGroup);
                cmd.Parameters.AddWithValue("@MaterialPricingGroup", filterSegmentsdata.MaterialPricingGroup);
                cmd.Parameters.AddWithValue("@SDDocumentCurrency", filterSegmentsdata.SDDocumentCurrency);
                cmd.Parameters.AddWithValue("@MaterialNumber", filterSegmentsdata.MaterialNumber);
                cmd.Parameters.AddWithValue("@DepartureCountry", filterSegmentsdata.DepartureCountry);
                cmd.Parameters.AddWithValue("@PlantLocation_Region", filterSegmentsdata.PlantLocation_Region);
                cmd.Parameters.AddWithValue("@PlantLocation_Country", filterSegmentsdata.PlantLocation_Country);
                cmd.Parameters.AddWithValue("@DestinationCountry", filterSegmentsdata.DestinationCountry);
                cmd.Parameters.AddWithValue("@Region", filterSegmentsdata.Region);
                cmd.Parameters.AddWithValue("@CountyCode", filterSegmentsdata.CountyCode);
                cmd.Parameters.AddWithValue("@CityCode", filterSegmentsdata.CityCode);
                // missing fields added
                cmd.Parameters.AddWithValue("@MaterialGroup", filterSegmentsdata.MaterialGroup);
                cmd.Parameters.AddWithValue("@MaterialType", filterSegmentsdata.MaterialType);
                cmd.Parameters.AddWithValue("@ItemCategory", filterSegmentsdata.ItemCategory);
                cmd.Parameters.AddWithValue("@DeliveryCountry", filterSegmentsdata.DeliveryCountry);
                //
                cmd.Parameters.AddWithValue("@AccountNumberOfVenderOrCreditor", filterSegmentsdata.AccountNumberOfVenderOrCreditor);
                cmd.Parameters.AddWithValue("@Plant", filterSegmentsdata.Plant);
                cmd.Parameters.AddWithValue("@TaxOnSales_PurchaseCode", filterSegmentsdata.TaxOnSales_PurchaseCode);
                cmd.Parameters.AddWithValue("@SalesUnitOfMeasure", filterSegmentsdata.SalesUnitOfMeasure);
                cmd.Parameters.AddWithValue("@InternationalArticleNumber", filterSegmentsdata.InternationalArticleNumber);
                cmd.Parameters.AddWithValue("@CompanyCode", filterSegmentsdata.CompanyCode);
                cmd.Parameters.AddWithValue("@TaxJurisdiction", filterSegmentsdata.TaxJurisdiction);
                cmd.Parameters.AddWithValue("@SalesOrganizationOfSalesOrder", filterSegmentsdata.SalesOrganizationOfSalesOrder);
                cmd.Parameters.AddWithValue("@Customer", filterSegmentsdata.Customer);
                cmd.Parameters.AddWithValue("@VariantCondition", filterSegmentsdata.VariantCondition);
                cmd.Parameters.AddWithValue("@CountryKey", filterSegmentsdata.CountryKey);
                cmd.Parameters.AddWithValue("@PrefernceZone", filterSegmentsdata.PrefernceZone);
                cmd.Parameters.AddWithValue("@SalesAndDistributionDocumentNumber", filterSegmentsdata.SalesAndDistributionDocumentNumber);
                cmd.Parameters.AddWithValue("@ItemNumberOfSDDocument", filterSegmentsdata.ItemNumberOfSDDocument);
                cmd.Parameters.AddWithValue("@PricingReferenceMaterialOfMainItem", filterSegmentsdata.PricingReferenceMaterialOfMainItem);
                cmd.Parameters.AddWithValue("@MaterialPricingGroupofMainItem", filterSegmentsdata.MaterialPricingGroupofMainItem);
                cmd.Parameters.AddWithValue("@SalesDistrict", filterSegmentsdata.SalesDistrict);
                cmd.Parameters.AddWithValue("@SalesGroup", filterSegmentsdata.SalesGroup);
                cmd.Parameters.AddWithValue("@IndustryKey", filterSegmentsdata.IndustryKey);
                cmd.Parameters.AddWithValue("@SalesOffice", filterSegmentsdata.SalesOffice);
                cmd.Parameters.AddWithValue("@CustomerAttributeForConditionGroups", filterSegmentsdata.CustomerAttributeForConditionGroups);
                cmd.Parameters.AddWithValue("@VariableKeyLong", filterSegmentsdata.VariableKeyLong);
                cmd.Parameters.AddWithValue("@CommodityCode", filterSegmentsdata.CommodityCode);
                cmd.Parameters.AddWithValue("@CustomerGroupSalesOrg", filterSegmentsdata.CustomerGroupSalesOrg);
                cmd.Parameters.AddWithValue("@CustomerGroupDistributionChannel", filterSegmentsdata.CustomerGroupDistributionChannel);
                cmd.Parameters.AddWithValue("@CustomerGroupDivision", filterSegmentsdata.CustomerGroupDivision);
                cmd.Parameters.AddWithValue("@ConsumerTradeChannel", filterSegmentsdata.ConsumerTradeChannel);
                cmd.Parameters.AddWithValue("@SoldToParty", filterSegmentsdata.SoldToParty);
                cmd.Parameters.AddWithValue("@Payer", filterSegmentsdata.Payer);
                cmd.Parameters.AddWithValue("@ShipToParty", filterSegmentsdata.ShipToParty);
                cmd.Parameters.Add("@returnObj", SqlDbType.BigInt);
                cmd.Parameters["@returnObj"].Direction = ParameterDirection.Output;
                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();
                if (k != 0)
                {
                    return k;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                //var errorLog = new ErrorLogEntity();
                //errorLog.PipeLineName = "Pricing";
                //errorLog.ParentNodeName = "FilterSegments_save";
                //errorLog.ErrorMessage = ex.Message;
                //SaveErrorLogData(errorLog);
                Logger logger = new Logger(_configuration);
                logger.ErrorLogData(ex, ex.Message);
                return 0;
            }
        }

        public List<ConditionItemsEntity> ConditionItemSelect(string conditionRecordNumber, string conditionType, string variableKey)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration["DatabaseConnectionString"]);
                SqlCommand cmd = new SqlCommand("ConditionItems_Select", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConditionRecordNumber", conditionRecordNumber);
                cmd.Parameters.AddWithValue("@ConditionType", conditionType);
                cmd.Parameters.AddWithValue("@VariableKey", variableKey);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<ConditionItemsEntity> conditionItemsList = new List<ConditionItemsEntity>();
                while (dr.Read())
                {
                    ConditionItemsEntity conditionItems = new ConditionItemsEntity();
                    conditionItems.Id = dr["ID"] == DBNull.Value ? 0 : (Int64)dr["ID"];
                    conditionItems.ConditionRecordNumber = dr["ConditionRecordNumber"] == DBNull.Value ? "" : (string)dr["ConditionRecordNumber"];
                    conditionItems.ConditionType = dr["ConditionType"] == DBNull.Value ? "" : (string)dr["ConditionType"];
                    conditionItems.ScaleType = dr["ScaleType"] == DBNull.Value ? "" : (string)dr["ScaleType"];
                    conditionItems.VariableKey = dr["VariableKey"] == DBNull.Value ? "" : (string)dr["VariableKey"];
                    conditionItems.ConditionValidFromDate = dr["ValidFrom"] == DBNull.Value ? "" : (string)dr["ValidFrom"];
                    conditionItems.ConditionValidToDate = dr["ValidTo"] == DBNull.Value ? "" : (string)dr["ValidTo"];
                    conditionItems.ConditionScaleQuantity = dr["ConditionScaleQuantity"] == DBNull.Value ? "" : (string)dr["ConditionScaleQuantity"];
                    conditionItems.ScaleValue = dr["ScaleValue"] == DBNull.Value ? "" : (string)dr["ScaleValue"];
                    conditionItems.CalculationTypeForCondition = dr["CalculationTypeForCondition"] == DBNull.Value ? "" : (string)dr["CalculationTypeForCondition"];
                    conditionItems.ConditionAmountOrPercentageRate = dr["ConditionAmountOrPercentageRate"] == DBNull.Value ? "" : (string)dr["ConditionAmountOrPercentageRate"];
                    conditionItems.CurrencyOrPercentageRateUnit = dr["CurrencyOrPercentageRateUnit"] == DBNull.Value ? "" : (string)dr["CurrencyOrPercentageRateUnit"];
                    conditionItems.ConditionPricingUnit = dr["ConditionPricingUnit"] == DBNull.Value ? "" : (string)dr["ConditionPricingUnit"];
                    conditionItems.ConditionUnit = dr["ConditionUnit"] == DBNull.Value ? "" : (string)dr["ConditionUnit"];
                    conditionItems.NumeratorForUnitConversion = dr["NumeratorForUnitConversion"] == DBNull.Value ? "" : (string)dr["NumeratorForUnitConversion"];
                    conditionItems.DenomineratorForUnitConversion = dr["DenomineratorForUnitConversion"] == DBNull.Value ? "" : (string)dr["DenomineratorForUnitConversion"];
                    conditionItems.ConditionAmountLowerLimit = dr["ConditionAmountLowerLimit"] == DBNull.Value ? "" : (string)dr["ConditionAmountLowerLimit"];
                    conditionItems.ConditionAmountUpperLimit = dr["ConditionAmountUpperLimit"] == DBNull.Value ? "" : (string)dr["ConditionAmountUpperLimit"];
                    conditionItems.ConditionCurrencyForCumulationFields = dr["ConditionCurrencyForCumulationFields"] == DBNull.Value ? "" : (string)dr["ConditionCurrencyForCumulationFields"];
                    conditionItems.DeletionIndicatorForConditionItem = dr["DeletionIndicatorForConditionItem"] == DBNull.Value ? "" : (string)dr["DeletionIndicatorForConditionItem"];
                    conditionItems.ConditionItemIndex = dr["ConditionItemIndex"] == DBNull.Value ? "" : (string)dr["ConditionItemIndex"];
                    conditionItems.AccrualAmount = dr["AccrualAmount"] == DBNull.Value ? "" : (string)dr["AccrualAmount"];
                    conditionItems.AdditionalValueDays = dr["AdditionalValueDays"] == DBNull.Value ? "" : (string)dr["AdditionalValueDays"];
                    conditionItems.FixedValueDate = dr["FixedValueDate"] == DBNull.Value ? "" : (string)dr["FixedValueDate"];
                    conditionItems.MaxNumOfSalesOrdersPerConditionRecord = dr["MaxNumOfSalesOrdersPerConditionRecord"] == DBNull.Value ? "" : (string)dr["MaxNumOfSalesOrdersPerConditionRecord"];
                    conditionItems.ConditionBaseValueMin = dr["ConditionBaseValueMin"] == DBNull.Value ? "" : (string)dr["ConditionBaseValueMin"];
                    conditionItems.ConditionBaseValueMax = dr["ConditionBaseValueMax"] == DBNull.Value ? "" : (string)dr["ConditionBaseValueMax"];
                    conditionItems.MaximumConditionValue = dr["MaximumConditionValue"] == DBNull.Value ? "" : (string)dr["MaximumConditionValue"];
                    conditionItems.NumberOfIncrementalScale = dr["NumberOfIncrementalScale"] == DBNull.Value ? "" : (string)dr["NumberOfIncrementalScale"];
                    conditionItems.ScaleNumberForPricing = dr["ScaleNumberForPricing"] == DBNull.Value ? "" : (string)dr["ScaleNumberForPricing"];
                    conditionItems.ScaleBasis = dr["ScaleBasis"] == DBNull.Value ? "" : (string)dr["ScaleBasis"];
                    conditionItemsList.Add(conditionItems);
                }
                con.Close();
                return conditionItemsList;
            }
            catch (Exception ex)
            {
                //var errorLog = new ErrorLogEntity();
                //errorLog.PipeLineName = "Pricing";
                //errorLog.ParentNodeName = "ConditionItems_Select";
                //errorLog.ErrorMessage = ex.Message;
                //SaveErrorLogData(errorLog);
                Logger logger = new Logger(_configuration);
                logger.ErrorLogData(ex, ex.Message);
                return null;
            }
        }

        public List<ConditionItemsEntity> ConditionItemSelectOver(string conditionType, string variableKey, string validFrom, string validTo)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration["DatabaseConnectionString"]);
                SqlCommand cmd = new SqlCommand("ConditionItems_Select_Over", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConditionType", conditionType);
                cmd.Parameters.AddWithValue("@VariableKey", variableKey);
                cmd.Parameters.AddWithValue("@ValidFrom", validFrom);
                cmd.Parameters.AddWithValue("@ValidTo", validTo);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<ConditionItemsEntity> conditionItemsList = new List<ConditionItemsEntity>();
                while (dr.Read())
                {
                    ConditionItemsEntity conditionItems = new ConditionItemsEntity();
                    conditionItems.Id = dr["ID"] == DBNull.Value ? 0 : (Int64)dr["ID"];
                    conditionItems.ConditionRecordNumber = dr["ConditionRecordNumber"] == DBNull.Value ? "" : (string)dr["ConditionRecordNumber"];
                    conditionItems.ConditionType = dr["ConditionType"] == DBNull.Value ? "" : (string)dr["ConditionType"];
                    conditionItems.ScaleType = dr["ScaleType"] == DBNull.Value ? "" : (string)dr["ScaleType"];
                    conditionItems.VariableKey = dr["VariableKey"] == DBNull.Value ? "" : (string)dr["VariableKey"];
                    conditionItems.ConditionValidFromDate = dr["ValidFrom"] == DBNull.Value ? "" : (string)dr["ValidFrom"];
                    conditionItems.ConditionValidToDate = dr["ValidTo"] == DBNull.Value ? "" : (string)dr["ValidTo"];
                    conditionItems.ConditionScaleQuantity = dr["ConditionScaleQuantity"] == DBNull.Value ? "" : (string)dr["ConditionScaleQuantity"];
                    conditionItems.ScaleValue = dr["ScaleValue"] == DBNull.Value ? "" : (string)dr["ScaleValue"];
                    conditionItems.CalculationTypeForCondition = dr["CalculationTypeForCondition"] == DBNull.Value ? "" : (string)dr["CalculationTypeForCondition"];
                    conditionItems.ConditionAmountOrPercentageRate = dr["ConditionAmountOrPercentageRate"] == DBNull.Value ? "" : (string)dr["ConditionAmountOrPercentageRate"];
                    conditionItems.CurrencyOrPercentageRateUnit = dr["CurrencyOrPercentageRateUnit"] == DBNull.Value ? "" : (string)dr["CurrencyOrPercentageRateUnit"];
                    conditionItems.ConditionPricingUnit = dr["ConditionPricingUnit"] == DBNull.Value ? "" : (string)dr["ConditionPricingUnit"];
                    conditionItems.ConditionUnit = dr["ConditionUnit"] == DBNull.Value ? "" : (string)dr["ConditionUnit"];
                    conditionItems.NumeratorForUnitConversion = dr["NumeratorForUnitConversion"] == DBNull.Value ? "" : (string)dr["NumeratorForUnitConversion"];
                    conditionItems.DenomineratorForUnitConversion = dr["DenomineratorForUnitConversion"] == DBNull.Value ? "" : (string)dr["DenomineratorForUnitConversion"];
                    conditionItems.ConditionAmountLowerLimit = dr["ConditionAmountLowerLimit"] == DBNull.Value ? "" : (string)dr["ConditionAmountLowerLimit"];
                    conditionItems.ConditionAmountUpperLimit = dr["ConditionAmountUpperLimit"] == DBNull.Value ? "" : (string)dr["ConditionAmountUpperLimit"];
                    conditionItems.ConditionCurrencyForCumulationFields = dr["ConditionCurrencyForCumulationFields"] == DBNull.Value ? "" : (string)dr["ConditionCurrencyForCumulationFields"];
                    conditionItems.DeletionIndicatorForConditionItem = dr["DeletionIndicatorForConditionItem"] == DBNull.Value ? "" : (string)dr["DeletionIndicatorForConditionItem"];
                    conditionItems.ConditionItemIndex = dr["ConditionItemIndex"] == DBNull.Value ? "" : (string)dr["ConditionItemIndex"];
                    conditionItems.AccrualAmount = dr["AccrualAmount"] == DBNull.Value ? "" : (string)dr["AccrualAmount"];
                    conditionItems.AdditionalValueDays = dr["AdditionalValueDays"] == DBNull.Value ? "" : (string)dr["AdditionalValueDays"];
                    conditionItems.FixedValueDate = dr["FixedValueDate"] == DBNull.Value ? "" : (string)dr["FixedValueDate"];
                    conditionItems.MaxNumOfSalesOrdersPerConditionRecord = dr["MaxNumOfSalesOrdersPerConditionRecord"] == DBNull.Value ? "" : (string)dr["MaxNumOfSalesOrdersPerConditionRecord"];
                    conditionItems.ConditionBaseValueMin = dr["ConditionBaseValueMin"] == DBNull.Value ? "" : (string)dr["ConditionBaseValueMin"];
                    conditionItems.ConditionBaseValueMax = dr["ConditionBaseValueMax"] == DBNull.Value ? "" : (string)dr["ConditionBaseValueMax"];
                    conditionItems.MaximumConditionValue = dr["MaximumConditionValue"] == DBNull.Value ? "" : (string)dr["MaximumConditionValue"];
                    conditionItems.NumberOfIncrementalScale = dr["NumberOfIncrementalScale"] == DBNull.Value ? "" : (string)dr["NumberOfIncrementalScale"];
                    conditionItems.ScaleNumberForPricing = dr["ScaleNumberForPricing"] == DBNull.Value ? "" : (string)dr["ScaleNumberForPricing"];
                    conditionItems.ScaleBasis = dr["ScaleBasis"] == DBNull.Value ? "" : (string)dr["ScaleBasis"];
                    conditionItemsList.Add(conditionItems);
                }
                con.Close();
                return conditionItemsList;
            }
            catch (Exception ex)
            {
                //var errorLog = new ErrorLogEntity();
                //errorLog.PipeLineName = "Pricing";
                //errorLog.ParentNodeName = "ConditionItems_Select_Over";
                //errorLog.ErrorMessage = ex.Message;
                //SaveErrorLogData(errorLog);
                Logger logger = new Logger(_configuration);
                logger.ErrorLogData(ex, ex.Message);
                return null;
            }
        }

        public int SaveConditionItemsdata(ConditionItemsEntity conditionItemsdata)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration["DatabaseConnectionString"]);
                SqlCommand cmd = new SqlCommand("ConditionItems_Save", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConditionRecordNumber", conditionItemsdata.ConditionRecordNumber);
                cmd.Parameters.AddWithValue("@ConditionType", conditionItemsdata.ConditionType);
                cmd.Parameters.AddWithValue("@ScaleType", conditionItemsdata.ScaleType);
                cmd.Parameters.AddWithValue("@VariableKey", conditionItemsdata.VariableKey);
                cmd.Parameters.AddWithValue("@ValidFrom", conditionItemsdata.ConditionValidFromDate);
                cmd.Parameters.AddWithValue("@ValidTo", conditionItemsdata.ConditionValidToDate);
                cmd.Parameters.AddWithValue("@ConditionScaleQuantity", conditionItemsdata.ConditionScaleQuantity);
                cmd.Parameters.AddWithValue("@ScaleValue", conditionItemsdata.ScaleValue);
                cmd.Parameters.AddWithValue("@CalculationTypeForCondition", conditionItemsdata.CalculationTypeForCondition);
                cmd.Parameters.AddWithValue("@ConditionAmountOrPercentageRate", conditionItemsdata.ConditionAmountOrPercentageRate);
                cmd.Parameters.AddWithValue("@CurrencyOrPercentageRateUnit", conditionItemsdata.CurrencyOrPercentageRateUnit);
                cmd.Parameters.AddWithValue("@ConditionPricingUnit", conditionItemsdata.ConditionPricingUnit);
                cmd.Parameters.AddWithValue("@ConditionUnit", conditionItemsdata.ConditionUnit);
                cmd.Parameters.AddWithValue("@NumeratorForUnitConversion", conditionItemsdata.NumeratorForUnitConversion);
                cmd.Parameters.AddWithValue("@DenomineratorForUnitConversion", conditionItemsdata.DenomineratorForUnitConversion);
                cmd.Parameters.AddWithValue("@ConditionAmountLowerLimit", conditionItemsdata.ConditionAmountLowerLimit);
                cmd.Parameters.AddWithValue("@ConditionAmountUpperLimit", conditionItemsdata.ConditionAmountUpperLimit);
                cmd.Parameters.AddWithValue("@ConditionCurrencyForCumulationFields", conditionItemsdata.ConditionCurrencyForCumulationFields);
                cmd.Parameters.AddWithValue("@DeletionIndicatorForConditionItem", conditionItemsdata.DeletionIndicatorForConditionItem);
                cmd.Parameters.AddWithValue("@ConditionItemIndex", conditionItemsdata.ConditionItemIndex);
                cmd.Parameters.AddWithValue("@AccrualAmount", conditionItemsdata.AccrualAmount);
                cmd.Parameters.AddWithValue("@AdditionalValueDays", conditionItemsdata.AdditionalValueDays);
                cmd.Parameters.AddWithValue("@FixedValueDate", (string.IsNullOrEmpty(conditionItemsdata.FixedValueDate) ? "" : DateTime.Parse(conditionItemsdata.FixedValueDate).ToString("yyyyMMdd")).ToString());
                cmd.Parameters.AddWithValue("@MaxNumOfSalesOrdersPerConditionRecord", conditionItemsdata.MaxNumOfSalesOrdersPerConditionRecord);
                cmd.Parameters.AddWithValue("@ConditionBaseValueMin", conditionItemsdata.ConditionBaseValueMin);
                cmd.Parameters.AddWithValue("@ConditionBaseValueMax", conditionItemsdata.ConditionBaseValueMax);
                cmd.Parameters.AddWithValue("@MaximumConditionValue", conditionItemsdata.MaximumConditionValue);
                cmd.Parameters.AddWithValue("@NumberOfIncrementalScale", conditionItemsdata.NumberOfIncrementalScale);
                cmd.Parameters.AddWithValue("@ScaleNumberForPricing", conditionItemsdata.ScaleNumberForPricing);
                cmd.Parameters.AddWithValue("@ConditionScaleValue", conditionItemsdata.ConditionScaleValue);
                cmd.Parameters.AddWithValue("@ScaleBasis", conditionItemsdata.ScaleBasis);
                cmd.Parameters.AddWithValue("@isDeleted", conditionItemsdata.IsDeleted);
                cmd.Parameters.Add("@returnObj", SqlDbType.BigInt);
                cmd.Parameters["@returnObj"].Direction = ParameterDirection.Output;
                con.Open();
                int k = cmd.ExecuteNonQuery();
                con.Close();
                if (k != 0)
                {
                    return k;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                //var errorLog = new ErrorLogEntity();
                //errorLog.PipeLineName = "Pricing";
                //errorLog.ParentNodeName = "ConditionItems_Save";
                //errorLog.ErrorMessage = ex.Message;
                //SaveErrorLogData(errorLog);
                Logger logger = new Logger(_configuration);
                logger.ErrorLogData(ex, ex.Message);
                return 0;
            }
        }
       
        //public void SaveErrorLogData(ErrorLogEntity errorLogData)
        //{
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(_configuration["DatabaseConnectionString"]);
        //        SqlCommand cmd = new SqlCommand("ErrorLogDetails_save", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@PipeLineName", errorLogData.PipeLineName);
        //        cmd.Parameters.AddWithValue("@FileName", errorLogData.FileName);
        //        cmd.Parameters.AddWithValue("@ParentNodeName", errorLogData.ParentNodeName);
        //        cmd.Parameters.AddWithValue("@ErrorMessage", errorLogData.ErrorMessage);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();


        //    }
        //    catch (Exception ex)
        //    {
        //        Logger logger = new Logger(_configuration);
        //        logger.ErrorLogData(ex, ex.Message);
        //    }
        //}
    }
}
