using SacMobileBurgman.Models.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SacMobileBurgman.Common
{
    public static class SecurityCommon
    {
        const int CONST_LENGHT_TOKEN = 30;

        public static string CreateToken()
        {
            char[] AvailableCharacters = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'
          };

            char[] identifier = new char[CONST_LENGHT_TOKEN];
            byte[] randomData = new byte[CONST_LENGHT_TOKEN];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomData);
            }

            for (int idx = 0; idx < identifier.Length; idx++)
            {
                int pos = randomData[idx] % AvailableCharacters.Length;
                identifier[idx] = AvailableCharacters[pos];
            }

            return new string(identifier);
        }

        public static bool ValidateToken(string token)
        {
            bool result = default(bool);
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT Token FROM TokenAcesso ");
            sql.Append("WHERE Token = @Token ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@Token", token))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                        {
                            SetAccessTokenToSession(rdr.GetString(0));
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public static string GetAccessTokenFromSession()
        {
            string result = default(string);
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                if (session["accessToken"] != null)
                {
                    result = session["accessToken"].ToString();
                }
            }
            return result;
        }

        public static void SetAccessTokenToSession(string accessToken)
        {
            var session = HttpContext.Current.Session;
            if (session != null)
            {
                session["accessToken"] = accessToken;
            }
        }
    }
}