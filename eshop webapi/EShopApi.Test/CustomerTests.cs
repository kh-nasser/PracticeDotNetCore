using eshop_webapi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;

namespace EShopApi.Test
{
    [TestClass]
    public class CustomerTests
    {
        private HttpClient _client;
        public CustomerTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [TestMethod]
        public void DummyTest()
        {
            //arrange
            Random random = new Random();

            //act
            var ran1 =  random.Next(0,50);
            var ran2 =  random.Next(51,100);
            Trace.Write(new[] { ran1, ran2});

            //assert
            Assert.AreNotEqual(ran1, ran2);
        }

        [TestMethod]
        public void CustomerGetAllTest()
        {
            //arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/customers");

            //act
            var response = _client.SendAsync(request).Result;
            Trace.Write(response);
            //System.Diagnostics.Debug.WriteLine(response.Content);

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void CustomerGetOneTest(int id)
        {   
            //arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/Api/Customers/{id}"); 
            
            //act
            var response = _client.SendAsync(request).Result;

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void CustomerPostTest()
        {
            //arrange
            var request = new HttpRequestMessage(HttpMethod.Post, $"/Api/Customers/");
            //act
            var response = _client.SendAsync(request).Result;
            //assert

            Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }
    }
}