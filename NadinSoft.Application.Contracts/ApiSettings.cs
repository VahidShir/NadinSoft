namespace NadinSoft.Application.Contracts;

public record ApiSettings
{
    public string SecretKey { get; set; }
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
}