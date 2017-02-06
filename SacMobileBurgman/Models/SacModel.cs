using SacMobileBurgman.Models.Helper;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SacMobileBurgman.Models
{
    public class SacModel
    {
        #region Public Methods

        public void PostSac(List<SacVORequest> sacList)
        {
            if (sacList != null)
            {
                using (SqlHelper db = new SqlHelper())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO SAC ");
                    sql.Append("(IdUsuario,IdCliente,IdTipoSAC,Observacao,DataReclamacao,FlagMobile) ");
                    sql.Append("VALUES ");
                    sql.Append("(@IdUsuario,@IdCliente,@IdTipoSAC,@Observacao,@DataReclamacao,@FlagMobile) ");
                    
                    sacList.ForEach(x =>
                    {
                        db.ExecScalar(sql.ToString(), "@IdUsuario", x.UserID, "@IdCliente", x.CustomerID,
                                               "@IdTipoSAC", x.SacTypeID, "@DataReclamacao", x.CreateAt,
                                               "@Observacao", x.Description, "@FlagMobile", 1);
                    });

                }
            }
        }

        public List<SacTypeVOResponse> GetSacType(DateTime updatedAt)
        {
            List<SacTypeVOResponse> result = new List<SacTypeVOResponse>();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT [IdTipoSAC],[Descricao],[Status] ");
            sql.Append(",Convert(varchar(10),CONVERT(date,DataCadastro,106),103) as [DataCadastro] ");
            sql.Append("FROM [dbo].[TipoSAC] ");
            sql.Append("WHERE CONVERT(VARCHAR,DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("ORDER BY DataCadastro ASC ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@UpdatedAt", updatedAt))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        result.Add(ReadLoadEntity((IDataRecord)rdr));
                    }
                }
            }

            return result;
        }

        #endregion

        #region Private Methods

        private SacTypeVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            SacTypeVOResponse result = new SacTypeVOResponse();

            result.SacTypeID = rdr.GetInt32(0);
            result.Description = rdr.GetString(1);
            result.Status = Convert.ToChar(rdr.GetValue(2));
            result.UpdatedAt = Convert.ToDateTime(rdr.GetString(3));

            return result;
        }

        #endregion
    }
}