using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using todolist_mvc.Models;
using todolist_mvc.Model;
using NuGet.Common;
using System.Web;
using static todolist_mvc.Model.UserTask;

namespace todolist_mvc.ApiRequests
{
    public class Api
    {
        private static readonly HttpClient client;

        static Api()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://localhost:8080/")
            };
        }

        public Api(string token = "")
        {
            if (token != null && token != "")
            {
                SetTokenForClientAsync(token); // Установка токена для HttpClient
            }   
        }
        public async Task UserRegistrationAsync(string login, string password)
        {
            UserRegistration registrationModel = new UserRegistration()
            {
                UserPassword = password,
                UserName = login,
            };

            var json = JsonConvert.SerializeObject(registrationModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Registration", data);
        }

        public async Task<List<Priority>> GetPriorityList()
        {
            List<Priority> priotiryList;

            var response = await client.GetAsync("api/Priority");
            priotiryList = JsonConvert.DeserializeObject<List<Priority>>(response.Content.ReadAsStringAsync().Result);

            return priotiryList;
        }
        public async Task<List<UserTask>> GetTasks()
        {
            List<UserTask> tasks;

            var response = await client.GetAsync("api/tasks");
            tasks = JsonConvert.DeserializeObject<List<UserTask>>(response.Content.ReadAsStringAsync().Result, new MyDateOnlyConverter());

            if (tasks == null)
            {
                tasks = new List<UserTask>(0);
            }
            return tasks;
        }
        public async Task<UserTask> GetTask(int? id)
        {
            UserTask task;

            var response = await client.GetAsync($"api/tasks/{id}");
            task = JsonConvert.DeserializeObject<UserTask>(response.Content.ReadAsStringAsync().Result, new MyDateOnlyConverter());

            return task;
        }

        public async Task EditTask(UserTask userTask)
        {
            var json = JsonConvert.SerializeObject(userTask);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/tasks", data);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Задача обновлена");
                return;
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                return;
            }
            return;
        }
        public async Task AddTask(UserTask userTask)
        {
            var json = JsonConvert.SerializeObject(userTask);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/tasks", data);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Задача добавлена");
                return;
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                return;
            }
        }
        public async Task DeleteTask(int id)
        {
            var response = await client.DeleteAsync($"api/tasks/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Задача Удалена");
                return;
            }
            else
            {
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                return;
            }
        }

        public async Task<string> UserLoginAsync(string login, string password)
        {
            string token = String.Empty;
            UserLogin loginModel = new UserLogin()
            {
                Login = login,
                Password = password,
            };

            var json = JsonConvert.SerializeObject(loginModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Login", data);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                return String.Empty;
            }
            else
            {

                token = response.Content.ReadAsStringAsync().Result;
                return token;
            }
        }
        /// <summary>
        /// Token добавляется к httpclient
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task SetTokenForClientAsync(string token)
        {
            if (token == null || token == "")
            {
                return;
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public class MyDateOnlyConverter : JsonConverter<DateOnly>
        {
            public override bool CanRead => true;
            public override bool CanWrite => true;

            public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                string dateString = (string)reader.Value;
                return DateOnly.Parse(dateString);
            }

            public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
            }
        }
    }
}
