using CryptoSimulator.DataAccess.Data;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using CryptoSimulator.DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoSimulator.DataAccess.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(CryptoSimulatorDbContext context) : base(context)
        {
        }

        public User GetById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }

        public int Insert(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }
    }
}
