using ControlRoomApi.Authentication.Dto;

namespace ControlRoomApi.Authentication.Service.IService
{
    internal interface IAuthenticationService
    {
        public Task<AuthResponseDto?> AuthenticateUserAsync(AuthRequestDto authRequest);

    }
}
