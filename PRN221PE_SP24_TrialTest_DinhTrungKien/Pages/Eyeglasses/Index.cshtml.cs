using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Implements;
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
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }

        public IActionResult OnGet(int? pageIndex, string? searchInput, int? minPrice, int? maxPrice)
        {
            int totalItems;
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
            ViewData["SearchInput"] = searchInput;
            ViewData["MinPriceInput"] = minPrice;
            ViewData["MaxPriceInput"] = maxPrice;

            totalItems = CountSearchingMinMaxPrice(searchInput, minPrice, maxPrice);

            TotalPages = (int)Math.Ceiling(totalItems / (double)4); // Tính tổng số trang

            CurrentPage = pageIndex ?? 1; // Lấy số trang hiện tại, mặc định là trang 1

            if (CurrentPage < 1)
                CurrentPage = 1;
            else if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;

            GetSearchingList(searchInput, minPrice, maxPrice, CurrentPage);

            return Page();
        }

        public int CountSearchingMinMaxPrice(string? searchInput, int? minPrice, int? maxPrice)
        {
            int totalItems = 0;
            if (string.IsNullOrWhiteSpace(searchInput)) searchInput = "";
            if (minPrice < 0 || minPrice == null)
            {
                if (maxPrice < 0 || maxPrice == null)
                {
                    totalItems = _unitOfWork.EyeglassRepository.Count(filter: e => e.EyeglassesDescription.Contains(searchInput.Trim()));
                }
                else
                {
                    totalItems = _unitOfWork.EyeglassRepository.Count(filter: e => e.Price <= maxPrice && e.EyeglassesDescription.Contains(searchInput.Trim()));
                }
            }
            else
            {
                if (maxPrice < 0 || maxPrice == null || maxPrice < minPrice)
                {
                    totalItems = _unitOfWork.EyeglassRepository.Count(filter: e => e.Price >= minPrice && e.EyeglassesDescription.Contains(searchInput.Trim()));
                }
                else
                {
                    totalItems = _unitOfWork.EyeglassRepository.Count(filter: e => e.Price >= minPrice && e.Price <= maxPrice && e.EyeglassesDescription.Contains(searchInput.Trim()));
                }
            }
            return totalItems;
        }

        public void GetSearchingList(string? searchInput, int? minPrice, int? maxPrice, int CurrentPage)
        {
            if (string.IsNullOrWhiteSpace(searchInput)) searchInput = "";
            if (minPrice < 0 || minPrice == null)
            {
                if (maxPrice < 0 || maxPrice == null)
                {
                    Eyeglass = _unitOfWork.EyeglassRepository.Get(includeProperties: "LensType", filter: e => e.EyeglassesDescription.Contains(searchInput.Trim()), orderBy: q => q.OrderByDescending(e => e.CreatedDate), pageIndex: CurrentPage, pageSize: 4).ToList();
                }
                else
                {
                    Eyeglass = _unitOfWork.EyeglassRepository.Get(includeProperties: "LensType", filter: e => e.Price <= maxPrice && e.EyeglassesDescription.Contains(searchInput.Trim()), orderBy: q => q.OrderByDescending(e => e.CreatedDate), pageIndex: CurrentPage, pageSize: 4).ToList();
                }
            }
            else
            {
                if (maxPrice < 0 || maxPrice == null || maxPrice < minPrice)
                {
                    Eyeglass = _unitOfWork.EyeglassRepository.Get(includeProperties: "LensType", filter: e => e.Price >= minPrice && e.EyeglassesDescription.Contains(searchInput.Trim()), orderBy: q => q.OrderByDescending(e => e.CreatedDate), pageIndex: CurrentPage, pageSize: 4).ToList();
                }
                else
                {
                    Eyeglass = _unitOfWork.EyeglassRepository.Get(includeProperties: "LensType", filter: e => e.Price >= minPrice && e.Price <= maxPrice && e.EyeglassesDescription.Contains(searchInput.Trim()), orderBy: q => q.OrderByDescending(e => e.CreatedDate), pageIndex: CurrentPage, pageSize: 4).ToList();
                }
            }
        }
    }
}
