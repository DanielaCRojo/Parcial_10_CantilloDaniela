using Microsoft.AspNetCore.SignalR;
using WebAPI.DAL.Entities;

namespace WebAPI.Domain.Interfaces
{
    public interface ICountryService
    {
        //Una de las tantas firmas de un método!
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country> GetCountryByIdAsync(Guid id);
        Task<Country> CreateCountryAsync(Country country);
        Task<Country> EditCountryAsync(Country country);
        Task<Country> DeleteCountryAsync(Guid id);
    }
}
