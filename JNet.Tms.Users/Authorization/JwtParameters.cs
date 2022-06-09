using System;

namespace JNet.Tms
{
    public class JwtParameters
    {
        public const string Issuer = "JNET";

        public const string Audience = "TMS";

        public static string SigningKey => App.Configuration["JwtSigningKey"];

        public static TimeSpan Expires => TimeSpan.FromHours(int.Parse(App.Configuration["JwtExpireHours"]));
    }
}
