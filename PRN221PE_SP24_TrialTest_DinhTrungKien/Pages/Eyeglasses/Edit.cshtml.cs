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
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public ValidEyeglassesData ValidEyeglassesData { get; set; } = default!;

        public IActionResult OnGet(int? id)
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
            if (id == null || _unitOfWork.EyeglassRepository.GetByID(id) == null)
            {
                return NotFound();
            }

            var eyeglass = _unitOfWork.EyeglassRepository.GetByID(id);
            if (eyeglass == null)
            {
                return NotFound();
            }
            var validEyeglassesData = new ValidEyeglassesData
            {
                EyeglassesId = eyeglass.EyeglassesId,
                EyeglassesName = eyeglass.EyeglassesName,
                EyeglassesDescription = eyeglass.EyeglassesDescription,
                FrameColor = eyeglass.FrameColor,
                Quantity = eyeglass.Quantity,
                Price = eyeglass.Price,
                CreatedDate = eyeglass.CreatedDate,
                LensTypeId = eyeglass.LensTypeId
            };
            ValidEyeglassesData = validEyeglassesData;
            ViewData["LensTypeName"] = new SelectList(_unitOfWork.LensTypeRepository.Get(), "LensTypeId", "LensTypeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newEyeglassInfo = new Eyeglass
            {
                EyeglassesId = ValidEyeglassesData.EyeglassesId,
                EyeglassesName = ValidEyeglassesData.EyeglassesName,
                EyeglassesDescription = ValidEyeglassesData.EyeglassesDescription,
                FrameColor = ValidEyeglassesData.FrameColor,
                Quantity = ValidEyeglassesData.Quantity,
                Price = ValidEyeglassesData.Price,
                CreatedDate = ValidEyeglassesData.CreatedDate,
                LensTypeId = ValidEyeglassesData.LensTypeId
            };
            _unitOfWork.EyeglassRepository.Update(newEyeglassInfo);

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EyeglassExists(ValidEyeglassesData.EyeglassesId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EyeglassExists(int id)
        {
            return _unitOfWork.EyeglassRepository.Find(e => e.EyeglassesId == id).Any();
        }
    }
}
