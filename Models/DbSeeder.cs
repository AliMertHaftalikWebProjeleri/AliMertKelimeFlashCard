using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AliMertKelimeEzberleme.Models
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            context.Database.EnsureCreated();

            if (!context.Languages.Any())
            {
                context.Languages.AddRange(
                    new Language { Name = "İngilizce" },
                    new Language { Name = "Almanca" },
                    new Language { Name = "İspanyolca" }
                );
                await context.SaveChangesAsync();
            }

            var engId = context.Languages.FirstOrDefault(l => l.Name == "İngilizce")?.Id;
            if (engId.HasValue && context.Flashcards.Count(f => f.LanguageId == engId.Value) < 25)
            {
                var existingEng = context.Flashcards.Where(f => f.LanguageId == engId.Value).ToList();
                context.Flashcards.RemoveRange(existingEng);
                await context.SaveChangesAsync();

                context.Flashcards.AddRange(
                    new Flashcard { Word = "Time", Meaning = "Zaman", LanguageId = engId.Value, ExampleSentence = "Time is money." },
                    new Flashcard { Word = "Year", Meaning = "Yıl", LanguageId = engId.Value, ExampleSentence = "Happy new year!" },
                    new Flashcard { Word = "People", Meaning = "İnsanlar", LanguageId = engId.Value, ExampleSentence = "Many people live in this city." },
                    new Flashcard { Word = "Way", Meaning = "Yol", LanguageId = engId.Value, ExampleSentence = "Can you show me the way?" },
                    new Flashcard { Word = "Day", Meaning = "Gün", LanguageId = engId.Value, ExampleSentence = "It is a beautiful day." },
                    new Flashcard { Word = "Man", Meaning = "Adam", LanguageId = engId.Value, ExampleSentence = "The man is walking." },
                    new Flashcard { Word = "Thing", Meaning = "Şey", LanguageId = engId.Value, ExampleSentence = "What is this thing?" },
                    new Flashcard { Word = "Woman", Meaning = "Kadın", LanguageId = engId.Value, ExampleSentence = "The woman is reading a book." },
                    new Flashcard { Word = "Life", Meaning = "Hayat", LanguageId = engId.Value, ExampleSentence = "Life is beautiful." },
                    new Flashcard { Word = "Child", Meaning = "Çocuk", LanguageId = engId.Value, ExampleSentence = "The child is playing." },
                    new Flashcard { Word = "World", Meaning = "Dünya", LanguageId = engId.Value, ExampleSentence = "We live in a big world." },
                    new Flashcard { Word = "School", Meaning = "Okul", LanguageId = engId.Value, ExampleSentence = "I go to school every day." },
                    new Flashcard { Word = "State", Meaning = "Durum / Devlet", LanguageId = engId.Value, ExampleSentence = "The state of the economy is good." },
                    new Flashcard { Word = "Family", Meaning = "Aile", LanguageId = engId.Value, ExampleSentence = "I love my family." },
                    new Flashcard { Word = "Student", Meaning = "Öğrenci", LanguageId = engId.Value, ExampleSentence = "She is a smart student." },
                    new Flashcard { Word = "Group", Meaning = "Grup", LanguageId = engId.Value, ExampleSentence = "We are in the same group." },
                    new Flashcard { Word = "Country", Meaning = "Ülke", LanguageId = engId.Value, ExampleSentence = "Turkey is my country." },
                    new Flashcard { Word = "Problem", Meaning = "Sorun", LanguageId = engId.Value, ExampleSentence = "I have a big problem." },
                    new Flashcard { Word = "Hand", Meaning = "El", LanguageId = engId.Value, ExampleSentence = "Wash your hands." },
                    new Flashcard { Word = "Part", Meaning = "Bölüm", LanguageId = engId.Value, ExampleSentence = "This is the best part." },
                    new Flashcard { Word = "Place", Meaning = "Yer", LanguageId = engId.Value, ExampleSentence = "This place is amazing." },
                    new Flashcard { Word = "Case", Meaning = "Kasa / Durum", LanguageId = engId.Value, ExampleSentence = "In this case, we must wait." },
                    new Flashcard { Word = "Week", Meaning = "Hafta", LanguageId = engId.Value, ExampleSentence = "See you next week." },
                    new Flashcard { Word = "Company", Meaning = "Şirket", LanguageId = engId.Value, ExampleSentence = "He works for a big company." },
                    new Flashcard { Word = "System", Meaning = "Sistem", LanguageId = engId.Value, ExampleSentence = "The system is down." }
                );
            }

            var gerId = context.Languages.FirstOrDefault(l => l.Name == "Almanca")?.Id;
            if (gerId.HasValue && context.Flashcards.Count(f => f.LanguageId == gerId.Value) < 25)
            {
                var existingGer = context.Flashcards.Where(f => f.LanguageId == gerId.Value).ToList();
                context.Flashcards.RemoveRange(existingGer);
                await context.SaveChangesAsync();

                context.Flashcards.AddRange(
                    new Flashcard { Word = "Zeit", Meaning = "Zaman", LanguageId = gerId.Value, ExampleSentence = "Ich habe keine Zeit." },
                    new Flashcard { Word = "Jahr", Meaning = "Yıl", LanguageId = gerId.Value, ExampleSentence = "Frohes neues Jahr!" },
                    new Flashcard { Word = "Leute", Meaning = "İnsanlar", LanguageId = gerId.Value, ExampleSentence = "Viele Leute sind hier." },
                    new Flashcard { Word = "Weg", Meaning = "Yol", LanguageId = gerId.Value, ExampleSentence = "Das ist der richtige Weg." },
                    new Flashcard { Word = "Tag", Meaning = "Gün", LanguageId = gerId.Value, ExampleSentence = "Guten Tag!" },
                    new Flashcard { Word = "Mann", Meaning = "Adam", LanguageId = gerId.Value, ExampleSentence = "Der Mann arbeitet." },
                    new Flashcard { Word = "Ding", Meaning = "Şey", LanguageId = gerId.Value, ExampleSentence = "Was ist dieses Ding?" },
                    new Flashcard { Word = "Frau", Meaning = "Kadın", LanguageId = gerId.Value, ExampleSentence = "Die Frau liest ein Buch." },
                    new Flashcard { Word = "Leben", Meaning = "Hayat", LanguageId = gerId.Value, ExampleSentence = "Das Leben ist schön." },
                    new Flashcard { Word = "Kind", Meaning = "Çocuk", LanguageId = gerId.Value, ExampleSentence = "Das Kind spielt." },
                    new Flashcard { Word = "Welt", Meaning = "Dünya", LanguageId = gerId.Value, ExampleSentence = "Die Welt ist groß." },
                    new Flashcard { Word = "Schule", Meaning = "Okul", LanguageId = gerId.Value, ExampleSentence = "Ich gehe zur Schule." },
                    new Flashcard { Word = "Staat", Meaning = "Devlet", LanguageId = gerId.Value, ExampleSentence = "Der Staat hilft uns." },
                    new Flashcard { Word = "Familie", Meaning = "Aile", LanguageId = gerId.Value, ExampleSentence = "Meine Familie ist klein." },
                    new Flashcard { Word = "Student", Meaning = "Öğrenci", LanguageId = gerId.Value, ExampleSentence = "Er ist ein guter Student." },
                    new Flashcard { Word = "Gruppe", Meaning = "Grup", LanguageId = gerId.Value, ExampleSentence = "Unsere Gruppe ist die beste." },
                    new Flashcard { Word = "Urlaub", Meaning = "Tatil", LanguageId = gerId.Value, ExampleSentence = "Ich brauche Urlaub." },
                    new Flashcard { Word = "Problem", Meaning = "Sorun", LanguageId = gerId.Value, ExampleSentence = "Kein Problem!" },
                    new Flashcard { Word = "Hand", Meaning = "El", LanguageId = gerId.Value, ExampleSentence = "Gib mir deine Hand." },
                    new Flashcard { Word = "Teil", Meaning = "Bölüm", LanguageId = gerId.Value, ExampleSentence = "Das ist ein Teil." },
                    new Flashcard { Word = "Platz", Meaning = "Yer", LanguageId = gerId.Value, ExampleSentence = "Hier ist mein Platz." },
                    new Flashcard { Word = "Gesetz", Meaning = "Yasa", LanguageId = gerId.Value, ExampleSentence = "Das Gesetz ist streng." },
                    new Flashcard { Word = "Woche", Meaning = "Hafta", LanguageId = gerId.Value, ExampleSentence = "Bis nächste Woche." },
                    new Flashcard { Word = "Arbeit", Meaning = "İş", LanguageId = gerId.Value, ExampleSentence = "Ich liebe meine Arbeit." },
                    new Flashcard { Word = "Tisch", Meaning = "Masa", LanguageId = gerId.Value, ExampleSentence = "Der Tisch ist neu." }
                );
            }

            var spaId = context.Languages.FirstOrDefault(l => l.Name == "İspanyolca")?.Id;
            if (spaId.HasValue && context.Flashcards.Count(f => f.LanguageId == spaId.Value) < 25)
            {
                var existingSpa = context.Flashcards.Where(f => f.LanguageId == spaId.Value).ToList();
                context.Flashcards.RemoveRange(existingSpa);
                await context.SaveChangesAsync();

                context.Flashcards.AddRange(
                    new Flashcard { Word = "Tiempo", Meaning = "Zaman", LanguageId = spaId.Value, ExampleSentence = "No tengo tiempo." },
                    new Flashcard { Word = "Año", Meaning = "Yıl", LanguageId = spaId.Value, ExampleSentence = "Feliz año nuevo!" },
                    new Flashcard { Word = "Gente", Meaning = "İnsanlar", LanguageId = spaId.Value, ExampleSentence = "Hay mucha gente aquí." },
                    new Flashcard { Word = "Camino", Meaning = "Yol", LanguageId = spaId.Value, ExampleSentence = "Sigue este camino." },
                    new Flashcard { Word = "Día", Meaning = "Gün", LanguageId = spaId.Value, ExampleSentence = "Buenos días!" },
                    new Flashcard { Word = "Hombre", Meaning = "Adam", LanguageId = spaId.Value, ExampleSentence = "El hombre camina." },
                    new Flashcard { Word = "Cosa", Meaning = "Şey", LanguageId = spaId.Value, ExampleSentence = "Qué es esta cosa?" },
                    new Flashcard { Word = "Mujer", Meaning = "Kadın", LanguageId = spaId.Value, ExampleSentence = "La mujer lee." },
                    new Flashcard { Word = "Vida", Meaning = "Hayat", LanguageId = spaId.Value, ExampleSentence = "La vida es bella." },
                    new Flashcard { Word = "Niño", Meaning = "Çocuk", LanguageId = spaId.Value, ExampleSentence = "El niño juega." },
                    new Flashcard { Word = "Mundo", Meaning = "Dünya", LanguageId = spaId.Value, ExampleSentence = "El mundo es pequeño." },
                    new Flashcard { Word = "Escuela", Meaning = "Okul", LanguageId = spaId.Value, ExampleSentence = "Voy a la escuela." },
                    new Flashcard { Word = "Estado", Meaning = "Devlet", LanguageId = spaId.Value, ExampleSentence = "El estado de ánimo es bueno." },
                    new Flashcard { Word = "Familia", Meaning = "Aile", LanguageId = spaId.Value, ExampleSentence = "Amo a mi familia." },
                    new Flashcard { Word = "Estudiante", Meaning = "Öğrenci", LanguageId = spaId.Value, ExampleSentence = "Soy un estudiante." },
                    new Flashcard { Word = "Grupo", Meaning = "Grup", LanguageId = spaId.Value, ExampleSentence = "Es un buen grupo." },
                    new Flashcard { Word = "País", Meaning = "Ülke", LanguageId = spaId.Value, ExampleSentence = "Turquía es mi país." },
                    new Flashcard { Word = "Problema", Meaning = "Sorun", LanguageId = spaId.Value, ExampleSentence = "No hay problema." },
                    new Flashcard { Word = "Mano", Meaning = "El", LanguageId = spaId.Value, ExampleSentence = "Lava tus manos." },
                    new Flashcard { Word = "Parte", Meaning = "Bölüm", LanguageId = spaId.Value, ExampleSentence = "Es una buena parte." },
                    new Flashcard { Word = "Lugar", Meaning = "Yer", LanguageId = spaId.Value, ExampleSentence = "Este lugar es hermoso." },
                    new Flashcard { Word = "Caso", Meaning = "Durum", LanguageId = spaId.Value, ExampleSentence = "En este caso, si." },
                    new Flashcard { Word = "Semana", Meaning = "Hafta", LanguageId = spaId.Value, ExampleSentence = "Buena semana." },
                    new Flashcard { Word = "Libro", Meaning = "Kitap", LanguageId = spaId.Value, ExampleSentence = "Me gusta el libro." },
                    new Flashcard { Word = "Trabajo", Meaning = "İş", LanguageId = spaId.Value, ExampleSentence = "El trabajo duro." }
                );
            }
            await context.SaveChangesAsync();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var adminEmail = "admin@flashcard.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var langId = context.Languages.FirstOrDefault()?.Id;
                var newAdmin = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Sistem Yöneticisi",
                    PreferredLanguageId = langId
                };
                var result = await userManager.CreateAsync(newAdmin, "Admin123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}
