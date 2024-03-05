using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces;
using PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Pages.Eyeglasses
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Eyeglass> Eyeglass { get;set; } = default!;

        public IActionResult OnGet()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var userRole = session.GetString("Role");
            if (String.IsNullOrEmpty(userRole))
            {
                return RedirectToPage("/Index");
            }
            if (!userRole.Equals("1") && !userRole.Equals("2"))
            {
                return RedirectToPage("/Index");
            }
            var eyeglassList = _unitOfWork.EyeglassRepository.Get(includeProperties: "LensType");
            Eyeglass = eyeglassList.ToList();
            return Page();
        }
    }
}
