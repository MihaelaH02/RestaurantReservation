public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpireMinutes { get; set; }

    /* public JwtSettings(IConfiguration configuration)
     {
         Key = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
               ?? configuration["JwtSettings:Key"]
               ?? throw new Exception("JWT_SECRET_KEY is missing.");

         Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
                  ?? configuration["JwtSettings:Issuer"]
                  ?? "DefaultIssuer";

         Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
                    ?? configuration["JwtSettings:Audience"]
                    ?? "DefaultAudience";

         ExpireMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRE_MINUTES"), out var expire)
                         ? expire
                         : int.Parse(configuration["JwtSettings:ExpireMinutes"] ?? "60");
     }*/
}
