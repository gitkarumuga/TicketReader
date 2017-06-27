using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicketReader.Models;

namespace TicketReader.Services
{
    public class ApiService
    {

        #region Methods
        public async Task<Response> LoginUser(string urlBase, string servicePrefix, string controller, object model)
        {
            try
            {
                // Convert "model" object into corresponding JSON
                var contentJSON = JsonConvert.SerializeObject(model);
                // Convert JSON object string into UTF-8 encoding.
                var content = new StringContent(contentJSON, Encoding.UTF8, "application/json");

                // Form the complete the url data to perform HTTP call
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                // Perform the HTTP Post Call to communicate with Backend
                var response = await client.PostAsync(url, content);

                // Check the HTTP Header Response code
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                // Obtain the JSON object String from HTTP response Content
                var resultJSON = await response.Content.ReadAsStringAsync();
                // Convert the JSON object into indicated object type (i.e. "T")
                var result = JsonConvert.DeserializeObject<UserInfo>(resultJSON);

                // Return the successfull response
                return new Response
                {
                    IsSuccess = true,
                    Message = "Login success.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        } 

        public async Task<Response> GetTicket<T>(string urlBase, string servicePrefix, string controller, string ticketCode)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, ticketCode);
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(resultJSON);
                return new Response
                {
                    IsSuccess = true,
                    Message = "OK",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> SaveTicket<T>(string urlBase, string servicePrefix, string controller, T model)
        {

            try
            {
                // Convert "model" object into corresponding JSON
                var contentJSON = JsonConvert.SerializeObject(model);
                // Convert JSON object string into UTF-8 encoding.
                var content = new StringContent(contentJSON, Encoding.UTF8, "aplication/json");

                // Form the complete the url data to perform HTTP call
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                // Perform the HTTP Post Call to communicate with Backend
                var response = await client.PostAsync(url, content);

                // Check the HTTP Header Response code
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                // Obtain the JSON object String from HTTP response Content
                var resultJSON = await response.Content.ReadAsStringAsync();
                // Convert the JSON object into indicated object type (i.e. "T")
                var result = JsonConvert.DeserializeObject<T>(resultJSON);

                // Return the successfull response
                return new Response
                {
                    IsSuccess = true,
                    Message = "Success.",
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        #endregion

    }
}
