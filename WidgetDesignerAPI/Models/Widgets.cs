using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetDesignerAPI.Models
{
    public class Widgets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(200)]
        public string WidgetName { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string DataSourceJson { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartCol { get; set; }
        public int StartRow { get; set; }
        [MaxLength(200)]
        public string DataBindingJsonNode { get; set; }
        [MaxLength(200)]
        public string FontName { get; set; }
        [Column(TypeName = "text")]
        [MaxLength]
        public string WidgetHtml { get; set; }
        [MaxLength(200)]
        public string? WidgetIconUrl { get; set; }
        [MaxLength(200)]
        public string? WidgetCSSUrl { get; set; }
        public string? WidgetCSS { get; set; }
    }
}
