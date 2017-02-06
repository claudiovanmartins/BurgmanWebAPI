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
    public class CustomerModel
    {
        #region Public Methods

        public List<CustomerVO> GetCustomer(int tradeID, DateTime updatedAt)
        {
            List<CustomerVO> result = new List<CustomerVO>();

            string sql = "SELECT [Codigo], dbo.Proper([NomeFantasia]) as [NomeFantasia], dbo.Proper([RazaoSocial]) as [RazaoSocial], [TipoPessoa], "
                        + "[DDDCelularOperacional], [CelularOperacional], [EmailOperacional], [CEPOperacional], dbo.Proper([LogradouroOperacional]) as [LogradouroOperacional], "
                        + "[NumeroOperacional], [ComplementoOperacional], dbo.Proper([BairroOperacional]) as [BairroOperacional], [CodigoEstadoFiscal], [CodigoCidadeFiscal], "
                        + "[DDDTelefoneOperacional], [TelefoneOperacional], [DDDFaxOperacional], [FaxOperacional], [CPFOperacional], [CNPJOperacional], [InscEstadualOperacional], "
                        + "[InscMunicipalOperacional], [CdTipoLogradouroOperacional], [DataCadastro], [CodRepresentante], [CodigoTabelaPreco], [FlagMobile] "
                        + "FROM [Empresa] "
                        + "WHERE CodRepresentante = @CodRepresentante "
                        + "AND CONVERT(VARCHAR,DataCadastro,120) > CONVERT(VARCHAR,@DataCadastro,120) "
                        + "AND CodigoCategoria = 5 -- Clientes "
                        + "ORDER BY DataCadastro ASC ";

            using (SqlHelper db = new SqlHelper())
            using (SqlDataReader rdr = db.ExecDataReader(sql, "@CodRepresentante", tradeID, "@DataCadastro", updatedAt))
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

        public int PostCustomer(CustomerVO customer)
        {
            int customerID = default(int);
            if (customer != null)
            {
                using (SqlHelper db = new SqlHelper())
                {
                    string sql = "INSERT INTO Empresa ("
                                + " [NomeFantasia],"
                                + " [RazaoSocial],"
                                + " [TipoPessoa],"
                                + " [DDDCelularOperacional],"
                                + " [CelularOperacional],"
                                + " [EmailOperacional],"
                                + " [CEPOperacional],"
                                + " [LogradouroOperacional],"
                                + " [NumeroOperacional],"
                                + " [ComplementoOperacional],"
                                + " [BairroOperacional],"
                                + " [CodigoEstadoOperacional],"
                                + " [CodigoCidadeOperacional],"
                                + " [DDDTelefoneOperacional],"
                                + " [TelefoneOperacional],"
                                + " [DDDFaxOperacional],"
                                + " [FaxOperacional],"
                                + " [CPFOperacional],"
                                + " [CNPJOperacional],"
                                + " [InscEstadualOperacional],"
                                + " [InscMunicipalOperacional],"
                                + " [CodigoCategoria],"
                                + " [CodigoPaisOperacional],"
                                + " [CodTipoMovimentacao],"
                                + " [CdTipoLogradouroOperacional],"
                                + " CodigoEstadoFiscal,"
                                + " CodigoCidadeFiscal,"
                                + " CodigoPaisFiscal,"
                                + " CEPFiscal,"
                                + " LogradouroFiscal,"
                                + " NumeroFiscal,"
                                + " ComplementoFiscal,"
                                + " BairroFiscal,"
                                + " DDDTelefoneFiscal,"
                                + " TelefoneFiscal,"
                                + " DDDFaxFiscal,"
                                + " FaxFiscal,"
                                + " EmailFiscal,"
                                + " CNPJFiscal,"
                                + " CPFFiscal,"
                                + " InscEstadualFiscal,"
                                + " InscMunicipalFiscal,"
                                + " CdTipoLogradouroFiscal,"
                                + " CodigoEstadoCobranca,"
                                + " CodigoCidadeCobranca,"
                                + " CodigoPaisCobranca,"
                                + " CEPCobranca,"
                                + " LogradouroCobranca,"
                                + " NumeroCobranca,"
                                + " ComplementoCobranca,"
                                + " BairroCobranca,"
                                + " DDDTelefoneCobranca,"
                                + " TelefoneCobranca,"
                                + " DDDFaxCobranca,"
                                + " FaxCobranca,"
                                + " EmailCobranca,"
                                + " CNPJCobranca,"
                                + " CPFCobranca,"
                                + " InscEstadualCobranca,"
                                + " InscMunicipalCobranca,"
                                + " CdTipoLogradouroCobranca,"
                                + " DataCadastro,"
                                + " CodRepresentante,"
                                + " CodigoTabelaPreco,"
                                + " FlagMobile )"
                                + " VALUES ( "
                                + " dbo.Proper(@NomeFantasia),"
                                + " dbo.Proper(@RazaoSocial),"
                                + " @TipoPessoa,"
                                + " @DDDCelularOperacional,"
                                + " @CelularOperacional,"
                                + " @EmailOperacional,"
                                + " @CEPOperacional,"
                                + " dbo.Proper(@LogradouroOperacional),"
                                + " @NumeroOperacional,"
                                + " dbo.Proper(@ComplementoOperacional),"
                                + " dbo.Proper(@BairroOperacional),"
                                + " @CodigoEstadoFiscal,"
                                + " @CodigoCidadeFiscal,"
                                + " @DDDTelefoneOperacional,"
                                + " @TelefoneOperacional,"
                                + " @DDDFaxOperacional,"
                                + " @FaxOperacional,"
                                + " @CPFOperacional,"
                                + " @CNPJOperacional,"
                                + " @InscEstadualOperacional,"
                                + " @InscMunicipalOperacional,"
                                + " 5,"
                                + " 30,"
                                + " 1,"
                                + " @TipoLogradouro,"
                                + " @CodigoEstadoFiscal,"
                                + " @CodigoCidadeFiscal,"
                                + " 30,"
                                + " @CEPOperacional,"
                                + " dbo.Proper(@LogradouroOperacional),"
                                + " @NumeroOperacional,"
                                + " dbo.Proper(@ComplementoOperacional),"
                                + " dbo.Proper(@BairroOperacional),"
                                + " @DDDTelefoneOperacional,"
                                + " @TelefoneOperacional,"
                                + " @DDDFaxOperacional,"
                                + " @FaxOperacional,"
                                + " @EmailOperacional,"
                                + " @CNPJOperacional,"
                                + " @CPFOperacional,"
                                + " @InscEstadualOperacional,"
                                + " @InscMunicipalOperacional,"
                                + " @TipoLogradouro,"
                                + " @CodigoEstadoFiscal,"
                                + " @CodigoCidadeFiscal,"
                                + " 30,"
                                + " @CEPOperacional,"
                                + " dbo.Proper(@LogradouroOperacional),"
                                + " @NumeroOperacional,"
                                + " dbo.Proper(@ComplementoOperacional),"
                                + " dbo.Proper(@BairroOperacional),"
                                + " @DDDTelefoneOperacional,"
                                + " @TelefoneOperacional,"
                                + " @DDDFaxOperacional,"
                                + " @FaxOperacional,"
                                + " @EmailOperacional,"
                                + " @CNPJOperacional,"
                                + " @CPFOperacional,"
                                + " @InscEstadualOperacional,"
                                + " @InscMunicipalOperacional,"
                                + " @TipoLogradouro,"
                                + " @DataCadastro,"
                                + " @CodRepresentante,"
                                + " @CodigoTabelaPreco,"
                                + " @FlagMobile "
                                + " ) "
                                + "SELECT CAST(scope_identity() AS int) as Codigo ";

                    customerID = (int)db.ExecScalar(sql,
                             new SqlParameter("@NomeFantasia", customer.FantasyName),
                             new SqlParameter("@RazaoSocial", customer.CompanyName),
                             new SqlParameter("@TipoPessoa", customer.CompanyType.ToUpper()),
                             new SqlParameter("@DDDCelularOperacional", customer.AreaCodeCell),
                             new SqlParameter("@CelularOperacional", customer.Cell),
                             new SqlParameter("@EmailOperacional", customer.Email),
                             new SqlParameter("@CEPOperacional", customer.ZipCode),
                             new SqlParameter("@LogradouroOperacional", customer.Address),
                             new SqlParameter("@NumeroOperacional", customer.Number),
                             new SqlParameter("@ComplementoOperacional", customer.Complement),
                             new SqlParameter("@BairroOperacional", customer.Neighborhood),
                             new SqlParameter("@CodigoEstadoFiscal", customer.StateCode),
                             new SqlParameter("@CodigoCidadeFiscal", customer.CityCode),
                             new SqlParameter("@DDDTelefoneOperacional", customer.AreaCodePhone),
                             new SqlParameter("@TelefoneOperacional", customer.Phone),
                             new SqlParameter("@DDDFaxOperacional", customer.AreaCodeFax),
                             new SqlParameter("@FaxOperacional", customer.Fax),
                             new SqlParameter("@CPFOperacional", customer.CompanyType.ToUpper().Equals("F") ? customer.CPF : DBNull.Value.ToString()),
                             new SqlParameter("@CNPJOperacional", customer.CompanyType.ToUpper().Equals("J") ? customer.CNPJ : DBNull.Value.ToString()),
                             new SqlParameter("@InscEstadualOperacional", customer.StateRegistration),
                             new SqlParameter("@InscMunicipalOperacional", customer.CityRegistration),
                             new SqlParameter("@TipoLogradouro", customer.AddressType),
                             new SqlParameter("@DataCadastro", customer.UpdatedAt),
                             new SqlParameter("@CodRepresentante", customer.TradeID),
                             new SqlParameter("@CodigoTabelaPreco", customer.CompanyType.ToUpper().Equals("F") ? 2 : 3),
                             new SqlParameter("@FlagMobile", 1));
                }
            }

            return customerID;
        }

        #endregion

        #region Private Methods

        private CustomerVO ReadLoadEntity(IDataRecord rdr)
        {
            CustomerVO result = new CustomerVO();

            result.ID = rdr.GetInt32(0);
            result.FantasyName = rdr.GetString(1);
            result.CompanyName = rdr.GetString(2);
            result.CompanyType = rdr.GetString(3);
            result.AreaCodeCell = rdr.IsDBNull(4) ? default(string) : rdr.GetString(4);
            result.Cell = rdr.IsDBNull(5) ? default(string) : rdr.GetString(5);
            result.Email = rdr.IsDBNull(6) ? default(string) : rdr.GetString(6);
            result.ZipCode = rdr.GetString(7);
            result.Address = rdr.GetString(8);
            result.Number = rdr.GetString(9);
            result.Complement = rdr.IsDBNull(10) ? default(string) : rdr.GetString(10);
            result.Neighborhood = rdr.GetString(11);
            result.StateCode = rdr.GetInt32(12);
            result.CityCode = rdr.GetInt32(13);
            result.AreaCodePhone = rdr.IsDBNull(14) ? default(string) : rdr.GetString(14);
            result.Phone = rdr.IsDBNull(15) ? default(string) : rdr.GetString(15);
            result.AreaCodeFax = rdr.IsDBNull(16) ? default(string) : rdr.GetString(16);
            result.Fax = rdr.IsDBNull(17) ? default(string) : rdr.GetString(17);
            result.CPF = rdr.IsDBNull(18) ? default(string) : rdr.GetString(18);
            result.CNPJ = rdr.IsDBNull(19) ? default(string) : rdr.GetString(19);
            result.StateRegistration = rdr.IsDBNull(20) ? default(string) : rdr.GetString(20);
            result.CityRegistration = rdr.IsDBNull(21) ? default(string) : rdr.GetString(21);
            result.AddressType = rdr.GetInt32(22);
            result.UpdatedAt = rdr.GetDateTime(23);
            result.TradeID = rdr.GetInt32(24);
            result.TablePriceID = rdr.IsDBNull(25) ? default(int) : rdr.GetInt32(25);

            return result;
        }

        #endregion
    }
}