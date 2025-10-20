namespace No._18.Models
{
    public class CaseIndexViewModel
    {
        // 用於存放左側列表的所有案件
        public IEnumerable<CaseModel> AllCases { get; set; }

        // 用於存放右側要顯示細節的單一案件
        public CaseModel SelectedCase { get; set; }

        // 用於存放並回傳使用者輸入的搜尋關鍵字
        public string SearchString { get; set; }
    }
}
