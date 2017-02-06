using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.ValueObjects
{
    public class CustomerVO
    {
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "FantasyName")]
        public string FantasyName { get; set; }

        [JsonProperty(PropertyName = "CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "CompanyType")]
        public string CompanyType { get; set; }

        [JsonProperty(PropertyName = "AreaCodeCell")]
        public string AreaCodeCell { get; set; }

        [JsonProperty(PropertyName = "Cell")]
        public string Cell { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "ZipCode")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "Number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "Complement")]
        public string Complement { get; set; }

        [JsonProperty(PropertyName = "Neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty(PropertyName = "StateCode")]
        public int? StateCode { get; set; }

        [JsonProperty(PropertyName = "CityCode")]
        public int? CityCode { get; set; }

        [JsonProperty(PropertyName = "AreaCodePhone")]
        public string AreaCodePhone { get; set; }

        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "AreaCodeFax")]
        public string AreaCodeFax { get; set; }

        [JsonProperty(PropertyName = "Fax")]
        public string Fax { get; set; }

        [JsonProperty(PropertyName = "CPF")]
        public string CPF { get; set; }

        [JsonProperty(PropertyName = "CNPJ")]
        public string CNPJ { get; set; }

        [JsonProperty(PropertyName = "StateRegistration")]
        public string StateRegistration { get; set; }

        [JsonProperty(PropertyName = "CityRegistration")]
        public string CityRegistration { get; set; }

        [JsonProperty(PropertyName = "AddressType")]
        public int AddressType { get; set; }

        [JsonProperty(PropertyName = "UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "TradeID")]
        public int TradeID { get; set; }

        [JsonProperty(PropertyName = "TablePriceID")]
        public int TablePriceID { get; set; }
    }
}