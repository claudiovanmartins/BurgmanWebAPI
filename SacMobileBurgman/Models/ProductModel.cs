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
   
    public class ProductModel
    {
        #region Public Methods

        public List<ProductVO> GetProduct(DateTime updatedAt)
        {
            List<ProductVO> result = new List<ProductVO>();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT pd.Codigo, dbo.Proper(pd.Descricao) as Descricao ");
            sql.Append(",Convert(varchar(10),CONVERT(date,pd.DataCadastro,106),103) as DataCadastro ");
            sql.Append(",pd.EstoqueFisico ");
            sql.Append(",u.Sigla ");
            sql.Append("FROM Produtos pd ");
            sql.Append("INNER JOIN Unidades  u ");
            sql.Append("ON pd.cdUnidade = u.Codigo ");
            sql.Append("WHERE pd.cdTipoProduto = 1 and CONVERT(VARCHAR,pd.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("AND pd.Status = 'A' ");
            sql.Append("ORDER BY DataCadastro ASC ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@UpdatedAt", updatedAt))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        result.Add(ReadLoadProductEntity((IDataRecord)rdr));
                    }
                }
            }

            return result;
        }

        public List<ProductPriceVO> GetProductPrice(int tradeID, DateTime updatedAt)

        {
            List<ProductPriceVO> result = new List<ProductPriceVO>();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT pr.Codigo, pr.CodigoTabelaPreco, pr.CodigoProduto,  ");
            sql.Append("pr.VlrVendaMinimo, pr.VlrVendaBruto, ");
            sql.Append("Convert(varchar(10),CONVERT(date,pr.DataCadastro,106),103) as DataCadastro, ");
            sql.Append("pd.comissao ");
            sql.Append("FROM preco pr ");
            sql.Append("INNER JOIN produtos pd ON pr.CodigoProduto = pd.Codigo ");
            sql.Append("WHERE pr.codigotabelapreco in ");
            sql.Append("(select codigotabelapreco from empresa ");
            sql.Append("where codigocategoria = 5 and codrepresentante = @TradeID ");
            sql.Append("and codigotabelapreco is not null ");
            sql.Append("group by codigotabelapreco) ");
            sql.Append("AND pd.cdTipoProduto = 1 ");
            sql.Append("AND pd.status = 'A' ");
            sql.Append("AND CONVERT(VARCHAR,pr.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("ORDER BY pr.CodigoProduto, pr.DataCadastro ASC ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@TradeID", tradeID, "@UpdatedAt", updatedAt))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        result.Add(ReadLoadProductPriceEntity((IDataRecord)rdr));
                    }
                }
            }

            return result;
        }

        public List<TablePriceVOResponse> GetTablePrice(int tradeID)
        {
            List<TablePriceVOResponse> result = new List<TablePriceVOResponse>();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT tp.Codigo, dbo.Proper(Max(tp.Descricao)) as Descricao ");
            sql.Append("FROM TabelaPreco tp ");
            sql.Append("INNER JOIN Empresa em ON tp.Codigo = em.CodigoTabelaPreco ");
            sql.Append("WHERE em.CodigoCategoria = 5 and em.CodRepresentante = @TradeID ");
            sql.Append("Group by tp.Codigo ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@TradeID", tradeID))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        result.Add(ReadLoadTablePriceEntity((IDataRecord)rdr));
                    }
                }
            }

            return result;
        }


        #endregion


        #region Private Methods

        private ProductVO ReadLoadProductEntity(IDataRecord rdr)
        {
            ProductVO result = new ProductVO();

            result.ProductID = rdr.GetInt32(0);
            result.Product = rdr.IsDBNull(1) ? default(string) : rdr.GetString(1);
            result.UpdatedAt = Convert.ToDateTime(rdr.GetString(2));
            result.ProductInventoryQuantity = rdr.IsDBNull(3) ? default(decimal) : rdr.GetDecimal(3);
            result.MeasureUnit = rdr.IsDBNull(4) ? default(string) : rdr.GetString(4);

            return result;
        }

        private ProductPriceVO ReadLoadProductPriceEntity(IDataRecord rdr)
        {
            ProductPriceVO result = new ProductPriceVO();

            result.ProductPriceID = rdr.GetInt32(0);
            result.TablePriceID = rdr.GetInt32(1);
            result.ProductID = rdr.GetInt32(2);
            result.ProductMinPrice = rdr.IsDBNull(3) ? default(decimal) : rdr.GetDecimal(3);
            result.ProductMaxPrice = rdr.IsDBNull(4) ? default(decimal) : rdr.GetDecimal(4);
            result.UpdatedAt = Convert.ToDateTime(rdr.GetString(5));
            result.Commission = rdr.IsDBNull(6) ? default(decimal) : rdr.GetDecimal(6);

            return result;
        }

        private TablePriceVOResponse ReadLoadTablePriceEntity(IDataRecord rdr)
        {
            TablePriceVOResponse result = new TablePriceVOResponse();

            result.TablePriceID = rdr.GetInt32(0);
            result.TablePriceDescription = rdr.IsDBNull(1) ? default(string) : rdr.GetString(1);

            return result;
        }

        #endregion

    }




}