using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetDesignerAPI.Models
{
    public class WidgetModel
    {
        public int Id { get; set; }
        public string WidgetName { get; set; }
        public string Description { get; set; }
        public string DataSourceJson { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string WidgetHtml { get; set; }      
        public IFormFile? WidgetIconUrl { get; set; }       
        public string? WidgetCSSUrl { get; set; }
        public string? WidgetCSS { get; set; }
        public int StartCol { get; set; } = 0;
        public int StartRow { get; set; } = 0;
        public string DataBindingJsonNode { get; set; }
        public string FontName { get; set; }
    }
}
