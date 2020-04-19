using AdventureTourManagement.Interface;
using AdventureTourManagement.Models.Shopping;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SecureAccess;
using SecureAccess.Helper;
using SecureAccess.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventureTourManagement.Utility
{
    public class ConnectService : IConnect
    {
        EncryptionDecryption encryption;
        ILogger<ConnectService> logger;

        public ConnectService(IServiceProvider provider, ILogger<ConnectService> logger)
        {
            this.logger = logger;
            var secureaccessFactory = new SecureAccessFactory();
            encryption = secureaccessFactory.CreateInstance(provider).SecureAccess.GetEncryptionDecryption;
        }
        public async Task<string> GetConnectionAsync()
        {
            var response = await GetNetworkConnectDetailsAsync();
            string result = JsonConvert.SerializeObject(response);
            return result;
        }

        private async Task<NetworkKeyDTO> GetNetworkConnectDetailsAsync()
        {
            logger.LogInformation("GetNetworkConnectDetailsAsync started");
            try
            {
                NetworkKeyDTO result = new NetworkKeyDTO();
                string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                logger.LogInformation(assemblyFile);
                var assemblyPath = string.Empty;
                if (assemblyFile.Contains("\\"))
                {
                    assemblyPath = assemblyFile.Substring(0, assemblyFile.LastIndexOf('\\')) + "\\Utility\\" + "ATMConnect.txt";
                    logger.LogInformation(assemblyPath);
                }
                else
                {
                    assemblyPath = assemblyFile.Substring(0, assemblyFile.LastIndexOf('/')) + "/Utility/" + "ATMConnect.txt";
                    logger.LogInformation(assemblyPath);
                }
                if (File.Exists(assemblyPath))
                {
                    var credentials = (await File.ReadAllLinesAsync(assemblyPath)).ToList();
                    logger.LogInformation(credentials.Aggregate((x,y)=>x+","+y));
                    result.EncryptedUserName = credentials[0];
                    result.EncryptedUserMessage = "encryptionkey";
                    result.EncryptedKey = credentials[1];
                    result.EncryptedMessage = "encryptionkey";

                }

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            
        }
    }
}
