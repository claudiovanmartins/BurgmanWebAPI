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
    public class SyncModel
    {
        #region Public Methods

        public List<SyncVOResponse> GetSyncItems(int UserID, DateTime updatedAt)
        {
            List<SyncVOResponse> result = new List<SyncVOResponse>();

            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT 1 as ModuleID, 'Customer' as Module, COUNT(*) as CountRows ");
            sql.Append("	  FROM empresa em ");
            sql.Append("INNER JOIN operadores op ");
            sql.Append("		ON em.codrepresentante = op.cdrepresentante ");
            sql.Append("	 WHERE op.codigo = @UserID ");
            sql.Append("	   AND em.CodigoCategoria = 5 ");
            sql.Append("	   AND CONVERT(VARCHAR,em.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("	UNION ");
            sql.Append("   SELECT 2 as ModuleID, 'Product' as Module, COUNT(*) as CountRows ");
            sql.Append("	 FROM produtos po ");
            sql.Append("  INNER JOIN preco pr ");
            sql.Append("	   on po.Codigo = pr.CodigoProduto ");
            sql.Append("  INNER JOIN tabelapreco tp ");
            sql.Append("	   ON pr.codigotabelapreco = tp.codigo ");
            sql.Append("	WHERE tp.codigo in ( ");
            sql.Append("						SELECT em.CodigoTabelaPreco  ");
            sql.Append("						  FROM empresa em ");
            sql.Append("					INNER JOIN operadores op  ");
            sql.Append("							ON em.codrepresentante = op.cdrepresentante ");
            sql.Append("						 WHERE em.CodigoTabelaPreco is not null ");
            sql.Append("						   AND em.CodigoCategoria = 5 ");
            sql.Append("						   AND op.codigo = @UserID ");
            sql.Append("					  GROUP BY em.CodigoTabelaPreco ");
            sql.Append("						) ");
            sql.Append("	  AND CONVERT(VARCHAR,po.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("	UNION ");
            sql.Append("   SELECT 3 as ModuleID, 'Price' as Module, COUNT(*) as CountRows ");
            sql.Append("	 FROM preco pr ");
            sql.Append("  INNER JOIN tabelapreco tp ");
            sql.Append("	   ON pr.codigotabelapreco = tp.codigo ");
            sql.Append("	WHERE tp.codigo in ( ");
            sql.Append("						SELECT em.CodigoTabelaPreco  ");
            sql.Append("						  FROM empresa em ");
            sql.Append("					INNER JOIN operadores op  ");
            sql.Append("							ON em.codrepresentante = op.cdrepresentante ");
            sql.Append("						 WHERE em.CodigoTabelaPreco is not null ");
            sql.Append("						   AND em.CodigoCategoria = 5 ");
            sql.Append("						   AND op.codigo = @UserID ");
            sql.Append("					  GROUP BY em.CodigoTabelaPreco ");
            sql.Append("						) ");
            sql.Append("	  AND CONVERT(VARCHAR,pr.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("   UNION ");
            sql.Append("  SELECT 4 as ModuleID, 'SalesOrder' as Module, COUNT(*) as CountRows  ");
            sql.Append("	FROM pedidovenda pd ");
            sql.Append("   WHERE pd.cdrepresentante in  ");
            sql.Append("			( ");
            sql.Append("				select em.CodRepresentante  ");
            sql.Append("				  from empresa em ");
            sql.Append("			inner join operadores op  ");
            sql.Append("					on em.codrepresentante = op.cdrepresentante ");
            sql.Append("				 where em.CodigoTabelaPreco is not null ");
            sql.Append("		 		   and em.CodigoCategoria = 5 ");
            sql.Append("				   and op.codigo = @UserID ");
            sql.Append("			  Group by em.CodRepresentante ");
            sql.Append("			) ");
            sql.Append("	 AND CONVERT(VARCHAR,pd.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("   UNION ");
            sql.Append("   SELECT 5 as ModuleID, 'SalesOrderItem' as Module, COUNT(*) as CountRows  ");
            sql.Append("	 FROM pedidovendalinha pl ");
            sql.Append("  INNER JOIN pedidovenda pd ");
            sql.Append("	   ON pl.cdPedidoVenda = pd.Codigo ");
            sql.Append("    WHERE pd.cdrepresentante in  ");
            sql.Append("			( ");
            sql.Append("				select em.CodRepresentante  ");
            sql.Append("				  from empresa em ");
            sql.Append("			inner join operadores op  ");
            sql.Append("					on em.codrepresentante = op.cdrepresentante ");
            sql.Append("				 where em.CodigoTabelaPreco is not null ");
            sql.Append("		 		   and em.CodigoCategoria = 5 ");
            sql.Append("				   and op.codigo = @UserID ");
            sql.Append("			  Group by em.CodRepresentante ");
            sql.Append("			) ");
            sql.Append("	 AND CONVERT(VARCHAR,pd.DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("	UNION ");
            sql.Append("	SELECT 6 as ModuleID, 'SacType' as Module, COUNT(*) as CountRows  ");
            sql.Append("	  FROM TipoSAC ");
            sql.Append("	 WHERE CONVERT(VARCHAR,DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            sql.Append("	UNION ");
            sql.Append("   SELECT 7 as ModuleID, 'TransactionsType' as Module, COUNT(*) as CountRows  ");
            sql.Append("	 FROM Tipo_Movimentacao ");
            sql.Append("	WHERE CONVERT(VARCHAR,DataCadastro,120) > CONVERT(VARCHAR,@UpdatedAt,120) ");
            
            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql.ToString(), "@UserID", UserID, "@UpdatedAt", updatedAt))
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

        private SyncVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            SyncVOResponse result = new SyncVOResponse();
            result.ModuleID = rdr.GetInt32(0);
            result.Module = rdr.GetString(1);
            result.CountRows = rdr.GetInt32(2);

            return result;
        }

        #endregion
    }
}