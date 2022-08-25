using WebApplication_SampleApp.Bussines;
using WebApplication_SampleApp.Core;
using WebApplication_SampleApp.Models;
using WebApplication_SampleApp.Models.Context;
using WebApplication_SampleApp.Models.Entities;

public class EBulletinService
{
    private DatabaseContext _db = new DatabaseContext();
    public ServiceResult<object> Create(string email)
    {
        ServiceResult<object> result = new ServiceResult<object>();

        if (_db.EBulletins.Any(x => x.EmailAddress.ToLower() == email))
        {
            result.AddError($"'{email}' address is already registered.");
            return result;

        }        
        EBulletin eBulletin = new EBulletin
        {
            EmailAddress = email,
            Banned = false,
            CreatedAt = DateTime.Now

        };
        _db.EBulletins.Add(eBulletin);
        if (_db.SaveChanges() == 0)
            result.AddError("Failed to register.");      
        else
            result.Data = eBulletin;
        return result;
    }
}