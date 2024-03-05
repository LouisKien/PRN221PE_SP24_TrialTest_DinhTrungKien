using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("Role");
            return RedirectToPage("/Login");
        }
    }
}
