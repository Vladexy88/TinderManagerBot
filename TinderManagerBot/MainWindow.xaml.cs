using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TinderManagerBot.AccountData;
using TinderManagerBot.Base;
using TinderManagerBot.MessagesWork;

namespace TinderManagerBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthorizationBot AuthorizationBot { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AuthorizationBot = new AuthorizationBot();
            WriteMessage writeMessage = new WriteMessage();
        }

        private void SendCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationBot.SendSmsToPhone(phoneTextB, logListB);
        }

        private void UseCodeBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationBot.SmsValidate(phoneTextB.Text, codeConfirmTextB, logListB);
            AccountData.AccountInformation accountInformation = new AccountData.AccountInformation();
            accountInformation.ShowAccountInformation(allAccountLbl, activeAccountLbl);
        }

        private async void StartWorkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(countLikeAddedTextB.Text))
            {
                int countLike = Convert.ToInt32(countLikeAddedTextB.Text);
                if (countLike != 0)
                {
                   //Повторная авторизация через почту без телефона
                    await AuthorizationBot.ValidationAccount(invalidateAccountLbl);
                    AuthorizationBot.AddLike(countLike, searchUsersLbl, likeUserCount: addedLikeLbl);
                    await AuthorizationBot.GetReceivedMessage();
                    AuthorizationBot.SendMessage(writtenMessagesLbl);
                }
            }
            
        }

        private void StartWritedMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            //await AuthorizationBot.GetReceivedMessage();
            //AuthorizationBot.SendMessage(writtenMessagesLbl);
            AuthorizationBot.GenerateToken();
        }
    }
}
