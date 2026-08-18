namespace Hearth.Integrations.APIs.Plaid;

public class PlaidOptions
{
    public string ClientId { get; set; } = default!;
    public string Secret { get; set; } = default!;
    public string BaseUrl { get; set; } = "https://production.plaid.com";
}