using BookStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using BookStore.Models;
using BookStore.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.Models.ViewModels;

namespace Bookstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
		{
			_unitOfWork = unitOfWork;
			_productRepository = _unitOfWork.Product;
			_hostingEnvironment = hostingEnvironment;
		}

		public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                    c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                    c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
            };            
            if(id == null || id == 0)
            {
                return View(productVM);
            }
            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
            return View(productVM);
        }

		[HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _hostingEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string imageLocation = Path.Combine(wwwRoot, @"images\products");
                    string extension = Path.GetExtension(file.FileName);
                    if(obj.Product.ImageUrl != null)
					{
						var oldImage = Path.Combine(wwwRoot, obj.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImage))
						{
							System.IO.File.Delete(oldImage);
						}
					}
					using (var fs = new FileStream(Path.Combine(imageLocation, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
				}
                if(obj.Product.Id == 0)
                {
					_unitOfWork.Product.Add(obj.Product);
				}
                else
                {
					_unitOfWork.Product.Update(obj.Product);
				}
				_unitOfWork.Save();
                TempData["Succes"] = "Product Opgeslagen!";
                return RedirectToAction("Index");
			}
            return View(obj);
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
		{
            var productList = _unitOfWork.Product.GetAll(includeProperties:"category,CoverType");
            return Json(new { data = productList });
		}
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Fout bij verwijderen van product" });
            }
            if(obj.ImageUrl != null)
            {
                var oldImage = Path.Combine(_hostingEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                if(System.IO.File.Exists(oldImage))
				{
					System.IO.File.Delete(oldImage);
				}
			}
			_unitOfWork.Product.Remove(obj);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Product verwijderd!" });
		}
		#endregion
	}
}
