using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces;
using PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Pages.Eyeglasses
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

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
            ViewData["LensTypeName"] = new SelectList(_unitOfWork.LensTypeRepository.Get(), "LensTypeId", "LensTypeName");
            return Page();
        }

        [BindProperty]
        public Eyeglass Eyeglass { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
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
            if (!ModelState.IsValid || Eyeglass == null)
            {
                return Page();
            }

            try
            {
                _unitOfWork.EyeglassRepository.Insert(Eyeglass);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while creating the product: {ex.Message}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
