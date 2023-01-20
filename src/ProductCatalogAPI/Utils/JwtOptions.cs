namespace ProductCatalogAPI.Utils
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        //implement theExpirationDays method
        public int ExpirationDays { get; set; }
    }


}
