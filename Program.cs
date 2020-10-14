using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoConsolePostMessage
{
    class Program
    {
        static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5000/")
        };
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SNOW Software!");
            Console.WriteLine();
            Call_Postman();
        }


        static void Call_Postman()
        {
            Console.WriteLine("Pelase enter your name : ");
            var username = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Enter your message: ");
            var message = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(message) && (!string.IsNullOrWhiteSpace(username)))
            {
                try
                {
                    var postresult = SendMessageAsync(string.Format("\r\n Username : {0}  \r\n Message : {1}   \r\n Time : {2} \r\n", username, message, DateTime.Now.ToString("t"))).GetAwaiter().GetResult();
                    if (postresult.IsSuccessStatusCode)
                    {
                        Console.WriteLine();
                        Console.WriteLine("message sent successfully");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue..");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Your message failed,  " + ex.Message);
                    Console.WriteLine();
                }
                Console.ReadKey();
                Console.WriteLine();
                Call_Postman();
            }
            else
            {

                Console.WriteLine("Please enter your name/message");
                Console.WriteLine();
                Call_Postman();
            }
        }


        /// <summary>
        /// Used async to let app to continue with other task if something to run
        /// </summary>
        /// <param name="message">user's message </param>
        /// <returns></returns>
        static async Task<HttpResponseMessage> SendMessageAsync(string message)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return await PostMessage(message);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">user's message</param>
        /// <returns>http client details</returns>
        static async Task<HttpResponseMessage> PostMessage(object message)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                                       "api/values", message);
            response.EnsureSuccessStatusCode();
            return response;

        }
    }
}