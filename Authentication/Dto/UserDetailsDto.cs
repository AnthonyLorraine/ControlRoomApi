namespace ControlRoomApi.Authentication.Dto
{
    public enum LicenseFeature
    {
        DEVELOPMENT,
        RUNTIME,
        METABOTRUNTIME,
        IQBOTRUNTIME
    }
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public List<RoleDto> Roles { get; set; } = [];
        public List<PermissionDto> Permissions { get; set; } = [];
        public List<LicenseFeature> LicenseFeatures { get; set; } = [];
        public int PrincipleId { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public bool PasswordExpired { get; set; }
        public bool PasswordSet { get; set; }
        public bool EnableAutoLogin { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Locked { get; set; }
    }
}
