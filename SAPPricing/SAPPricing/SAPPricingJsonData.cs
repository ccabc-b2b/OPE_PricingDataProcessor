using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SAPPricing
    {
    public class SAPPricingJsonData
    {
        readonly string containerName = Properties.Settings.Default.ContainerName;
        readonly string blobDirectoryPrefix = Properties.Settings.Default.BlobDirectoryPrefix;
        readonly string destblobDirectoryPrefix = Properties.Settings.Default.DestDirectory;
        private static IConfiguration _configuration;
        private SAPPricingData pricingData;
        public SAPPricingJsonData(IConfiguration configuration)
        {
            _configuration = configuration;
            pricingData = new SAPPricingData(_configuration);
        }
 
        public void LoadPricingData()
        {

            try
            {
                List<BlobEntity> blobList = new List<BlobEntity>();
                var storageKey = _configuration["StorageKey"];

                var storageAccount = CloudStorageAccount.Parse(storageKey);
                var myClient = storageAccount.CreateCloudBlobClient();
                var container = myClient.GetContainerReference(containerName);

                var list = container.ListBlobs().OfType<CloudBlobDirectory>().ToList();

                var blobListDirectory = list[0].ListBlobs().OfType<CloudBlobDirectory>().ToList();

                foreach (var blobDirectory in blobListDirectory)
                {
                    if (blobDirectory.Prefix == blobDirectoryPrefix)
                    {
                        //int sapPricingCount = 0;
                        foreach (var blobFile in blobDirectory.ListBlobs().OfType<CloudBlockBlob>())
                        {
                            //if (sapPricingCount != 1000)
                            //{
                            BlobEntity blobDetails = new BlobEntity();
                            string[] blobName = blobFile.Name.Split(new char[] { '/' });
                            string[] filename = blobName[2].Split(new char[] { '.' }); 
                            string[] fileDateTime = filename[0].Split(new char[] { '_' });
                            string fileCreatedDateTime = fileDateTime[1] + fileDateTime[2];
                            string formatString = "yyyyMMddHHmmss";
                            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobFile.Name);
                            blobDetails.Blob = blockBlob;
                            blobDetails.FileName = blobName[2];
                            blobDetails.FileCreatedDate = DateTime.ParseExact(fileCreatedDateTime, formatString, null);
                            blobDetails.FileData = blockBlob.DownloadTextAsync().Result;
                            blobDetails.BlobName = blobFile.Name;

                            blobList.Add(blobDetails);
                           
                        }
                        blobList = blobList.OrderByDescending(x => x.FileCreatedDate.Date).ThenByDescending(x => x.FileCreatedDate.TimeOfDay).ToList();
                    }
                }
                foreach (var blobDetails in blobList)
                {

                    CheckRequiredFields(blobDetails, container);

                }
            }
            catch (StorageException ex)
            {
                var errorLog = new ErrorLogEntity();
                errorLog.PipeLineName = "Pricing";
                errorLog.ErrorMessage = ex.Message;
                pricingData.SaveErrorLogData(errorLog);
                Logger logger = new Logger(_configuration);
                logger.ErrorLogData(ex, ex.Message);
            }
        }

        public void CheckRequiredFields(BlobEntity blobDetails, CloudBlobContainer container)
        {
            try
            {
                List<string> errors = new List<string>();
                if (string.IsNullOrEmpty(blobDetails.FileData))
                {
                    blobDetails.Status = "Error";
                    var errorLog = new ErrorLogEntity();
                    errorLog.PipeLineName = "Pricing";
                    errorLog.FileName = blobDetails.FileName;
                    errorLog.ErrorMessage = "File is empty";
                    pricingData.SaveErrorLogData(errorLog);
                    Logger logger = new Logger(_configuration);
                    logger.ErrorLogData(null, "File is empty");
                }
                else
                {
                    SAPPricing pricingdataList = JsonConvert.DeserializeObject<SAPPricing>(blobDetails.FileData, new JsonSerializerSettings
                    {
                        Error = delegate (object sender, ErrorEventArgs args)
                        {
                            errors.Add(args.ErrorContext.Error.Message);
                            args.ErrorContext.Handled = true;
                        },
                        Converters = { new IsoDateTimeConverter() }
                    });
                    Dictionary<string, int> returnData = new Dictionary<string, int>();
                    if (pricingdataList.payload == null)
                        {
                        returnData.Add("Pricing", 0);
                        var errorLog = new ErrorLogEntity();
                        errorLog.PipeLineName = "Pricing";
                        errorLog.FileName = blobDetails.FileName;
                        errorLog.ErrorMessage ="Payload is null";
                        pricingData.SaveErrorLogData(errorLog);
                        Logger logger = new Logger(_configuration);
                        logger.ErrorLogData(null, errors[0]);
                        }
                    else
                        {
                        var countE1KONP = 0;
                        foreach (var pricingdata in pricingdataList.payload)
                            {
                            if (pricingdata == null)
                                {
                                blobDetails.Status = "Error";
                                var errorLog = new ErrorLogEntity();
                                errorLog.PipeLineName = "Pricing";
                                errorLog.FileName = blobDetails.FileName;
                                errorLog.ErrorMessage = errors[0];
                                pricingData.SaveErrorLogData(errorLog);
                                Logger logger = new Logger(_configuration);
                                logger.ErrorLogData(null, errors[0]);
                                }
                            else
                                {
                                //insert to table 
                                if (string.IsNullOrEmpty(pricingdata.SalesOrganization))
                                    {
                                    pricingdata.SalesOrganization = "OPE1";
                                    pricingdata.CustomerGroupSalesOrg = "OPE1";
                                    }

                                if (pricingdata.E1KONH != null)
                                    {
                                    if (pricingdata.E1KONH.Count != 0)
                                        {
                                        //E1KONP 
                                        var E1KONH_List = pricingdata.E1KONH;
                                        var conditionItemsList = new List<ConditionItemsEntity>();
                                        var E1KONH_List_Disitnct = new List<ConditionRecordsEntity>();
                                        var Disitnct_ConditionRecordNumber = new List<string>();
                                        var conditionScaleList = new List<ConditionScaleDBEntity>();
                                        var distCount = 0;
                                        foreach (var condition in E1KONH_List)
                                            {
                                            if (E1KONH_List_Disitnct.Count == 0)
                                                {
                                                E1KONH_List_Disitnct.Add(condition);
                                                }
                                            else if (E1KONH_List_Disitnct[distCount].ConditionRecordNumber != condition.ConditionRecordNumber)
                                                {
                                                E1KONH_List_Disitnct.Add(condition);
                                                distCount++;
                                                }
                                            }


                                        foreach (var e1KONH in E1KONH_List_Disitnct)
                                            {
                                            var IdList = new List<Int64>();
                                            var conditionItem = pricingData.ConditionItemSelect(e1KONH.ConditionRecordNumber, pricingdata.ConditionType, pricingdata.VariableKey);
                                            foreach (var conditions in conditionItem)
                                                {
                                                IdList.Add(conditions.Id);
                                                var isDeleted = '1';
                                                conditions.IsDeleted = isDeleted.ToString();
                                                conditionItemsList.Add(conditions);
                                                }
                                            var conditionItem_Master = pricingData.ConditionItemSelectOver(pricingdata.ConditionType, pricingdata.VariableKey, e1KONH.ConditionValidFromDate, e1KONH.ConditionValidToDate);

                                            foreach (var conditions in conditionItem_Master)
                                                {
                                                if (!IdList.Contains(conditions.Id))
                                                    {
                                                    var isDeleted = '1';
                                                    conditions.IsDeleted = isDeleted.ToString();
                                                    conditionItemsList.Add(conditions);
                                                    }

                                                }

                                            }

                                        foreach (var E1KONH in E1KONH_List)
                                            {
                                            if (E1KONH.E1KONP != null && E1KONH.E1KONP.Count > 0)
                                                {
                                                // Check E1KONW
                                                var deleteFlag = 'X';
                                                foreach (var item in E1KONH.E1KONP)
                                                    {

                                                    if (item.DeletionIndicatorForConditionItem == deleteFlag.ToString())
                                                        {
                                                        var isDeleted = '1';
                                                        var E1KONP = item;
                                                        E1KONP.ConditionRecordNumber = E1KONH.ConditionRecordNumber;
                                                        E1KONP.ConditionType = pricingdata.ConditionType;
                                                        E1KONP.VariableKey = pricingdata.VariableKey;
                                                        E1KONP.ConditionValidFromDate = E1KONH.ConditionValidFromDate;
                                                        E1KONP.ConditionValidToDate = E1KONH.ConditionValidToDate;
                                                        E1KONP.IsDeleted = isDeleted.ToString();
                                                        conditionItemsList.Add(E1KONP);
                                                        }
                                                    else
                                                        {
                                                        if (!string.IsNullOrEmpty(E1KONH.ConditionValidFromDate) && !string.IsNullOrEmpty(E1KONH.ConditionValidToDate))
                                                            {
                                                            var isDeleted = '0';
                                                            var E1KONP = item;
                                                            E1KONP.ConditionRecordNumber = E1KONH.ConditionRecordNumber;
                                                            E1KONP.ConditionType = pricingdata.ConditionType;
                                                            E1KONP.VariableKey = pricingdata.VariableKey;
                                                            E1KONP.ConditionValidFromDate = E1KONH.ConditionValidFromDate;
                                                            E1KONP.ConditionValidToDate = E1KONH.ConditionValidToDate;
                                                            E1KONP.IsDeleted = isDeleted.ToString();
                                                            E1KONP.E1KONW = item.E1KONW;
                                                            if (!string.IsNullOrEmpty(E1KONP.ScaleType))
                                                                {
                                                                conditionItemsList.Add(E1KONP);
                                                                }
                                                            else
                                                                {
                                                                blobDetails.Status = "Error";
                                                                var errorLog = new ErrorLogEntity();
                                                                errorLog.PipeLineName = "Pricing";
                                                                errorLog.FileName = blobDetails.FileName;
                                                                errorLog.ParentNodeName = "chechRequiredField";
                                                                errorLog.ErrorMessage = "ScaleType is null";
                                                                pricingData.SaveErrorLogData(errorLog);
                                                                Logger logger = new Logger(_configuration);
                                                                logger.ErrorLogData(null, errorLog.ErrorMessage);
                                                                break;
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                            }


                                        foreach (var E1KONH in conditionItemsList)
                                            {
                                            countE1KONP++;
                                            if (E1KONH.FixedValueDate == "00000000")
                                                {
                                                E1KONH.FixedValueDate = null;
                                                }
                                            var return_E1KONP = pricingData.SaveConditionItemsdata(E1KONH);
                                            returnData.Add("E1KONP" + countE1KONP, return_E1KONP);
                                            if (return_E1KONP != 0)
                                                {
                                                pricingdata.ConditionRecordNumber = E1KONH.ConditionRecordNumber;
                                                var return_E1KOMG2 = pricingData.SaveFilterSegmentsdata(pricingdata);
                                                returnData.Add("E1KOMG" + countE1KONP, return_E1KOMG2);
                                                }
                                            }

                                        }
                                    }

                                foreach (var returnvalue in returnData)
                                    {
                                    if (returnvalue.Value == 0)
                                        {
                                        blobDetails.Status = "Error";
                                        var errorLog = new ErrorLogEntity();
                                        errorLog.PipeLineName = "Pricing";
                                        errorLog.FileName = blobDetails.FileName;
                                        errorLog.ParentNodeName = returnvalue.Key;
                                        pricingData.SaveErrorLogData(errorLog);
                                        Logger logger = new Logger(_configuration);
                                        logger.ErrorLogData(null, errorLog.ErrorMessage);
                                        break;
                                        }
                                    else
                                        {
                                        blobDetails.Status = "Success";
                                        }
                                    }
                                }
                            }
                        }

                }

                var destDirectory = destblobDirectoryPrefix + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                MoveFile(blobDetails, container, destDirectory);
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogEntity();
                errorLog.PipeLineName = "Pricing";
                errorLog.ParentNodeName = "CheckRequiredFields";
                errorLog.ErrorMessage = ex.Message;
                errorLog.FileName = blobDetails.FileName;
                pricingData.SaveErrorLogData(errorLog);
                blobDetails.Status = "Error";
                var destDirectory = destblobDirectoryPrefix + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day;
                MoveFile(blobDetails, container, destDirectory);
            }
        }

        public void MoveFile(BlobEntity blob, CloudBlobContainer destContainer, string destDirectory)
        {
            CloudBlockBlob destBlob;
            try
            {
                if (blob.Blob == null)
                    throw new Exception("Source blob cannot be null.");

                if (!destContainer.Exists())
                    throw new Exception("Destination container does not exist.");

                //Copy source blob to destination container
                string name = blob.FileName;
                if (destDirectory != "" && blob.Status == "Success")
                    destBlob = destContainer.GetBlockBlobReference(destDirectory + "\\Success\\" + name);
                else
                    destBlob = destContainer.GetBlockBlobReference(destDirectory + "\\Error\\" + name);

                destBlob.StartCopy(blob.Blob);
                //remove source blob after copy is done.
                blob.Blob.Delete();
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLogEntity();
                errorLog.PipeLineName = "Pricing";
                errorLog.FileName = blob.FileName;
                errorLog.ParentNodeName = "Pricing move";
                errorLog.ErrorMessage = ex.Message;
                pricingData.SaveErrorLogData(errorLog);
                Logger logger = new Logger(_configuration);
                logger.ErrorLogData(ex, ex.Message);
            }
        }
    }
}
