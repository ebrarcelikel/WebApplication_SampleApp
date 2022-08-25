using Microsoft.AspNetCore.Mvc;

namespace WebApplication_SampleApp.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
