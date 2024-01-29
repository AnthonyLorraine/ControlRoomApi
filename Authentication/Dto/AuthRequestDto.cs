namespace ControlRoomApi.Authentication.Dto
{
    public class AuthRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public bool MultiLogin { get; set; }
    }
}
