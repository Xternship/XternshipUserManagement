namespace ApiGateway.Settings
{
    public class EmailSettings
    {
        public string? DefaultFromEmail { get; set; }
        public string? SMTPSettingHost { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
