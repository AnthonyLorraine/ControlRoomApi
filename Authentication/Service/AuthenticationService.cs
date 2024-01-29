using ControlRoomApi.Authentication.Dto;
using ControlRoomApi.Authentication.Service.IService;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ControlRoomApi.Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _controlRoomUrl;
        public AuthenticationService(HttpClient httpClient, string controlRoomUrl)
        {
            _httpClient = httpClient;
            _controlRoomUrl = controlRoomUrl;
        }

        public async Task<AuthResponseDto?> AuthenticateUserAsync(AuthRequestDto authRequest)
        {
            if (string.IsNullOrEmpty(authRequest.Username))
            {
                throw new Exception("Invalid Authorization Request. Username is required.");
            }

            if(string.IsNullOrEmpty(authRequest.ApiKey)) 
            {
                if (string.IsNullOrEmpty(authRequest.Password))
                {
                    throw new Exception("Invalid Authorization Request. Either API Key or Password is required.");
                } 
            }

            if (string.IsNullOrEmpty(authRequest.Password))
            {
                if (string.IsNullOrEmpty(authRequest.ApiKey))
                {
                    throw new Exception("Invalid Authorization Request. Either API Key or Password is required.");
                }
            }

            AuthResponseDto? authResponseDto = null;
            ErrorDto? errorDto = null;

            HttpRequestMessage request = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new($"{_controlRoomUrl}/v1/authentication"),
                Content = new StringContent(JsonSerializer.Serialize(authRequest), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);


            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    authResponseDto = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                    break;
                case HttpStatusCode.BadRequest:
                    errorDto = await response.Content.ReadFromJsonAsync<ErrorDto>();
                    throw new BadRequestException(errorDto);
                case HttpStatusCode.Unauthorized:
                    errorDto = await response.Content.ReadFromJsonAsync<ErrorDto>();
                    throw new UnauthorizedAccessException(errorDto);
                case HttpStatusCode.InternalServerError:
                    errorDto = await response.Content.ReadFromJsonAsync<ErrorDto>();
                    throw new InternalServerErrorException(errorDto);
                default:
                    string? responseData = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Unknown status code {response.StatusCode} returned. {responseData}");
            }

            return authResponseDto;
        }
    }
}
