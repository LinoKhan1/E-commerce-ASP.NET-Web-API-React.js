namespace e_commerce.Server.Config
{
    public class PayPalSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Mode { get; set; } // live or Sandbox
    }
}
