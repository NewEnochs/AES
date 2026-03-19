
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace AES
{
    public class JWTEncryption
    {
        //
        // 摘要:
        //     刷新 Token 身份标识
        private static readonly string[] _refreshTokenClaims = new string[5] { "f", "e", "s", "l", "k" };

        //
        // 摘要:
        //     日期类型的 Claim 类型
        private static readonly string[] DateTypeClaimTypes = new string[3] { "iat", "nbf", "exp" };

        //
        // 摘要:
        //     框架 App 静态类
        internal static Type FrameworkApp { get; set; }

        public static string GenerateJwtToken(Dictionary<string, object> claimsDict, int expireMinutes = 30)
        {
            string secret = "3c1cbc3f546eda35168c3aa3cb91780fbe703f0996c6d123ea96dc85c70bbc0a";
            var claims = claimsDict.Select(kv => new Claim(kv.Key, kv.Value?.ToString() ?? "")).ToList();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
