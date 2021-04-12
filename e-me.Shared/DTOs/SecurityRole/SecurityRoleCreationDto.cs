namespace e_me.Shared.DTOs.SecurityRole
{
    public class SecurityRoleCreationDto
    {
        public string Name { get; set; }

        public int SecurityType { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
