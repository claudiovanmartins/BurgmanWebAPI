using Microsoft.SqlServer.Types;
using SacMobileBurgman.Common;
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
    public class InventoryModel
    {
        #region Public Methods

        public int PostInventory(InventoryVORequest inventory)
        {
            int inventoryID = default(int);
            if (inventory != null)
            {
                //int userID = UserModel.GetUserIDByAccessToken(SecurityCommon.GetAccessTokenFromSession());

                using (SqlHelper db = new SqlHelper())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO dbo.InventarioRecolha ");
                    sql.Append("([OperadorID],[CodigoDeBarras],[GeoLocalizacao]");
                    sql.Append(",[DataCadastro],[StatusID],[TipoInventarioID],[Sync])");
                    sql.Append("VALUES ");
                    sql.Append("(@OperadorID, @CodigoDeBarras, @GeoLocalizacao, @DataCadastro, @StatusID, @TipoInventarioID, @Sync) ");
                    sql.Append("SELECT CAST(scope_identity() AS int) as Codigo ");

                    SqlParameter geo = new SqlParameter("@GeoLocalizacao", SqlDbType.Udt);
                    geo.UdtTypeName = "Geography";
                    geo.Value = GetGeographyFromText(inventory.Latitude, inventory.Longitude);

                    inventoryID = (int)db.ExecScalar(sql.ToString(),
                                            new SqlParameter("@OperadorID", inventory.UserID),
                                            new SqlParameter("@CodigoDeBarras", inventory.BarCode),
                                            geo,
                                            new SqlParameter("@DataCadastro", DateTime.Now),
                                            new SqlParameter("@StatusID", inventory.StatusID),
                                            new SqlParameter("@TipoInventarioID", inventory.InventoryTypeID),
                                            new SqlParameter("@Sync", 1));

                }
            }
            return inventoryID;
        }

        private SqlGeography GetGeographyFromText(double latitude, double longitude)
        {
            SqlGeography geoLocationIn = null;
            geoLocationIn = SqlGeography.Point(latitude, longitude, 4326);

            return geoLocationIn;
        }

        public List<InventoryTypeVOResponse> GetInventoryType(DateTime updatedAt)
        {
            List<InventoryTypeVOResponse> result = new List<InventoryTypeVOResponse>();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT [TipoInventarioID],[Tipo],[Simbolo], ");
            sql.Append("Convert(varchar(10),CONVERT(date,DataCadastro,106),103) as [DataCadastro] ");
            sql.Append("FROM [dbo].[TipoInventario] ");
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

        private InventoryTypeVOResponse ReadLoadEntity(IDataRecord rdr)
        {
            InventoryTypeVOResponse result = new InventoryTypeVOResponse
            {
                InventoryTypeID = rdr.GetInt32(0),
                Type = rdr.GetString(1),
                Symbol = rdr.IsDBNull(2) ? default(char) : Convert.ToChar(rdr.GetValue(2)),
                UpdatedAt = rdr.IsDBNull(3) ? default(DateTime) : Convert.ToDateTime(rdr.GetString(3))
            };

            return result;
        }

        #endregion
    }
}