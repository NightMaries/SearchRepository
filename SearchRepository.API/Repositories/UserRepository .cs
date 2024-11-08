using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SearchRepository.API.Entities;
using SearchRepository.API.Interfaces;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace SearchRepository.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SearchRepositoryContext _db;

        public UserRepository(SearchRepositoryContext db)
        {
            _db = db;
        }

        public User CreateUser(User user)
        {
            if(user != null)
            {
                _db.Add(user);
                try
                {
                    _db.SaveChanges();
                }
                catch(SqlTypeException ex)
                {
                    throw new SqlTypeException($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
                
            }
            return user;
        }

        public bool DeleteUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                throw new Exception($"Пользователя нет с таким айди - {id}");
            }
            else
            {
                _db.Remove(user);
            }
            _db.SaveChanges();
            return true;
        }

        public User EditUser(User user, int id)
        {
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return user;
        }

        public User FindUserById(int id)
        {
            return _db.Users.Find(id);
        }

        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }
    }
}