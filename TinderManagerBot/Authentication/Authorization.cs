using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinderManagerBot.JsonData;
using Newtonsoft.Json;
using System.Windows.Controls;
using TinderManagerBot.AccountData;
using TinderManagerBot.JsonData.Core;
using TinderManagerBot.Profile;
using TinderManagerBot.Teasers;
using TinderManagerBot.MessagesInformation;
using TinderManagerBot.UserInfo;
using TinderManagerBot.MatchesNew;
using TinderManagerBot.MessagesWork;

namespace TinderManagerBot.Base
{
    class AuthorizationBot
    {
        private string profileId { get; set; }
        private string ApiToken { get; set; }
        private string SessionId { get; set; }
        private string NextPageToken { get; set; }

        private readonly AccountInformation accountInformation;

        private List<RootObject> rootObjectCollection { get; set; }
        private List<MatchesNewRootObject> matchesNewRootObjectCollection { get; set; }
        private List<string> likesUserIdCollection { get; set; }
        private List<MessageRootObject> messageRootObjectCollection { get; set; }

        /// <summary>
        /// //////////////////////////
        /// </summary>
        private RootObject RootObject { get; set; }
        private ProfileRootObject ProfileRootObject { get; set; }
        private TeasersRootObject TeasersRootObject { get; set; }
        private MessageRootObject MessageRootObject { get; set; }
        private UserInfoRootObject UserInfoRootObject { get; set; }
        private MatchesNewRootObject MatchesNewRootObject { get; set; }
        public AuthorizationBot()
        {
            accountInformation = new AccountInformation();
            rootObjectCollection = new List<RootObject>();
            matchesNewRootObjectCollection = new List<MatchesNewRootObject>();
            likesUserIdCollection = new List<string>();
            messageRootObjectCollection = new List<MessageRootObject>();
        }

        public async void fake()
        {
            string url = "https://teamo.ru/signin";

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36";
            CookieContainer cookieContainer = new CookieContainer();
            string data = $"user[_csrf_token]=eef6c8cd6f779769951d9c36d90ea982&user[email]=sada.sada.91@inbox.ru&user[password]=6c7577bfb2d6dc0&user[remember]=";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            request.ContentLength = dataBytes.Length;
            request.CookieContainer = cookieContainer;
            using (Stream stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(dataBytes, 0, dataBytes.Length);
            }
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            string content = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                content = reader.ReadToEnd();
                reader.Close();
            }
            response.Close();
            response.Dispose();

            //rootCrossAuthToken = JsonConvert.DeserializeObject<RootCrossAuthToken>(content);
            //crossAuthTokenId = rootCrossAuthToken.crossAuthTokenId;
        }

        public async void SendSmsToPhone(TextBox textBox, ListBox listBox)
        {
            string firstUrl = "https://api.gotinder.com/v2/auth/sms/send?auth_type=sms&locale=ru";
            string secondUrl = "https://api.gotinder.com/v2/auth/sms/validate?auth_type=sms&locale=ru";
            string threeUrl = "https://api.gotinder.com/v2/auth/login/sms?locale=ru";
            string phoneNumber = textBox.Text;
            HttpWebRequest request = WebRequest.Create(firstUrl) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36";
            CookieContainer cookieContainer = new CookieContainer();
            // string login = "asafawfaw@bk.ru";
            // string password = "MwPh2f7x";
            RootDataFirstRequest dataFirstRequest = new RootDataFirstRequest();
            dataFirstRequest.phone_number = phoneNumber;
            var firstJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataFirstRequest);

