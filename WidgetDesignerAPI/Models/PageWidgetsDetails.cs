using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetDesignerAPI.Models
{
    public class PageWidgetsDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PageId { get; set; }

        [ForeignKey("PageId")]  
        public Pages Page { get; set; }

        public int WidgetId { get; set; }

        [ForeignKey("WidgetId")] 
        public Widgets Widget { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartCol { get; set; }
        public int StartRow { get; set; }
    }
}
