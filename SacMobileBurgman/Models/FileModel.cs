using SacMobileBurgman.Models.Helper;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SacMobileBurgman.Models
{
    public class FileModel
    {
        public Dictionary<string, string> PostFileData(FileVOResquest fileData)
        {
            Dictionary<string, string> imageCollection = null;

            if (fileData != null)
            {
                using (SqlHelper db = new SqlHelper())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO ClienteImagens ");
                    sql.Append("(IdUsuario, IdCliente, DataCadastro, NomeImagem, Imagem, Comentario) ");
                    sql.Append("VALUES ");
                    sql.Append("(@IdUsuario,@IdCliente,GETDATE(),@NomeImagem, ");
                    sql.Append("CONVERT(VARBINARY(max), @Imagem), @Comentario) ");
                    sql.Append("SELECT CAST(scope_identity() AS int) as Codigo ");

                    int fileID = (int)db.ExecScalar(sql.ToString(), 
                        new SqlParameter("@IdUsuario", fileData.UserID),
                        new SqlParameter("@IdCliente", fileData.CustomerID),
                        new SqlParameter("@NomeImagem", fileData.NameImage),
                        new SqlParameter("@Imagem", fileData.AttachmentFile),
                        new SqlParameter("@Comentario", fileData.Comment));

                    
                    imageCollection = new Dictionary<string, string>()
                        {
                            {"imageID", fileID.ToString()}
                        };
                }
            }

            return imageCollection;
        }
    }
}