            // string data = $"login={login}&password={password}&s_post=QbAido4SpJuI9cct54vSwPPUxZbwn8QU&level=false";
            // byte[] dataBytes = Encoding.UTF8.GetBytes(firstJsonData);
            //  request.ContentLength = dataBytes.Length;
            request.CookieContainer = cookieContainer;
            using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            {

                // await writer.WriteAsync(dataBytes, 0, dataBytes.Length);
                await writer.WriteAsync(firstJsonData);
            }
            HttpWebResponse response = null;
            try
            {
                response = await request.GetResponseAsync() as HttpWebResponse;
                string content = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    content = await reader.ReadToEndAsync();
                    reader.Close();
                }
                listBox.Items.Add($"[{DateTime.Now}]: Код отправлен на номер");
                listBox.Items.Add($"[{DateTime.Now}]: Debug: {content}");
                response.Close();
                response.Dispose();
            }
            catch (WebException webException)
            {

                MessageBox.Show(webException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                response.Close();
                response.Dispose();
            }
        }

        public async void SmsValidate(string phoneNumberText, TextBox otpCode, ListBox logList)
        {
            string firstUrl = "https://api.gotinder.com/v2/auth/sms/send?auth_type=sms&locale=ru";
            string secondUrl = "https://api.gotinder.com/v2/auth/sms/validate?auth_type=sms&locale=ru";
            string threeUrl = "https://api.gotinder.com/v2/auth/login/sms?locale=ru";
            string phoneNumber = phoneNumberText;
            HttpWebRequest request = WebRequest.Create(secondUrl) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36";
            CookieContainer cookieContainer = new CookieContainer();
            // string login = "asafawfaw@bk.ru";
            // string password = "MwPh2f7x";
            RootDataSecond rootDataSecond = new RootDataSecond();
            rootDataSecond.otp_code = otpCode.Text;
            rootDataSecond.phone_number = phoneNumber;
            rootDataSecond.is_update = false;
            var firstJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(rootDataSecond);

            // string data = $"login={login}&password={password}&s_post=QbAido4SpJuI9cct54vSwPPUxZbwn8QU&level=false";
            // byte[] dataBytes = Encoding.UTF8.GetBytes(firstJsonData);
            //  request.ContentLength = dataBytes.Length;
            request.CookieContainer = cookieContainer;
            using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            {

                // await writer.WriteAsync(dataBytes, 0, dataBytes.Length);
                await writer.WriteAsync(firstJsonData);
            }
            HttpWebResponse response = null;
            try
            {
                response = await request.GetResponseAsync() as HttpWebResponse;
                string content = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    content = await reader.ReadToEndAsync();
                    reader.Close();
                }
                RootMetaData deserializeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootMetaData>(content);
                string refreshToken = deserializeObject.Data.refresh_token;
                logList.Items.Add($"[{DateTime.Now}]: Debug: {content}");
                response.Close();
                response.Dispose();
                Auth(phoneNumberText, refreshToken, logList);
            }
            catch (WebException webException)
            {
                MessageBox.Show(webException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                response.Close();
                response.Dispose();
            }

        }
        private async void Auth(string phone, string token, ListBox logList)
        {
            string firstUrl = "https://api.gotinder.com/v2/auth/sms/send?auth_type=sms&locale=ru";
            string secondUrl = "https://api.gotinder.com/v2/auth/sms/validate?auth_type=sms&locale=ru";
            string threeUrl = "https://api.gotinder.com/v2/auth/login/sms?locale=ru";
            string phoneNumber = phone;
            HttpWebRequest request = WebRequest.Create(threeUrl) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36";
            CookieContainer cookieContainer = new CookieContainer();
            // string login = "asafawfaw@bk.ru";
            // string password = "MwPh2f7x";
            RootDataThree dataFirstRequest = new RootDataThree();
            dataFirstRequest.phone_number = phoneNumber;
            dataFirstRequest.refresh_token = token;
            var firstJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataFirstRequest);
            request.CookieContainer = cookieContainer;
            using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                await writer.WriteAsync(firstJsonData);
            }
            HttpWebResponse response = null;
            try
            {
                response = await request.GetResponseAsync() as HttpWebResponse;
                string content = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    content = await reader.ReadToEndAsync();
                    reader.Close();
                }
                response.Close();
                response.Dispose();
                RootMetaData rootMetaData = JsonConvert.DeserializeObject<RootMetaData>(content);
                ApiToken = rootMetaData.Data.api_token;
                accountInformation.SaveTokenToFile(phoneNumber, ApiToken);
                logList.Items.Add($"Загружен в файл: {phoneNumber} - {ApiToken}");
            }
            catch (WebException webException)
            {
                MessageBox.Show(webException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                response.Close();
                response.Dispose();
            }
        }

        private async void GetRecomendation()
        {
            string url = "https://api.gotinder.com/v2/recs/core?locale=ru";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
            request.ContentType = "application/json";
            request.Headers.Add("X-Auth-Token", ApiToken);
            //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
            HttpWebResponse response = null;
            try
            {
                response = await request.GetResponseAsync() as HttpWebResponse;
                string content = string.Empty;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    content = await reader.ReadToEndAsync();
                    reader.Close();
                }
                response.Close();
                response.Dispose();
                RootObject = JsonConvert.DeserializeObject<JsonData.Core.RootObject>(content);


            }
            catch (WebException ex)
            {
                string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                MessageBox.Show(message);

            }
            finally
            {
                response.Close();
                response.Dispose();
            }

        }

        public async Task GetRecomendationCore()
        {
            List<string> tokenCollection = accountInformation.GetTokenFromFile();
            for (int i = 0; i < tokenCollection.Count; i++)
            {
                string urls = "https://api.gotinder.com/v2/recs/core?locale=ru";
                HttpWebRequest request = WebRequest.Create(urls) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
                request.ContentType = "application/json";
                request.Headers.Add("X-Auth-Token", tokenCollection[i]);
                //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
                HttpWebResponse response = null;
                try
                {
                    response = await request.GetResponseAsync() as HttpWebResponse;
                    string content = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        content = await reader.ReadToEndAsync();
                        reader.Close();
                    }
                    response.Close();
                    response.Dispose();
                    RootObject = JsonConvert.DeserializeObject<RootObject>(content);


                }

                catch (WebException ex)
                {
                    string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    MessageBox.Show("Превышен лимит",message);
      
                }
                finally
                {
                    response.Close();
                    response.Dispose();
                }
    
            }


        }

        public async void AddLike(int countLike, Label searchUser, Label likeUserCount)
        {
            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.
            List<string> tokenCollection = accountInformation.GetTokenFromFile();
            int likeCount = 0;
            int sNumber = 0;
            bool isCanselWork = false;
            string id = string.Empty;
            string content = string.Empty;
            int countResult = 0;
            string url = string.Empty;
            try
            {
                //await GetRecomendationCore();
                for (int n = 0; n < tokenCollection.Count; n++)
                {
                    //for (int j = 0; j < 25; j++)
                    //{

                    //    if (isCanselWork)
                    //    {
                    //        break;
                    //    }


                    //}

                    await GetRecomendationCore();
                    countResult += RootObject.Data.Results.Count;
                    //(countResult >= countLike || countResult <= countLike && !isCanselWork)
                    while (!isCanselWork)
                    {
                        await GetRecomendationCore();
                        searchUser.Content = $"Найдено пользователей : {RootObject.Data.Results.Count}";
                        for (int i = 0; i < RootObject.Data.Results.Count; i++)
                        {

                            id = RootObject.Data.Results[i].user._id;
                            sNumber = RootObject.Data.Results[i].s_number;
                            url = $"https://api.gotinder.com/like/{id}?locale=ru&s_number={sNumber}";
                            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                            request.Method = "GET";
                            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
                            request.ContentType = "application/json";
                            request.Proxy = new WebProxy("125.27.10.243:58219", false);
                            //ApiToken
                            request.Headers.Add("X-Auth-Token", tokenCollection[n]);
                            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                content = await reader.ReadToEndAsync();
                                reader.Close();
                            }
                            response.Close();
                            likeUserCount.Content = $"Поставлено лайков : {likeCount}";
                            response.Dispose();
                            likeCount++;
                            if (countLike == i || countLike == likeCount)
                            {
                                isCanselWork = true;
                                break;
                            }
                            if (isCanselWork)
                            {
                                break;
                            }

                        }
                    }
                  

                }
            }
            catch (NullReferenceException ex)
            {
               // MessageBox.Show("Превышен лимит", ex.Message);
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
          
            rootObjectCollection.Clear();
        }
        
        public async void GetProfileInfo()
        {
            string token = "2c7012ed-9231-4d25-9c60-d31dff8d683a";
            string url = "https://api.gotinder.com/v2/profile?include=likes%2Cplus_control%2Cproducts%2Cpurchase%2Cuser&locale=ru";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
            request.ContentType = "application/json";
            //ApiToken
            request.Headers.Add("X-Auth-Token", token);
            //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                content = await reader.ReadToEndAsync();
                reader.Close();
            }
            response.Close();
            response.Dispose();
            ProfileRootObject = JsonConvert.DeserializeObject<Profile.ProfileRootObject>(content);


        }

        
        //public async void GetProfileInfo(string semiToken, Image image, Label nameLbl, Label phoneLbl)
        //{
        //    string token = SelectApiKey(semiToken);
        //    string url = "https://api.gotinder.com/v2/profile?include=likes%2Cplus_control%2Cproducts%2Cpurchase%2Cuser&locale=ru";
        //   // string url = "https://api.gotinder.com/v2/profile?include=account%2Cboost%2Ccontact_cards%2Cemail_settings%2Cinstagram%2Clikes%2Cnotifications%2Cplus_control%2Cproducts%2Cpurchase%2Creadreceipts%2Cspotify%2Csuper_likes%2Ctinder_u%2Ctravel%2Ctutorials%2Cuser&locale=ru";
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Method = "GET";
        //    //Tinder/3.0.4 (iPhone; iOS 7.1; Scale/2.00)
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
        //    request.ContentType = "application/json";
        //    //ApiToken
        //    request.Headers.Add("X-Auth-Token", token);
        //    //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
        //    HttpWebResponse response = null;
        //    string content = string.Empty;
        //    try
        //    {
        //        response = await request.GetResponseAsync() as HttpWebResponse;
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //        {
        //            content = await reader.ReadToEndAsync();
        //            reader.Close();
        //        }
        //        response.Close();
        //        response.Dispose();
        //    }
        //    catch (WebException exp)
        //    {
        //        var errorMessage = new StreamReader(exp.Response.GetResponseStream()).ReadToEnd();
        //        MessageBox.Show(errorMessage);
        //        MessageBox.Show("Заного авторизуй аккаунт");
        //    }
        //    finally
        //    {
        //        response.Close();
        //        response.Dispose();
        //    }

        //    ProfileRootObject = JsonConvert.DeserializeObject<Profile.ProfileRootObject>(content);
        //    Uri uri = new Uri(ProfileRootObject.Data.User.photos[0].url);
        //    image.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
        //    profileId = ProfileRootObject.Data.User._id;
        //    nameLbl.Content = $"Имя: {ProfileRootObject.Data.User.name}";
        //    phoneLbl.Content = $"Номер: {ProfileRootObject.Data.User.phone_id}";

        //}

        public async void Update()
        {
            string url = "https://api.gotinder.com/updates?locale=ru";
            string token = "2c7012ed-9231-4d25-9c60-d31dff8d683a";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
            request.ContentType = "application/json";
            //ApiToken
            request.Headers.Add("X-Auth-Token", token);
            //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
            Updates.Update update = new Updates.Update();
            update.last_activity_date = DateTime.UtcNow;
            update.nudge = true;
            var serizalizeData = JsonConvert.SerializeObject(update);
            using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                await writer.WriteAsync(serizalizeData);
              //  writer.Close();
            }
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                content = await reader.ReadToEndAsync();
                reader.Close();
            }
            response.Close();
            response.Dispose();
            ProfileRootObject = JsonConvert.DeserializeObject<ProfileRootObject>(content);
        }

        //public async void GetPhotosLikes(string semiToken)
        //{
        //    string token = SelectApiKey(semiToken);
        //    string url = "https://api.gotinder.com/v2/fast-match/teasers?locale=ru";
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Method = "GET";
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
        //    request.ContentType = "application/json";
        //    //ApiToken
        //    request.Headers.Add("X-Auth-Token", token);
        //    //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
        //    HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
        //    string content = string.Empty;
        //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //    {
        //        content = await reader.ReadToEndAsync();
        //        reader.Close();
        //    }
        //    response.Close();
        //    response.Dispose();
        //    TeasersRootObject = JsonConvert.DeserializeObject<Teasers.TeasersRootObject>(content);

        //}

        public async Task ValidationAccount(Label invalidateAccountLbl)
        {
            List<string> tokenCollection = accountInformation.GetTokenFromFile();
            int countFileData = 0;
            List<int> invalidIds = new List<int>();
            string url = "https://api.gotinder.com/v2/matches?count=100&is_tinder_u=false&locale=ru&message=1";
            bool isValid = false;
            for (int i = 0; i < tokenCollection.Count; i++)
            {

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
                request.ContentType = "application/json";
                //ApiToken
                request.Headers.Add("X-Auth-Token", tokenCollection[i]);
                //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
                HttpWebResponse response = null;
                try
                {
                    response = await request.GetResponseAsync() as HttpWebResponse;
                    string content = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        content = await reader.ReadToEndAsync();
                        reader.Close();
                    }
                    response.Close();
                    response.Dispose();
                    matchesNewRootObjectCollection.Add(JsonConvert.DeserializeObject<MatchesNewRootObject>(content));

                }
                catch (ArgumentOutOfRangeException exp)
                {
                    MessageBox.Show("Аккаунт удален или его не существует", exp.Message);
                }
                catch (WebException argExcp)
                {
                    //MessageBox.Show(argExcp.Message);
                    invalidIds.Add(i);
                    isValid = true;
                    invalidateAccountLbl.Content = $"Недействительных аккаунтов : {i}";
                    //MessageBox.Show(argExcp.ToString());
                }
                //finally
                //{
                //    response.Close();
                //    response.Dispose();
                //}

            }
            if (isValid)
            {
                accountInformation.DeleteInvalidAccount(invalidIds);
            }
        }


        public async Task GetReceivedMessage()
        {
            //НАПИСАТЬ МЕТОД НА ВАЛИДАЦИЮ ТОКЕНА ККАУНТА
            List<string> tokenCollection = accountInformation.GetTokenFromFile();
            int countFileData = 0;
            string url = "https://api.gotinder.com/v2/matches?count=100&is_tinder_u=false&locale=ru&message=1";
            for (int i = 0; i < tokenCollection.Count; i++)
            {

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
                request.ContentType = "application/json";
                //ApiToken
                request.Headers.Add("X-Auth-Token", tokenCollection[i]);
                //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
                HttpWebResponse response = null;
                try
                {
                    response = await request.GetResponseAsync() as HttpWebResponse;
                    string content = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        content = await reader.ReadToEndAsync();
                        reader.Close();
                    }
                    response.Close();
                    response.Dispose();
                    matchesNewRootObjectCollection.Add(JsonConvert.DeserializeObject<MatchesNewRootObject>(content));
                    if (matchesNewRootObjectCollection[0].Data.next_page_token != null)
                    {
                        NextPageToken = matchesNewRootObjectCollection[0].Data.next_page_token;
                    }
                    if(matchesNewRootObjectCollection[0].Data.Matches.Count > 0)
                    {
                        for (int j = 0; j < matchesNewRootObjectCollection[i].Data.Matches.Count; j++)
                        {
                            likesUserIdCollection.Add(matchesNewRootObjectCollection[i].Data.Matches[j].id);
                        }
                    }
                    


                }
                catch (Exception exp)
                {
                    MessageBox.Show("Аккаунт удален или его не существует" , exp.Message);
                }
                finally
                {
                    response.Close();
                    response.Dispose();
                }

            }
            
        }

        public async void GetMessages()
        {

            List<string> tokenCollection = accountInformation.GetTokenFromFile();
            List<List<MessageRootObject>> botUserCollection = new List<List<MessageRootObject>>();
            //List<string> messageCollection = new List<string>();
            string url = string.Empty;
            //string url = $"https://api.gotinder.com/v2/matches/{MatchesNewRootObject.Data.Matches[userId].id}/messages?count=200&locale=ru&page_token=MjAxOS0xMS0wNVQxNzowMTowMC40Mzla";
            for (int n = 0; n < tokenCollection.Count; n++)
            {
                for (int i = 0; i < likesUserIdCollection.Count; i++)
                {
                    url = $"https://api.gotinder.com/v2/matches/{likesUserIdCollection[i]}/messages?count=100&locale=ru";
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
                    request.ContentType = "application/json";
                    //ApiToken
                    request.Headers.Add("X-Auth-Token", tokenCollection[n]);
                    //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
                    HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                    string content = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        content = await reader.ReadToEndAsync();
                        reader.Close();
                    }
                    response.Close();
                    messageRootObjectCollection.Add(JsonConvert.DeserializeObject<MessageRootObject>(content));
                    response.Dispose();
                }
                botUserCollection.Add(messageRootObjectCollection);
                //for (int j = 0; j < messageRootObjectCollection[n].Data.Messages.Count; j++)
                //{
                //    messageCollection.Add(MessageRootObject.Data.Messages[j].message);
                //}
            }
            
           

            //Обратно, так как месседжи в json в обратном порядке, не от начала до конца
            //for (int i = messageCollection.Count - 1; i >= 0; i--)
            //{
            //    messageList.Items.Add(messageCollection[i]);
            //}
        }
       

        //public async void GetUserInformation(string selectToken, int nameLikeUserId,Image image,Label nameLbl, Label bioLbl, Label distanceLbl)
        //{
        //    string token = SelectApiKey(selectToken);
        //    if (nameLikeUserId == -1)
        //    {
        //        nameLikeUserId = 0;
        //    }
        //    string id = MatchesNewRootObject.Data.Matches[nameLikeUserId].person._id;
        //    string url = $"https://api.gotinder.com/user/{id}?locale=ru";
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Method = "GET";
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
        //    request.ContentType = "application/json";
        //    //ApiToken
        //    request.Headers.Add("X-Auth-Token", token);
        //    //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
        //    HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
        //    string content = string.Empty;
        //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //    {
        //        content = await reader.ReadToEndAsync();
        //        reader.Close();
        //    }
        //    response.Close();
        //    MessageRootObject = JsonConvert.DeserializeObject<MessagesInformation.MessageRootObject>(content);
        //    response.Dispose();
        //    UserInfoRootObject = JsonConvert.DeserializeObject<UserInfo.UserInfoRootObject>(content);
        //    Uri uriPhoto = new Uri(UserInfoRootObject.Results.Photos[0].url);
        //    image.Source = new System.Windows.Media.Imaging.BitmapImage(uriPhoto);
        //    nameLbl.Content = $"Имя: {UserInfoRootObject.Results.name}";
        //    bioLbl.Content = $"Биография: {UserInfoRootObject.Results.bio}";
        //    distanceLbl.Content = $"{UserInfoRootObject.Results.distance_mi} км от вас";
            
        //}

        //public async Task GetUserInformation(string selectToken, int nameLikeUserId)
        //{
        //    string token = SelectApiKey(selectToken);
        //    if (nameLikeUserId == -1)
        //    {
        //        nameLikeUserId = 0;
        //    }
        //    string id = MatchesNewRootObject.Data.Matches[nameLikeUserId].person._id;
        //    string url = $"https://api.gotinder.com/user/{id}?locale=ru";
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Method = "GET";
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
        //    request.ContentType = "application/json";
        //    //ApiToken
        //    request.Headers.Add("X-Auth-Token", token);
        //    //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
        //    HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
        //    string content = string.Empty;
        //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //    {
        //        content = await reader.ReadToEndAsync();
        //        reader.Close();
        //    }
        //    response.Close();
        //    MessageRootObject = JsonConvert.DeserializeObject<MessagesInformation.MessageRootObject>(content);
        //    response.Dispose();
        //    UserInfoRootObject = JsonConvert.DeserializeObject<UserInfo.UserInfoRootObject>(content);
           

        //}

        private void GenerateSessionId()
        {
            #region Other algorithm
            //if(sessionId.Length != 37)
            //{
            //    sessionId += dataGenerate[random.Next(0, dataGenerate.Length)];
            //    for (int k = 0; k < separationGenerate.Length; k++)
            //    {
            //        if (sessionId.Length == separationGenerate[k])
            //        {
            //            sessionId += "-";
            //        }

            //    }
            //}
            #endregion

            string numbers = "0123456789";
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string dataGenerate = numbers + letters;
            Random random = new Random();
            SessionId = string.Empty;
            for (int i = 0; i < dataGenerate.Length; i++)
            {
                //Добавлено по 5 вместо 4 из за черты разделение
                switch (SessionId.Length)
                {
                    case 8:
                        SessionId += "-";
                        break;
                    case 13:
                        SessionId += "-";
                        break;
                    case 18:
                        SessionId += "-";
                        break;
                    case 23:
                        SessionId += "-";
                        break;
                    case 36:
                        SessionId += "-";
                        break;
                    default:
                        SessionId += dataGenerate[random.Next(0, dataGenerate.Length)];
                        break;
                }

            }
        }

        public async void SendMessage(Label writtenMessagesLbl)
        {
            List<string> tokenCollection = accountInformation.GetTokenFromFile();
            WriteMessage writeMessage = new WriteMessage();
            string message = writeMessage.GetMessageFromFile();
            int count = 0;
            for (int i = 0; i < tokenCollection.Count; i++)
            {
                for (int k = 0; k < likesUserIdCollection.Count; k++)
                {
                    //GenerateSessionId();
                    string url = $"https://api.gotinder.com/user/matches/{likesUserIdCollection[k]}?locale=ru";
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "POST";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
                    request.ContentType = "application/json";
                    Matches.MatchesRootObject MatchesRootObject = new Matches.MatchesRootObject();
                    string s = likesUserIdCollection[k];
                    string a = tokenCollection[i];
                    MatchesRootObject.matchId = likesUserIdCollection[k];
                    MatchesRootObject.message = message ?? ":)";
                    //MatchesRootObject.otherId = UserInfoRootObject.Results._id;
                    //MatchesRootObject.sessionId = SessionId;
                    //MatchesRootObject.tempMessageId = "0.20981060242048644";
                    //MatchesRootObject.userId = ProfileRootObject.Data.User._id;
                    var firstJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(MatchesRootObject);
                    //ApiToken
                    request.Headers.Add("X-Auth-Token", tokenCollection[i]);
                    //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
                    using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
                    {
                        await writer.WriteAsync(firstJsonData);
                        writer.Close();
                    }
                    HttpWebResponse response = null;
                    try
                    {
                        response  = await request.GetResponseAsync() as HttpWebResponse;
                        string content = string.Empty;
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            content = await reader.ReadToEndAsync();
                            reader.Close();
                        }
                        response.Close();
                        response.Dispose();
                    }
                    catch(WebException ex)
                    {
                       // MessageBox.Show(ex.Message);
                    }
                   
                    count++;
                    writtenMessagesLbl.Content = $"Написано сообщений: {count}/{likesUserIdCollection.Count}";
                }
               
            }
           
           // Matches.MatchesRootObject MatchesRootObject = JsonConvert.DeserializeObject<Matches.MatchesRootObject>(content);
        
        }

        public async void tester()
        {
            GenerateSessionId();
            string url = $"https://api.gotinder.com/user/matches/5cb62e6b78d4ba15004104415dc19e011f484f0100816246?locale=ru";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
            request.ContentType = "application/json";
            Matches.MatchesRootObject MatchesRootObject = new Matches.MatchesRootObject();
            MatchesRootObject.matchId = "5cb62e6b78d4ba15004104415dc19e011f484f0100816246";
            MatchesRootObject.message = "вот так";
            var firstJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(MatchesRootObject);
            //ApiToken
            request.Headers.Add("X-Auth-Token", "5bcfabf3-ce38-472b-9a0f-d246c7efb945");
            //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
            using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                await writer.WriteAsync(firstJsonData);
                writer.Close();
            }
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                content = await reader.ReadToEndAsync();
                reader.Close();
            }
            response.Close();
            response.Dispose();
        }

        public async void GenerateToken()
        {
            string url = "https://api.gotinder.com/ws/generate?locale=ru";
            List<string> tokenCollection = accountInformation.GetTokenFromFile();
            for (int i = 0; i < tokenCollection.Count; i++)
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
                request.ContentType = "application/json";
                //ApiToken
                request.Headers.Add("X-Auth-Token", tokenCollection[i]);
                //   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
                HttpWebResponse response = null;
                try
                {
                    response = await request.GetResponseAsync() as HttpWebResponse;
                    string content = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        content = await reader.ReadToEndAsync();
                        reader.Close();
                    }
                    response.Close();
                    response.Dispose();
                }
                catch (Exception ex)
                {


                }
            }
           
         
        }

        public async void BannedUser(int userId)
        {
            //string url = $"https://api.gotinder.com/report/{MatchesNewRootObject.Data.Matches[userId].id}?locale=ru";
            //string token = "";
            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //request.Method = "POST";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36";
            //request.ContentType = "application/json";
            ////BanUser.Banned banned = new BanUser.Banned();
            ////banned.cause = 2;
            ////var firstJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(banned);
            ////ApiToken
            //request.Headers.Add("X-Auth-Token", token);
            ////   requestSecond.Headers.Add("X-Supported-Image-Formats", "webp,jpeg");
            //using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            //{
            //    await writer.WriteAsync(firstJsonData);
            //    writer.Close();
            //}
            //HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            //string content = string.Empty;
            //using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //{
            //    content = await reader.ReadToEndAsync();
            //    reader.Close();
            //}
            //response.Close();
            //response.Dispose();

        }

        private void AddDisLike()
        {

        }
    }
}
