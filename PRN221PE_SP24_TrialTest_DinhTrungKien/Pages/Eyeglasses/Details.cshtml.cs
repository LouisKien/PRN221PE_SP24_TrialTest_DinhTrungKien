using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces;
using PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Pages.Eyeglasses
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public Eyeglass Eyeglass { get; set; } = default!; 

        public IActionResult OnGet(int? id)
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
            if (id == null || _unitOfWork.EyeglassRepository.GetByID(id) == null)
            {
                return NotFound();
            }

            var eyeglass = _unitOfWork.EyeglassRepository.GetByID(id);
            if (eyeglass == null)
            {
                return NotFound();
            }
            else 
            {
                Eyeglass = eyeglass;
            }
            ViewData["LensTypeName"] = new SelectList(_unitOfWork.LensTypeRepository.Get(), "LensTypeId", "LensTypeName");
            return Page();
        }
    }
}
