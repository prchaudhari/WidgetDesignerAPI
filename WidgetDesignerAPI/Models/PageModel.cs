namespace WidgetDesignerAPI.Models
{
    public class PageModel
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string Description { get; set; }
        public string DataSourceJson { get; set; }
        public string PageHtml { get; set; }
        public string? PageCSSUrl { get; set; }
        public decimal? PageWidth { get; set; }
        public decimal? PageHeight { get; set; }
        public List<PageWidgetsDetailsModel> Widgets { get; set; }
    }
}
