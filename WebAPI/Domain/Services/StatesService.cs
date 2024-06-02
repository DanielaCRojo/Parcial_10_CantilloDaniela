using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using WebAPI.DAL;
using WebAPI.DAL.Entities;
using WebAPI.Domain.Interfaces;
using System.Linq;
namespace WebAPI.Domain.Services
{
    public class StatesService : IStatesService

    {
        private readonly DataBaseContext _context;

        public StatesService(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<State> CreateState(Guid countryId, string name)
        {
            var country = await _context.Countries.FindAsync(countryId);
            if (country == null)
            {
                throw new ArgumentException("El país especificado no existe.");
            }

            var state = new State
            {
                Name = name,
                CreatedDate = DateTime.UtcNow,
                CountryId = countryId
            };
            _context.States.Add(state);
            await _context.SaveChangesAsync();
            return state;
        }


        public async Task<State> DeleteTaskAsync(Guid id)
        {
            try
            {
                var state = await _context.States.FirstOrDefaultAsync(s => s.Id == id);

                if (state == null)
                {
                    return null;
                }

                _context.States.Remove(state); 
                await _context.SaveChangesAsync();

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> EditState(Guid id)
        {
            try
            {
                var state = await _context.States.FindAsync(id);
                if (state == null)
                {
                    throw new ArgumentException("El Estado especificado no existe.");
                }
                state.ModifiedDate = DateTime.Now;
                _context.States.Update(state); //Virtualizo mi objeto
                await _context.SaveChangesAsync();

                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> GetStateByName(string name)
        {
            try
            {
                var state = await _context.States.FirstOrDefaultAsync(state => state.Name == name);
                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ??
                    dbUpdateException.Message);
            }
        }

        public async Task<IEnumerable<State>> GetStateByCountryIdAsync(Guid countryId)
        {
            try
            {
                var States = await _context.States.Where(state => state.CountryId == countryId).ToListAsync();
                return States;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ??
                    dbUpdateException.Message);
            }
        }
    }
}
