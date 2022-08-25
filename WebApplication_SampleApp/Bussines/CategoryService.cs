using WebApplication_SampleApp.Core;
using WebApplication_SampleApp.Models;
using WebApplication_SampleApp.Models.Context;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Bussines
{
    public class CategoryService
    {
        private DatabaseContext _db = new DatabaseContext();
        private HttpContext _httpContext;
        public CategoryService(HttpContext httpContext)
        {
            _httpContext = httpContext;

        }

        public CategoryService()
        {
        }

        public ServiceResult<List<Category>> List()
        {
            List<Category> categories = _db.Categories.ToList();
            ServiceResult<List<Category>> result = new ServiceResult<List<Category>>();
            result.Data = categories;
            return result;
        }

        public ServiceResult<Category> Create(CategoryViewModel model, HttpContext httpContext)
        {
            ServiceResult<Category> result = new ServiceResult<Category>();
            model.Name = model.Name.Trim();

            if (_db.Categories.Any(x => x.Name.ToLower() == model.Name.ToLower()))
            {
                result.AddError($"'{model.Name}' category is already in data.");
                return result;

            }

            Category category = new Category
            {
                Name = model.Name,
                Explanation = model.Explanation,
                CreatedUser = httpContext.Session.GetString(Constants.Username),
                CreatedAt = DateTime.Now

            };
            _db.Categories.Add(category);
            if (_db.SaveChanges() == 0)
                result.AddError("Failed to register.");
            else
                result.Data = category;

            return result;
        }

        public ServiceResult<Category> Find(int id)
        {
            ServiceResult<Category> result = new ServiceResult<Category>()
            {
                Data = _db.Categories.Find(id)
            };
            if (result.Data == null)
                result.AddError("The category is not found");
            


            return result;
        }

        public ServiceResult<Category> Update(int id, CategoryViewModel model, HttpContext httpContext)
        {
            ServiceResult<Category> result = new ServiceResult<Category>();
            model.Name = model.Name.Trim();

            

            if (_db.Categories.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.ID != id))
            {
                result.AddError($"'{model.Name}' category name is already registered.");
                return result;
            }
            Category category = _db.Categories.Find(id);

            category.Name = model.Name;
            category.Explanation = model.Explanation;

            category.ModifiedUser = httpContext.Session.GetString(Constants.Username);
            category.ModifiedAt = DateTime.Now;

            if (_db.SaveChanges() == 0)
                result.AddError("Failed to edit.");
            else
                result.Data = category;

            return result;
        }

        public ServiceResult<object> Remove(int id)
        {
            ServiceResult<object> result = new ServiceResult<object>();
            Category category = _db.Categories.Find(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                if (_db.SaveChanges() == 0)
                    result.AddError("Failed to delete.");


            }


            return result;
        }
    }
    
}