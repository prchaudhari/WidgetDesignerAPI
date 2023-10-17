using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetDesignerAPI.Models
{
    public class PageGenerationLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PageId { get; set; }

        public string FullHTML { get; set; }
        [MaxLength(200)]
        public string FileName { get; set; }       
        public bool Status { get; set; } = false;
        public DateTime? CreationTime{ get; set; }
      
    }
}
