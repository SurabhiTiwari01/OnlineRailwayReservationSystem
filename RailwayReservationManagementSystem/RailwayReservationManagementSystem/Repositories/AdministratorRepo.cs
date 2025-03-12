using Microsoft.EntityFrameworkCore;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;
using static RailwayReservationManagementSystem.Repositories.AdministratorRepo;

namespace RailwayReservationManagementSystem.Repositories
{
    public class AdministratorRepo : IAdministratorRepo
    {
            private readonly RailwayReservationManagementSystemContext _context;

            public AdministratorRepo(RailwayReservationManagementSystemContext context)
            {
                _context = context;
            }

            public async Task<Administrator> GetAdministratorByUsernameAsync(string username)
            {
                return await _context.Administrators
                                     .FirstOrDefaultAsync(a => a.Username == username);
            }

            public async Task AddAdministratorAsync(Administrator admin)
            {
                _context.Administrators.Add(admin);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAdministratorAsync(Administrator admin)
            {
                _context.Administrators.Update(admin);
                await _context.SaveChangesAsync();
            }

        }
    }
