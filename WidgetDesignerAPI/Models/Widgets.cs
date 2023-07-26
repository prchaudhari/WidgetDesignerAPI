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
        public string WidgetHtml { get; set; }
        public string WidgetIconUrl { get; set; }

    }
}
