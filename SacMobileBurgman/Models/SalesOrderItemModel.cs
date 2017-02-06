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
    public class SalesOrderItemModel
    {
        #region Public Methods

        public List<SalesOrderItemVOResponse> GetSalesOrderItem(int tradeID, DateTime updatedAt)
        {
            List<SalesOrderItemVOResponse> result = new List<SalesOrderItemVOResponse>();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT	pl.Codigo, ");
            sql.Append("        pl.cdPedidoVenda as CodigoPedidoVenda, ");
            sql.Append("		pl.cdProduto, ");
            sql.Append("		pl.Comissao,  ");
            sql.Append("		pl.ProdutoQuantidade,  ");
            sql.Append("		pl.ProdutoValorUnitario, ");
            sql.Append("		pl.ValorTotalIten as ValorTotalItem,  ");
            sql.Append("		pl.FlagMobile ");
            sql.Append("  FROM  pedidovendalinha pl  ");
            sql.Append(" inner join  ");
            sql.Append("		(select p.Codigo as cdCodigo,  ");
            sql.Append("				p.cdCliente as cdCliente, ");
            sql.Append("				p.ValorTotal as ValorTotal ");
            sql.Append("		   from(select  pv.Codigo, pv.cdCliente, sum(pvl.ValorTotalIten) as ValorTotal,  ");
            sql.Append("				row_number() over (partition by pv.cdCliente order by pv.codigo desc) as seqnum ");
            sql.Append("				from  pedidovenda pv ");
            sql.Append("				inner join empresa em on pv.cdCliente = em.Codigo ");
            sql.Append("				inner join pedidovendalinha pvl on pvl.cdPedidoVenda = pv.Codigo ");
            sql.Append("			    where pv.cdRepresentante = @TradeId ");
            sql.Append("			    and em.CodRepresentante = @TradeId ");
            sql.Append("				and em.CodigoCategoria = 5 ");
            sql.Append("				and CONVERT(VARCHAR,pv.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("				group by pv.Codigo, pv.cdCliente) p ");
            sql.Append("		  where seqnum <= 5 ");
            sql.Append("		  and ValorTotal > 0) as pa ");
            sql.Append("		  on pl.cdPedidoVenda = pa.cdCodigo ");
            sql.Append("	   order by pa.cdCodigo desc ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@TradeId", tradeID, "@UpdatedAt", updatedAt))
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

        public int PostSalesOrderItem(SalesOrderItemVORequest salesOrderItem)
        {
            int salesOrderItemID = default(int);
            if (salesOrderItem != null)
            {
                using (SqlHelper db = new SqlHelper())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO [PedidoVendaLinha] (  ");
                    sql.Append("[cdPedidoVenda], ");
                    sql.Append("[cdProduto], ");
                    sql.Append("[Comissao], ");
                    sql.Append("[ProdutoQuantidade], ");
                    sql.Append("[ProdutoValorUnitario], ");
                    sql.Append("[ValorTotalIten], ");
                    sql.Append("[FlagMobile], ");
                    sql.Append("[Linha])   ");
                    sql.Append("VALUES ( ");
                    sql.Append("@CodigoPedidoVendaMobile,  ");
                    sql.Append("@cdProduto,  ");
                    sql.Append("@Comissao, ");
                    sql.Append("@ProdutoQuantidade,  ");
                    sql.Append("@ProdutoValorUnitario, ");
                    sql.Append("@ValorTotalItem, ");
                    sql.Append("1, ");
                    sql.Append("0)  ");
                    sql.Append("SELECT CAST(scope_identity() AS int) as Codigo ");

                    //salesOrderItem.ForEach(x =>
                    //{

                   salesOrderItemID = (int)db.ExecScalar(sql.ToString(),
                    new SqlParameter("@CodigoPedidoVendaMobile", salesOrderItem.SalesOrderID),
                    new SqlParameter("@cdProduto", salesOrderItem.ProductID),
                    new SqlParameter("@Comissao", salesOrderItem.Commission),
                    new SqlParameter("@ProdutoQuantidade", salesOrderItem.ProductQuantity),
                    new SqlParameter("@ProdutoValorUnitario", salesOrderItem.ProductUnitaryValue),
                    new SqlParameter("@ValorTotalItem", salesOrderItem.AmountOrderItem));

                    //});
                }
            }

            return salesOrderItemID;
        }

        #endregion

        #region Private Methods

        private SalesOrderItemVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            SalesOrderItemVOResponse result = new SalesOrderItemVOResponse();

            result.SalesOrderItemID = rdr.GetInt32(0);
            result.SalesOrderID = rdr.GetInt32(1);
            result.ProductID = rdr.GetInt32(2);
            result.Commission = rdr.IsDBNull(3) ? default(decimal) : rdr.GetDecimal(3);
            result.ProductQuantity = rdr.GetDecimal(4);
            result.ProductUnitaryValue = rdr.IsDBNull(5) ? default(decimal) : rdr.GetDecimal(5);
            result.AmountOrderItem = rdr.IsDBNull(6) ? default(decimal) : rdr.GetDecimal(6);
            result.FlagMobile = rdr.IsDBNull(7) ? default(char) : Convert.ToChar(rdr.GetValue(7));

            return result;
        }

        #endregion
    }
}