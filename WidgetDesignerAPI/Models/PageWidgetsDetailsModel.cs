using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetDesignerAPI.Models
{
    public class PageWidgetsDetailsModel
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public int WidgetId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartCol { get; set; }
        public int StartRow { get; set; }
    }
}
