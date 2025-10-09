using System.ComponentModel.DataAnnotations;

namespace No._18.Models
{
    public class CaseModel
    {
        [Key]
        public int Id { get; set; } // 資料庫自動編號

        [Required]
        [Display(Name = "案件編號")]
        public string CaseNumber { get; set; }

        [Required]
        [Display(Name = "公司名稱")]
        public string CompanyName { get; set; }

        [Display(Name = "種類")]
        public string Type { get; set; }

        [Display(Name = "負責人姓名")]
        public string ResponsiblePerson { get; set; }

        [Phone]
        [Display(Name = "電話")]
        public string Phone { get; set; }

        [EmailAddress]
        [Display(Name = "信箱")]
        public string Email { get; set; }
    }
}
