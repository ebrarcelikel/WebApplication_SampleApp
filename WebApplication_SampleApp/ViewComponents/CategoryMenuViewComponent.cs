using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication_SampleApp.Bussines;

namespace WebApplication_SampleApp.ViewComponents
{
	[ViewComponent(Name ="category-menu")]
	public class CategoryMenuViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			CategoryService categoryService = new CategoryService();		

            return View(categoryService.List().Data);
		}
	}
}
