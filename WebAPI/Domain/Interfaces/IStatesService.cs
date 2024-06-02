using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.Domain.Interfaces;

public interface IStatesService
{
    Task<IEnumerable<State>> GetStateByCountryIdAsync(Guid countryId);

    Task<State> GetStateByName(string name);

    Task<State> CreateState(Guid countryId, string name);

    Task<State> EditState(Guid id);

    Task<State> DeleteTaskAsync(Guid id);
}
