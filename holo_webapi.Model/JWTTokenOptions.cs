namespace holo_webapi.Model
{
    public class JWTTokenOptions
    {
        public string Audience //表示 JWT 的受众（Audience）
        {
            get;
            set;
        }
        public string SecurityKey //表示 JWT 的安全密钥（Security Key）
        {
            get;
            set;
        }
        public string Issuer //表示 JWT 的签发者（Issuer）
        {
            get;
            set;
        }
    }
}