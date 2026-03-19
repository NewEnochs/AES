using AES.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Helper
{
    public static class TokenHelper
    {
        public static string SetToken(SYS_USER user)
        {
            var db = new DbContext().Db;
            // 构造需要写入 JWT 的声明（Claims）
            var claimsDict = new Dictionary<string, object>
            {
                { ClaimConst.CLAINM_USERID, user.Id.ToString() },
                { ClaimConst.CLAINM_ACCOUNT, user.Account },
                { ClaimConst.CLAINM_NAME, user.Name },
                { ClaimConst.CLAINM_SUPERADMIN, user.AdminType.ToString() }
            };

            var config = db.Queryable<SYS_CONFIG>().Where(x => x.Status == 0 && x.Code == "TokenTime").First();
            var timeNum = 1440;
            if (config != null)
            {
                if (int.TryParse(config.Value, out int parsed))
                {
                    timeNum = parsed;
                }
            }
            // 访问令牌，有效期短，1天
            var accessToken = JWTEncryption.GenerateJwtToken(claimsDict, timeNum);

            // 刷新令牌，有效期长，7天
            var refreshToken = JWTEncryption.GenerateJwtToken(
                new Dictionary<string, object> { { "access_token", accessToken } },
                10080);

            return accessToken;
        }
    }
}
