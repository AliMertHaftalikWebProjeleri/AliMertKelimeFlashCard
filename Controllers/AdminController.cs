using AliMertKelimeEzberleme.Models;
using ClosedXML.Excel;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliMertKelimeEzberleme.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var users = await _userManager.Users.Include(u => u.PreferredLanguage).ToListAsync();
            
            var languageStats = users
                .Where(u => u.PreferredLanguage != null)
                .GroupBy(u => u.PreferredLanguage.Name)
                .Select(g => new { Language = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.LanguageLabels = System.Text.Json.JsonSerializer.Serialize(languageStats.Select(s => s.Language).ToArray());
            ViewBag.LanguageData = System.Text.Json.JsonSerializer.Serialize(languageStats.Select(s => s.Count).ToArray());

            return View(users);
        }

        public async Task<IActionResult> ExportExcel()
        {
            var users = await _userManager.Users.Include(u => u.PreferredLanguage).ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Kullanıcılar");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Ad Soyad";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "E-posta";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Tercih Edilen Dil";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.FullName;
                    worksheet.Cell(currentRow, 2).Value = user.Email;
                    worksheet.Cell(currentRow, 3).Value = user.PreferredLanguage?.Name ?? "Belirtilmemiş";
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Kullanicilar.xlsx");
                }
            }
        }

        public async Task<IActionResult> ExportPdf()
        {
            var users = await _userManager.Users.Include(u => u.PreferredLanguage).ToListAsync();
            
            var sb = new StringBuilder();
            sb.Append("<html><head><meta charset='UTF-8'></head><body style='font-family: sans-serif; margin: 20px;'>");
            sb.Append("<h2 style='text-align:center;'>Sistem Kullanıcıları ve Dil Tercihleri</h2>");
            sb.Append("<table border='1' cellpadding='8' cellspacing='0' style='width: 100%; border-collapse: collapse; margin-top:20px;'>");
            sb.Append("<tr><th style='background-color:#0d6efd; color:white;'>Ad Soyad</th><th style='background-color:#0d6efd; color:white;'>E-posta</th><th style='background-color:#0d6efd; color:white;'>Tercih Edilen Dil</th></tr>");

            foreach (var user in users)
            {
                sb.Append($"<tr><td>{user.FullName}</td><td>{user.Email}</td><td>{(user.PreferredLanguage?.Name ?? "Belirtilmemiş")}</td></tr>");
            }
            sb.Append("</table></body></html>");

            using (var workStream = new MemoryStream())
            {
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    pdfWriter.SetCloseStream(false);
                    var pdfDocument = new PdfDocument(pdfWriter);
                    HtmlConverter.ConvertToPdf(sb.ToString(), pdfDocument, new ConverterProperties());
                }
                
                workStream.Position = 0;
                return File(workStream.ToArray(), "application/pdf", "Kullanicilar.pdf");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Prevent admin from deleting themselves
                var currentUserId = _userManager.GetUserId(User);
                if (user.Id == currentUserId)
                {
                    TempData["Error"] = "Kendinizi silemezsiniz!";
                    return RedirectToAction(nameof(Dashboard));
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Kullanıcı başarıyla silindi.";
                }
                else
                {
                    TempData["Error"] = "Kullanıcı silinirken bir hata oluştu.";
                }
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
