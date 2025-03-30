using Websitebanhang.Models;
using Websitebanhang.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace WebBanHang2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddAsync(category);
                TempData["SuccessMessage"] = "Danh mục đã được thêm thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Display(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return View(category);
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = category.Name;

            await _categoryRepository.UpdateAsync(existingCategory);
            TempData["SuccessMessage"] = "Danh mục đã được cập nhật thành công!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            TempData["SuccessMessage"] = "Danh mục đã được xóa thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}