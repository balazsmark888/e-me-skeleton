namespace e_me.Business.DTOs
{
    public class SecurityRoleCreationDto
    {
        public string Name { get; set; }

        public int SecurityType { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
