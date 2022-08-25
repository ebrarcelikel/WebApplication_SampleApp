using Microsoft.EntityFrameworkCore;
using WebApplication_SampleApp.Core;
using WebApplication_SampleApp.Models;
using WebApplication_SampleApp.Models.Context;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Bussines
{
    public class ServiceService
    {
        private DatabaseContext _db = new DatabaseContext();
        private HttpContext _httpContext;
        public ServiceService(HttpContext httpContext)
        {
            _httpContext = httpContext;

        }
        public ServiceResult<Service> Create(ServiceViewModel model, HttpContext httpContext)
        {
            ServiceResult<Service> result = new ServiceResult<Service>();

            Service service = new Service
            {
                Name = model.Name,
                Explanation = model.Explanation,
                Price = model.Price,
                CategoryID = model.CategoryID,
                OwnerID = httpContext.Session.GetInt32(Constants.UserId).Value,
                CreatedUser = httpContext.Session.GetString(Constants.Username),
                CreatedAt = DateTime.Now

            };
            _db.Services.Add(service);
            if (_db.SaveChanges() == 0)
                result.AddError("Failed to add service.");
            else
                result.Data = service;

            return result;
        }
        public ServiceResult<List<Service>> List(int userID)
        {
            IQueryable<Service> services;


            services = _db.Services.
                Include(X => X.Orders).
                Include(X => X.Category).
                AsQueryable();
            if (userID != null)
            {
                services = services.Where(n => n.OwnerID == userID);
            }
            ServiceResult<List<Service>> result = new ServiceResult<List<Service>>();
            result.Data = services.ToList();
            return result;
        }

        public ServiceResult<Service> Find(int id)
        {
            ServiceResult<Service> result = new ServiceResult<Service>()
            {
                Data = _db.Services.Include(m => m.Orders).
                Include(m => m.Category).
                SingleOrDefault(m => m.ID == id)
            };
            if (result.Data == null)
                result.AddError("The service is not found");



            return result;
        }

        public ServiceResult<Service> Update(int id, ServiceViewModel model, HttpContext httpContext)
        {
            ServiceResult<Service> result = new ServiceResult<Service>();
           
            Service service = _db.Services.Find(id);

            service.Name = model.Name;
            service.Explanation = model.Explanation;
            service.Price = model.Price ;
            service.CategoryID = model.CategoryID;
            service.ModifiedUser = httpContext.Session.GetString(Constants.Username);
            service.ModifiedAt = DateTime.Now;

            if (_db.SaveChanges() == 0)
                result.AddError("Failed to edit.");
            else
                result.Data = service;

            return result;
        }
        public ServiceResult<object> Remove(int id)
        {
            ServiceResult<object> result = new ServiceResult<object>();
            Service service = _db.Services.Find(id);
            if (service != null)
            {
                _db.Services.Remove(service);
                if (_db.SaveChanges() == 0)
                    result.AddError("Failed to delete.");
            }
            return result;
        }
    }

}





