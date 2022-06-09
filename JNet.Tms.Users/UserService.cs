using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JNet.Tms.Users
{
    public class UserService : EntityServiceBase<User>
    {
        [HttpPost]
        [AllowAnonymous]
        public string Login(string username, string password, int entId)
        {
            if (!TryValidateForProperty<User>(p => p.Username, username, out string message) ||
                !TryValidateForProperty<User>(p => p.Password, password, out message))
            {
                throw new AppException(message);
            }

            var uid = EntitySet.Where(p => p.Username == username && p.Password == Md5(password)).Select(p => p.ID).FirstOrDefault();
            if (uid == 0)
                throw new AppException("用户名不存在或密码错误");

            entId = DbContext.Set<Enterprise>()
                            .Where(p => p.ID == entId, entId > 0)
                            .Where(p => p.UID == uid)
                            .Take(1)
                            .Select(p => p.ID)
                            .FirstOrDefault();

            return new AuthorizedUser(uid, entId).GetJwtToken();
        }

        [HttpPost]
        [AllowAnonymous]
        public string Reg(User user)
        {
            if (EntitySet.Any(p => p.Username == user.Username))
                throw new AppException("用户名已存在");

            var now = DateTime.Now;
            user.RegTime = now;
            user.RegIP = HttpContext.Connection.RemoteIpAddress.ToString();
            user.Password = Md5(user.Password);

            EntitySet.Add(user);
            DbContext.SaveChanges();

            return new AuthorizedUser(user.ID, 0).GetJwtToken();
        }

        public object GetInfo([FromUID] int id, [FromEntId] int entId)
        {
            var query = from u in EntitySet.Where(p => p.ID == id)
                        join e in DbContext.Set<Enterprise>()
                        on new { EntId = entId, UID = u.ID } equals new { EntId = e.ID, e.UID } into ej
                        from er in ej.DefaultIfEmpty()
                        select new
                        {
                            UserName = u.Username,
                            EntName = er.Name
                        };

            return query.FirstOrDefault();
        }

        private static string Md5(string value)
        {
            using var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
