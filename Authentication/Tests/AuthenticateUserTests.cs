using ControlRoomApi.Authentication.Dto;
using ControlRoomApi.Authentication.Service;
using Xunit;

namespace ControlRoomApi.Authentication.Tests
{
    public class AuthenticateUserTests
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationService _authenticationService;
        // Mock API Endpoint, but be able to switch to test the responses are correct from the server?
        string validUserName = "";
        string validApiKey = "";

        public AuthenticateUserTests()
        {
            _httpClient = new HttpClient();
            _authenticationService = new(_httpClient, "ControlRoomUrl");

        }

        [Fact]
        public async void TestAuthenticateUser_MissingUsernameRaisesException() 
        {
            AuthRequestDto validAuthRequestDto = new AuthRequestDto()
            {
                Username = "",
                ApiKey = validApiKey,
                MultiLogin = true,
            };

            Exception exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                AuthResponseDto? response = await _authenticationService.AuthenticateUserAsync(validAuthRequestDto);
            });

            Assert.NotNull(exception);
            Assert.Equal("Invalid Authorization Request. Username is required.", exception.Message);
        }
        [Fact]
        public async void TestAuthenticateUser_MissingAPIKeyRaisesException() 
        {
             AuthRequestDto validAuthRequestDto = new AuthRequestDto()
            {
                Username = validUserName,
                ApiKey = "",
                MultiLogin = true,
            };


            Exception exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                AuthResponseDto? response = await _authenticationService.AuthenticateUserAsync(validAuthRequestDto);
            });

            Assert.NotNull(exception);
            Assert.Equal("Invalid Authorization Request. Either API Key or Password is required.", exception.Message);
        }
        [Fact]
        public async void TestAuthenticateUser_MissingPasswordRaisesException() 
        {
             AuthRequestDto validAuthRequestDto = new AuthRequestDto()
            {
                Username = validUserName,
                Password = "",
                MultiLogin = true,
            };

            Exception exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                AuthResponseDto? response = await _authenticationService.AuthenticateUserAsync(validAuthRequestDto);
            });

            Assert.NotNull(exception);
            Assert.Equal("Invalid Authorization Request. Either API Key or Password is required.", exception.Message);
        }
        [Fact]
        public async void TestAuthenticateUser_MissingPasswordValidAPIKeySuccess() 
        {
             AuthRequestDto validAuthRequestDto = new AuthRequestDto()
            {
                Username = validUserName,
                ApiKey = validApiKey,
                MultiLogin = true,
            };
            AuthResponseDto? response = await _authenticationService.AuthenticateUserAsync(validAuthRequestDto);

            Assert.NotNull(response);
            Assert.NotNull(response.User);
            Assert.NotNull(response.Token);
            Assert.NotEqual("", response.Token);
        }
        [Fact]
        public async void TestAuthenticateUser_MissingAPIKeyValidPasswordSuccess() {
            
             AuthRequestDto validAuthRequestDto = new AuthRequestDto()
            {
                Username = validUserName,
                ApiKey = "",
                MultiLogin = true,
                Password = "NotABlankPassword"
            };


            UnauthorizedAccessException exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                AuthResponseDto? response = await _authenticationService.AuthenticateUserAsync(validAuthRequestDto);
            });

            Assert.NotNull(exception);
            Assert.Equal("UM1110", exception.ErrorDto.Code);
        }
        [Fact]
        public async void TestAuthenticateUser_Success()
        {
            AuthRequestDto validAuthRequestDto = new AuthRequestDto()
            {
                Username = validUserName,
                ApiKey = validApiKey,
                MultiLogin = true,
            };
            AuthResponseDto? response = await _authenticationService.AuthenticateUserAsync(validAuthRequestDto);

            Assert.NotNull(response);
            Assert.NotEqual("", response.Token);

        }
        //[Fact] 
        //public async void TestAuthenticateUser_FailureStatusCode400() 
        //{
        //    // How to mock this as the Dto prevents bad data and the method input value checks only valid user/pass can be sent.
        //    Assert.True(false);
        //}            
        [Fact]
        public async void TestAuthenticateUser_FailureStatusCode401() 
        {
            AuthRequestDto invalidAuthRequestDto = new AuthRequestDto()
            {
                MultiLogin = true,
                Username = validUserName,
                Password = "WrongPassword"
            };

            UnauthorizedAccessException exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                AuthResponseDto? response = await _authenticationService.AuthenticateUserAsync(invalidAuthRequestDto);
            });

            Assert.NotNull(exception);
            Assert.Equal("UM1110", exception.ErrorDto.Code);
        }
        //[Fact]
        //public void TestAuthenticateUser_FailureStatusCode500()
        //{
        //    // TBC, to use Moq?
        //    Assert.True(false);
        //}

        private void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
