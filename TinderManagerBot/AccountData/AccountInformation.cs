using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TinderManagerBot.AccountData
{
    class AccountInformation
    { 
        public AccountInformation()
        {

        }

        public List<string> GetTokenFromFile()
        {
            //AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt"
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt");
            string[] distinctData = data.Distinct().ToArray();
            List<string> releaseData = new List<string>();
            for (int i = 0; i < distinctData.Length; i++)
            {
                releaseData.Add(SelectApiToken(distinctData[i]));
            }
            return releaseData;
        }

        public void DeleteInvalidAccount(List<int> ids)
        {
            var data = new List<string>(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt"));
    
            for (int i = 0; i < ids.Count; i++)
            {
                data[ids[i]] = null;
            }
            data.RemoveAll(t => t == null);
            File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt", data);
        }

        public void ShowAccountInformation(Label allAccountsLoaded, Label activeAccounts)
        {
            //AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt"
            string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt");
            string[] distinctData = data.Distinct().ToArray();
            allAccountsLoaded.Content = $"Всего аккаунтов: {distinctData.Length}";
            activeAccounts.Content = $"Активные аккаунты: {distinctData.Length}";


        }

        public void SaveTokenToFile(string phone, string token)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt", $"{phone}:{token}\n");
            //string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt");
            //string[] distinctData = data.Distinct().ToArray();

        }

        private string SelectApiToken(string data)
        {
            string subStringData = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                if (data.IndexOf(":") != -1)
                {
                    int startIndexSeparation = data.IndexOf(":");
                    int lengthStr = data.Length;
                    subStringData = data.Substring(startIndexSeparation + 1, lengthStr - startIndexSeparation - 1);
                }

            }
            return subStringData;
        }
    }
}
