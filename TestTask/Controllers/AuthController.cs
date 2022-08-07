using Insight.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestTask.Helper;
using TestTask.Models;

namespace TestTask.Controllers
{
    [RoutePrefix("api/v1")]
    // Простенькая авторизация и регистрация
    public class AuthController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MSsqlConnectionString"].ConnectionString;

        [Route("token")]
        [HttpPost]
        public IHttpActionResult Token(AccountModel account)
        {
            try
            {
                using (var repo = new SqlConnection(connectionString).OpenConnection())
                {
                    repo.Query("UserAuthCheck", new { account.Login, Password = Encryption.Encrypt(account.Password) });
                }
            
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string token = Convert.ToBase64String(time.Concat(key).ToArray());

                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Register")]
        [HttpPost]
        public IHttpActionResult Register(AccountModel account)
        {
            try
            {
                using (var repo = new SqlConnection(connectionString).OpenConnection())
                {
                    repo.Query("RegisterUser", new { account.Login, Password = Encryption.Encrypt(account.Password) });
                }

                return Ok("Пользователь зарегистрирован");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

// Простые процедуры для регистрации и авторизации
/*
CREATE PROCEDURE RegisterUser
    @Login NVARCHAR(100),
	@Password NVARCHAR(max)
AS
    IF NOT EXISTS(SELECT TOP 1 ID FROM Users WHERE Login = @Login)
    BEGIN
        INSERT INTO Users(Login, Password)
        VALUES(@Login, @Password)
    END
    ELSE
    BEGIN
        RAISERROR(N'Пользователь уже зарегистрирован!', 16, 1)
    END
GO

ALTER PROCEDURE UserAuthCheck
    @Login NVARCHAR(100),
	@Password NVARCHAR(max)
AS
    IF EXISTS(SELECT TOP 1 ID FROM Users WHERE Login = @Login AND Password = @Password)
    BEGIN
        SELECT CAST(1 as bit)
    END
    ELSE
    BEGIN
        RAISERROR(N'Пользователь не зарегистрирован!', 16, 1)
    END
GO
*/