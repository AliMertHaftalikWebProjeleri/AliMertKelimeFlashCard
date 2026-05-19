using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AliMertKelimeEzberleme.Models
{
    public class Language
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public ICollection<Flashcard> Flashcards { get; set; }
        public ICollection<AppUser> Users { get; set; }
    }
}
