using SacMobileBurgman.Models.Helper;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using SacMobileBurgman.Common;

namespace SacMobileBurgman.Models
{
    public class UserModel
    {
        #region Public Methods

        public UserVOResponse GetLoginAuthenticated(string login, string password)
        {
            UserVOResponse result = new UserVOResponse();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT op.Codigo, dbo.Proper(op.Nome) as Nome, op.Login, ");
            sql.Append("op.Senha, op.cdRepresentante, op.CodigoEmpresa, tp.Simbolo ");
            sql.Append("FROM Operadores op WITH (NOLOCK) ");
            sql.Append("LEFT JOIN TipoOperador tp ");
            sql.Append("ON op.TipoOperadorID = tp.TipoOperadorID ");
            sql.Append("WHERE [Login] COLLATE Latin1_General_CS_AS = @Login COLLATE Latin1_General_CS_AS ");
            sql.Append("AND [Senha] COLLATE Latin1_General_CS_AS = @Password COLLATE Latin1_General_CS_AS ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@Login", login, "@Password", password))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        result = ReadLoadEntity(rdr);
                    }
                }
            }

            return result;
        }

        public void GetAccessToken(ref UserVOResponse user)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT Token FROM TokenAcesso ");
            sql.Append("WHERE OperadorID = @OperadorID ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@OperadorID", user.ID))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                        {
                            CreateNewToken(ref user);
                        }
                    }
                }
                else
                {
                    CreateAccessToken(ref user);
                }
            }
        }

        public static int GetUserIDByAccessToken(string accessToken)
        {
            StringBuilder sql = new StringBuilder();
            int result = default(int);

            sql.Append("SELECT OperadorId FROM TokenAcesso ");
            sql.Append("WHERE Token = @Token ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@Token", accessToken))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (!rdr.IsDBNull(0))
                        {
                            result = rdr.GetInt32(0);
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        #region Private Methods

        private void CreateAccessToken(ref UserVOResponse user)
        {
            try
            {
                string Token = SecurityCommon.CreateToken();

                StringBuilder sql = new StringBuilder();

                sql.Append("insert into TokenAcesso ");
                sql.Append("values ( ");
                sql.Append("@OperadorID, ");
                sql.Append("@Token, ");
                sql.Append("DATEADD(month,6, getdate()), ");
                sql.Append("getdate()) ");

                using (SqlHelper db = new SqlHelper())
                {
                    db.ExecScalar(sql.ToString(), "@OperadorID", user.ID, "@Token", Token);
                }

                user.Token = Token;
            }
            catch (Exception)
            {
                user.Token = default(string);
            }
        }

        private void CreateNewToken(ref UserVOResponse user)
        {
            try
            {
                string Token = SecurityCommon.CreateToken();

                StringBuilder sql = new StringBuilder();

                sql.Append("update TokenAcesso ");
                sql.Append("set Token = @Token, ");
                sql.Append("DataExpiracao = DATEADD(month,6, getdate()) ");
                sql.Append("where Operadorid = @OperadorID");

                using (SqlHelper db = new SqlHelper())
                {
                    db.ExecScalar(sql.ToString(), "@OperadorID", user.ID, "@Token", Token);
                }

                user.Token = Token;
            }
            catch (Exception)
            {
                user.Token = default(string);
            }
        }

        private UserVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            UserVOResponse result = new UserVOResponse
            {
                ID = rdr.GetInt32(0),
                Name = rdr.GetString(1),
                Login = rdr.GetString(2),
                Password = rdr.GetString(3),
                TradeID = rdr.IsDBNull(4) ? default(int) : rdr.GetInt32(4),
                CompanyID = rdr.IsDBNull(5) ? default(int) : rdr.GetInt32(5),
                TypeUserSymbol = rdr.IsDBNull(6) ? default(string) : rdr.GetString(6),
            };
            return result;
        }

        #endregion
    }
}