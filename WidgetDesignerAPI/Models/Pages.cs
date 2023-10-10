using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetDesignerAPI.Models
{
    public class Pages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(200)]
        public string PageName { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public string DataSourceJson { get; set; }
        public string PageHtml { get; set; }
        public string PageContent { get; set; }

        [MaxLength(200)]
        public string? PageCSSUrl { get; set; }

        public decimal? PageWidth { get; set; }
        public decimal? PageHeight { get; set;}
    }
}
