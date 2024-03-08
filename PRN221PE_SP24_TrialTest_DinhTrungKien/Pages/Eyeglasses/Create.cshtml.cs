using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (!userRole.Equals("1"))
            {
                return RedirectToPage("/Index");
            }
            ViewData["LensTypeName"] = new SelectList(_unitOfWork.LensTypeRepository.Get(), "LensTypeId", "LensTypeName");
            return Page();
        }

        [BindProperty]
        public ValidEyeglassesData ValidEyeglassesData { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var userRole = session.GetString("Role");
            if (String.IsNullOrEmpty(userRole))
            {
                return RedirectToPage("/Index");
            }
            if (!userRole.Equals("1"))
            {
                return RedirectToPage("/Index");
            }
            if (!ModelState.IsValid || ValidEyeglassesData == null)
            {
                return Page();
            }

            try
            {
                var newEyeglass = new Eyeglass
                {
                    EyeglassesId = ValidEyeglassesData.EyeglassesId,
                    EyeglassesName = ValidEyeglassesData.EyeglassesName,
                    EyeglassesDescription = ValidEyeglassesData.EyeglassesDescription,
                    FrameColor = ValidEyeglassesData.FrameColor,
                    Quantity = ValidEyeglassesData.Quantity,
                    Price = ValidEyeglassesData.Price,
                    CreatedDate = DateTime.Now,
                    LensTypeId = ValidEyeglassesData.LensTypeId
                };
                _unitOfWork.EyeglassRepository.Insert(newEyeglass);
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

    public class ValidEyeglassesData
    {
        [Required(ErrorMessage = "EyeglassesId is required")]
        public int EyeglassesId { get; set; }
        [Required(ErrorMessage = "EyeglassesName is required")]
        [MinLength(10, ErrorMessage = "EyeglassesName must be at least 10 characters.")]
        [RegularExpression(@"^(?:[A-Z][a-zA-Z0-9@#$&()]*\s*)+$", ErrorMessage = "Each word of EyeglassesName must begin with a capital letter and can include alphanumeric characters and special characters such as @, #, $, &, (, ).")]
        public string EyeglassesName { get; set; } = null!;
        [Required(ErrorMessage = "EyeglassesDescription is required")]
        public string? EyeglassesDescription { get; set; }
        [Required(ErrorMessage = "FrameColor is required")]
        public string? FrameColor { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 999, ErrorMessage = "Quantity Per Unit must be larger than 0 and smaller than 999.")]
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "LensTypeId is required")]
        public string? LensTypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
