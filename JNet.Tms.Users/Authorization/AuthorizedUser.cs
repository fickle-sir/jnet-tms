using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JNet.Tms
{
    public class AuthorizedUser
    {
        public int UID { get; set; }

        public int EntId { get; set; }

        public AuthorizedUser() { }

        public AuthorizedUser(int uid, int entId)
        {
            UID = uid;
            EntId = entId;
        }

        public IEnumerable<Claim> ToClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UID), UID.ToString()),
                new Claim(nameof(EntId), EntId.ToString())
            };
            return claims;
        }

        public string GetJwtToken()
        {
            var user = this;
            var claims = user.ToClaims();
            var now = DateTime.Now;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtParameters.SigningKey));
            var jwt = new JwtSecurityToken(
                issuer: JwtParameters.Issuer,
                audience: JwtParameters.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(JwtParameters.Expires),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
