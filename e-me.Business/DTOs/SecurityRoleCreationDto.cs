namespace e_me.Business.DTOs
{
    public class SecurityRoleCreationDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
