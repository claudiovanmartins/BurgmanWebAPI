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
    public class TransactionTypeModel
    {
        #region Public Methods

        public List<TransactionTypeVOResponse> GetTransactionType(DateTime updatedAt)
        {
            List<TransactionTypeVOResponse> result = new List<TransactionTypeVOResponse>();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT CodMoviment, dbo.Proper(CFO_Desc) as CFO_Desc, SincronizaMobile, Convert(varchar(10),CONVERT(date,DataCadastro,106),103) as [DataCadastro]  ");
            sql.Append("FROM Tipo_Movimentacao ");
            sql.Append("WHERE CONVERT(VARCHAR,DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");

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

        private TransactionTypeVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            TransactionTypeVOResponse result = new TransactionTypeVOResponse();

            result.TransactionsTypeID = rdr.GetInt32(0);
            result.TransactionsTypeDesc = rdr.GetString(1);
            result.SyncMobile = Convert.ToChar(rdr.GetValue(2));
            result.UpdatedAt = Convert.ToDateTime(rdr.GetString(3));

            return result;
        }

        #endregion
    }
}