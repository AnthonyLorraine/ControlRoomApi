namespace ControlRoomApi.Authentication.Dto
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDetailsDto User { get; set; }
        public string TenantUUID { get; set; }
    }
}
