using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestProject2024.HelperClass;

namespace TestProject2024.Tests
{
    public  class BaseTest
    {
        private HttpClientHelper _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new HttpClientHelper();
        }

        [Test]

        public async Task GetAllUsers()
        {
            var responseFromHelper = _handler.GET("users");
        }

        [Test]

        public async Task CreateUser()
        {
            var requestBody = _handler.CreateUserRequestBody("testmest");

            var response = await _handler.POST("users", requestBody);

            Assert.AreEqual("Created", response.StatusCode.ToString()); 
        }

        [Test]

        public async Task GetSpecificUser()
        {
            var createUser = await _handler.CreateNewUserAsync("testusers");
            

        }
    }
}
