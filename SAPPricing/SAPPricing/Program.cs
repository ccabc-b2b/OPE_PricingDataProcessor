
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace SAPPricing
    {
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddAzureKeyVault(new Uri(Properties.Settings.Default.KeyVaultURI), new DefaultAzureCredential());
                IConfiguration configuration = builder.Build();
                SAPPricingJsonData pricingJsonData = new SAPPricingJsonData(configuration);
                pricingJsonData.LoadPricingData();
            }
            catch (Exception ex)
            {
                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddAzureKeyVault(new Uri(Properties.Settings.Default.KeyVaultURI), new DefaultAzureCredential());
                IConfiguration configuration = builder.Build();
                Logger logger = new Logger(configuration);
                logger.ErrorLogData(ex, ex.Message);
            }
        }
    }
}
