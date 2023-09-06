using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI.Domain.WeatherForecastResponse;

namespace WeatherForecastAPI.DomainServices
{
    public abstract class BaseDayMessageBuilder : IDayMessageBuilder
    {
        protected static SecretClient _secretClient;
        private static string key_vault_url = Environment.GetEnvironmentVariable("KEY_VAULT_URL");

        public static void SetSecretClient(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }
        static BaseDayMessageBuilder()
        {
            if (_secretClient == null)
            {
                try
                {
                    _secretClient = new SecretClient(vaultUri: new Uri(key_vault_url), credential: new DefaultAzureCredential());
                }catch (Exception ex)
                {
                    //Do Nothing - will happen only while running test cases
                }
            }
        }
        public abstract IList<string> GetAllMessages(IList<List> responseDataList);
    }
}
