using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AliMertKelimeEzberleme.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        
        public int? PreferredLanguageId { get; set; }
        [ForeignKey("PreferredLanguageId")]
        public virtual Language? PreferredLanguage { get; set; }
    }
}
