using System.ComponentModel.DataAnnotations;

namespace No._18.Models
{
    public class CaseModel
    {
        [Key]
        public int Id { get; set; } // 資料庫自動編號

        [Required] // NOT NULL
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

        [Display(Name = "案件狀態")]
        public CaseStatus Status { get; set; }
    }

    public enum CaseStatus  //列舉
    {
        [Display(Name = "收件")]
        Received,

        [Display(Name = "處理中")]
        Processing,

        [Display(Name = "待核對")]
        Review,

        [Display(Name = "已完成")]
        Completed
    }
}
