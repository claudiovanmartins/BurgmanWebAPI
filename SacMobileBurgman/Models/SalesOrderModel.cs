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
    public class SalesOrderModel
    {
        #region Public Methods

        public List<SalesOrderVOResponse> GetSalesOrder(int tradeID, DateTime updatedAt)
        {
            List<SalesOrderVOResponse> result = new List<SalesOrderVOResponse>();

            StringBuilder sql = new StringBuilder();

            sql.Append("  SELECT ");
            sql.Append("			p.Codigo as cdCodigo, ");
            sql.Append("			p.cdCliente as CodCliente,  ");
            sql.Append("			p.nrPedidoCliente as nrPedido,  ");
            sql.Append("			p.cdTipoMovimentacao as cdTipoMovimentacao, ");
            sql.Append("			Convert(varchar(10),CONVERT(date,p.DataLancamento,106),103) as dtLancamento,  ");
            sql.Append("			p.TipoFrete,  ");
            sql.Append("			Convert(varchar(10),CONVERT(date,p.DataEntrega,106),103) as dtEntrega,  ");
            sql.Append("			p.cdRepresentante as CodRepresentante,  ");
            sql.Append("			isnull(p.Comercial,'') as Observacao, ");
            sql.Append("			p.FlagMobile, ");
            sql.Append("			Convert(varchar(10),CONVERT(date,p.DataCadastro,106),103) as dtCadastro,");
            sql.Append("			(select count(*) as QuantidadeItens");
            sql.Append("			   from pedidovendalinha pvl");
            sql.Append("			  where pvl.cdPedidoVenda = p.Codigo) as QuantidadeItens,");
            sql.Append("			(select sum(ValorTotalIten) as ValorTotal  				");
            sql.Append("			   from pedidovendalinha");
            sql.Append("			  where cdPedidoVenda =  p.Codigo) as ValorTotalPedido");
            sql.Append("	FROM( ");
            sql.Append("			select  pv.Codigo , pv.nrPedidoCliente, pv.cdTipoMovimentacao, pv.DataLancamento, pv.TipoFrete,  ");
            sql.Append("					pv.DataEntrega, pv.cdCliente, pv.cdRepresentante, pv.Comercial, pv.FlagMobile, pv.DataCadastro, ");
            sql.Append("					row_number() over (partition by pv.cdCliente order by pv.codigo desc) as seqnum ");
            sql.Append("			  from pedidovenda pv ");
            sql.Append("		inner join empresa em ");
            sql.Append("				on pv.cdCliente = em.Codigo ");
            sql.Append("			 where pv.cdRepresentante = @TradeId ");
            sql.Append("			   and em.CodRepresentante = @TradeId ");
            sql.Append("			   and em.CodigoCategoria = 5 ");
            sql.Append("			   and CONVERT(VARCHAR,pv.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("		) as p ");
            sql.Append("   WHERE seqnum <= 5 ");
            sql.Append("ORDER BY p.DataCadastro ASC ");

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@TradeId", tradeID, "@UpdatedAt", updatedAt))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        //Só lista pedidos com ValorTotalPedido maior que 0
                        if (!rdr.IsDBNull(12) && rdr.GetDecimal(12) > 0)
                        {
                            result.Add(ReadLoadEntity((IDataRecord)rdr));
                        }
                    }
                }
            }

            return result;
        }

        public int PostSalesOrder(SalesOrderVORequest salesOrder)
        {
            int salesOrderID = default(int);
            if (salesOrder != null)
            {
                using (SqlHelper db = new SqlHelper())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO PedidoVenda ");
                    sql.Append("(nrPedidoCliente, ");
                    sql.Append("cdTipoMovimentacao, ");
                    sql.Append("DataLancamento, ");
                    sql.Append("TipoFrete, ");
                    sql.Append("DataEntrega, ");
                    sql.Append("cdCliente, ");
                    sql.Append("cdRepresentante, ");
                    sql.Append("Status, ");
                    sql.Append("cdCondicaopagamento, ");
                    sql.Append("cdTransportadora, ");
                    sql.Append("cdTabelaPreco, ");
                    sql.Append("cdAtendente, ");
                    sql.Append("FlagMobile, ");
                    sql.Append("DataCadastro, ");
                    sql.Append("CdEmpresa, ");
                    sql.Append("Comercial ");
                    sql.Append(")   ");
                    sql.Append("VALUES ( ");
                    sql.Append("@NumPedido, ");
                    sql.Append("@cdTipoMovimentacao,  ");
                    sql.Append("@dtLancamento,  ");
                    sql.Append("Upper(@TiploFrete), ");
                    sql.Append("@dtEntrega, ");
                    sql.Append("@CodEmpresa,  ");
                    sql.Append("@CodRepresentante, ");
                    sql.Append("'A', ");
                    sql.Append("4, ");
                    sql.Append("2404, ");
                    sql.Append("@CodTabelaPreco, ");
                    sql.Append("0, ");
                    sql.Append("1, ");
                    sql.Append("getdate(), ");
                    sql.Append("@CdEmpresa, ");
                    sql.Append("@Observacao) ");
                    sql.Append("SELECT CAST(scope_identity() AS int) as Codigo ");

                    salesOrderID = (int)db.ExecScalar(sql.ToString(),
                    new SqlParameter("@NumPedido", salesOrder.OrderNumber),
                    new SqlParameter("@cdTipoMovimentacao", salesOrder.TransactionsTypeID),
                    new SqlParameter("@dtLancamento", salesOrder.ReleaseAt),
                    new SqlParameter("@TiploFrete", salesOrder.Freight),
                    new SqlParameter("@dtEntrega", salesOrder.DeliveryAt),
                    new SqlParameter("@CodEmpresa", salesOrder.CustomerID),
                    new SqlParameter("@CodRepresentante", salesOrder.TradeID),
                    new SqlParameter("@CodTabelaPreco", salesOrder.TablePriceID),
                    new SqlParameter("@CdEmpresa", salesOrder.CompanyID),
                    new SqlParameter("@Observacao", salesOrder.Observation));
                }
            }

            return salesOrderID;
        }
        
        #endregion

        #region Private Methods

        private SalesOrderVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            SalesOrderVOResponse result = new SalesOrderVOResponse();

            result.SalesOrderID = rdr.GetInt32(0);
            result.CustomerID = rdr.GetInt32(1);
            result.OrderNumber = rdr.IsDBNull(2) ? default(string) : rdr.GetString(2);
            result.TransactionsTypeID = rdr.GetInt32(3);
            result.ReleaseAt = Convert.ToDateTime(rdr.GetString(4));
            result.Freight = rdr.IsDBNull(5) ? default(string) : rdr.GetString(5);
            result.DeliveryAt = Convert.ToDateTime(rdr.GetString(6));
            result.TradeID = rdr.GetInt32(7);
            result.Observation = rdr.IsDBNull(8) ? default(string) : rdr.GetString(8);
            result.UpdatedAt = Convert.ToDateTime(rdr.GetString(10));
            result.TotalItemsOrder = rdr.GetInt32(11);
            result.AmountOrder = rdr.GetDecimal(12);

            return result;
        }

        #endregion
    }
}