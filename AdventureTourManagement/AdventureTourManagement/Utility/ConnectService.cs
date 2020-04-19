using AdventureTourManagement.Interface;
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
        public ConnectService(IServiceProvider provider)
        {
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
            NetworkKeyDTO result = new NetworkKeyDTO();
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            var assemblyPath = string.Empty;
            if (assemblyFile.Contains("\\"))
            {
                assemblyPath = assemblyFile.Substring(0, assemblyFile.LastIndexOf('\\')) + "\\Utility\\" + "ATMConnect.txt";
            }
            else
            {
                assemblyPath = assemblyFile.Substring(0, assemblyFile.LastIndexOf('/')) + "/Utility/" + "ATMConnect.txt";
            }
            if (File.Exists(assemblyPath))
            {
                var credentials = (await File.ReadAllLinesAsync(assemblyPath)).ToList();

                result.EncryptedUserName = credentials[0];
                result.EncryptedUserMessage = "encryptionkey";
                result.EncryptedKey = credentials[1];
                result.EncryptedMessage = "encryptionkey";

            }

            return result;
        }
    }
}
