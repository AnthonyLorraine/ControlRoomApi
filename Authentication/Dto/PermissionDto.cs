namespace ControlRoomApi.Authentication.Dto
{
    public class PermissionDto
    {
        public int? Id { get; set; }
        public string Action { get; set; }
        public string ResourceId { get; set; }
        public string ResourceType { get; set; }
    }
}
