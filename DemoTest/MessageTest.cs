using Demo.Api.Controllers;
using Demo.DataAccess;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoTest
{
    [TestClass]
    public class MessageTest
    {
        // Http Request to API for message value.
        [TestMethod]
        public async Task Controller_Get_Should_Return_OKAsync()
        {
            var client = new RestClient("https://localhost:44342/api");
            var request = new RestRequest("message", Method.GET);
              // execute the request
            var response = await client.ExecuteAsync(request);
            var value = JsonConvert.DeserializeObject<List<Message>>(response.Content);
            Assert.AreEqual(value[0].MessageValue, "Hello World");
        }
    }

    // Helper extension to inject Callback
    public static class RestClientExtensions
    {
        public static async Task<RestResponse> ExecuteAsync(this RestClient client, RestRequest request)
        {
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            RestRequestAsyncHandle handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            return (RestResponse)(await taskCompletion.Task);
        }
    }
}
