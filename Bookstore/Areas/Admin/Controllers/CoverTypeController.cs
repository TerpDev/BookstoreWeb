using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Areas.Admin.Controllers
{
	public class CoverTypeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
