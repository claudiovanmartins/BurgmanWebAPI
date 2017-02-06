using SacMobileBurgman.Models.Helper;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.Models
{
    public class DefaultingCustomerModel
    {

        #region Public Methods

        public List<DefaultingCustomerVOResponse> GetDefaultingCustomer(int tradeID)
        {
            List<DefaultingCustomerVOResponse> result = new List<DefaultingCustomerVOResponse>();

            string sql = " SELECT	MAX(em.Codigo) as IdCliente,"
                        + " dbo.Proper(MAX(em.NomeFantasia)) as NomeFantasia,"
                        + " COUNT(fi.cdFornecedor) as Qtde_Duplicatas,"
                        + " SUM(fi.Valor) as Valor_Total_Duplicata"
                        + " FROM  FINANCEIRO fi"
                        + " INNER JOIN  EMPRESA em"
                        + " ON fi.cdFornecedor = em.Codigo"
                        + " WHERE fi.Tipo = 2"
                        + " AND em.CodRepresentante  = @CodRepresentante"
                        + " AND fi.Encerrado = 0"
                        + " AND fi.Vencimento < dateadd(day,-3, getdate())"
                        + " GROUP BY fi.cdFornecedor";

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql, "@CodRepresentante", tradeID))
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

        private DefaultingCustomerVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            DefaultingCustomerVOResponse result = new DefaultingCustomerVOResponse();

            result.CustomerID = rdr.GetInt32(0);
            result.Customer = rdr.GetString(1);
            result.DuplicatesQuantity = rdr.GetInt32(2);
            result.DuplicatesValue = rdr.IsDBNull(3) ? default(decimal) : rdr.GetDecimal(3);

            return result;
        }

        #endregion
    }
}