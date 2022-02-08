using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaCore_BDEO.Models.Intervention
{
    public class User
    {
        [Required]
        [MaxLength(40)]
        public string id { get; set; }

        [Required]
        [MaxLength(40)]
        public string company_id { get; set; }

        [Required]
        public string company_name { get; set; }

        [Required]
        public string company_code { get; set; }

        [Required]
        [MaxLength(6)]
        public string company_type { get; set; }

        [Required]
        [MaxLength(10)]
        public string service_code { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string surname { get; set; }
        public string document_number { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        [MaxLength(50)]
        public List<string> type { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int? zip { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public int status { get; set; }
        public string avatar { get; set; }
        public string signature { get; set; }
        public string access_token { get; set; }
        public int createdAt { get; set; }

        [Required]
        public string createdBy { get; set; }
        public int? updatedAt { get; set; }
        public string updatedBy { get; set; }

        //Otras propiedades
        public int loginAttemps { get; set; }
        public int expirationTime { get; set; }
        public int lastLoginAttemptAt { get; set; }
        public string jwt_token { get; set; }
        public CompanySettings companySettings { get; set; }
    }
}