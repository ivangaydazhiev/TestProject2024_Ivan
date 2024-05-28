using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using TestProject2024.HelperClass;
using TestProject2024.Models;

namespace TestProject2024.Tests
{
    public  class BaseTest
    {
        private HttpClientHelper _handler;
        private string _name;

        [SetUp]
        public void Setup()
        {
            _handler = new HttpClientHelper();
             _name =  Guid.NewGuid().ToString();
        }

        [Test]

        public async Task GetAllUsers()
        {
            var responseFromHelper = await _handler.GET("users");

            responseFromHelper.StatusCode.ToString().Should().Be("OK");   
        }

        [Test]

        public async Task CreateUser()
        {
          
            var requestBody = _handler.CreateUserRequestBody(_name); 

            var response = await _handler.POST("users", requestBody);

            response.StatusCode.ToString().Should().Be("Created");
        } 
        
        [Test]

        public async Task GetSpecificUser()
        {
            var requestBody =  _handler.CreateUserRequestBody(_name);
            var request = await _handler.POST("users", requestBody);
            var responseBody = request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseBody.Result);
            
            var response = await _handler.GetSpecificUser("users", user.Id);

            using (new AssertionScope())
            {
                response.StatusCode.ToString().Should().Be("OK");
                user.Name.Should().Be(_name);
                user.Id.ToString().Should().NotBeNullOrWhiteSpace();
            }
          
           
        }

        [Test]

        public async Task UpdateUser()
        {
            var requestBody = _handler.CreateUserRequestBody(_name);
            var request = await _handler.POST("users", requestBody);
            var responseBody = request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse>(responseBody.Result);

            var response = await  _handler.UPDATE("users", requestBody, user.Id);

            using (new AssertionScope())
            {
                response.StatusCode.ToString().Should().Be("OK");
                user.Name.Should().Be(_name);
                user.Id.ToString().Should().NotBeNullOrWhiteSpace();
            }

        }

        [Test]

        public async Task DeleteUser()
        {
            var requestBody = _handler.CreateUserRequestBody(_name);
            var request = await _handler.POST("users", requestBody);
            var responseBody = request.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserResponse> (responseBody.Result);

            var response = await _handler.DELETE("users", user.Id);

            using (new AssertionScope())
            {
                response.StatusCode.ToString().Should().Be("NoContent");
                user.Name.Should().Be(_name);
                user.Id.ToString().Should().NotBeNullOrWhiteSpace();
            }
        }

    }
}
