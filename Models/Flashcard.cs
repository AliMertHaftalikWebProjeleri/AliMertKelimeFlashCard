using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AliMertKelimeEzberleme.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Word { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Meaning { get; set; }
        
        [StringLength(250)]
        public string? ExampleSentence { get; set; }
        
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}
