using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderManagerBot.MessagesWork
{
    class WriteMessage
    {
        public string GetMessageFromFile()
        {
            //AppDomain.CurrentDomain.BaseDirectory + "Account tokens.txt"
           // string[] data = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Сообщения.txt");
            string content = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Сообщения.txt");
            return content;
        }
    }
}
