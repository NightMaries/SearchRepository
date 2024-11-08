using SearchRepository.API.Entities;
using System.Collections.Generic;

namespace SearchRepository.API.Interfaces;

public interface IUserRepository
{
    public Task<User> CreateUser(User user);
    bool DeleteUser(int id);
    User EditUser(User user, int id);
    User FindUserById(int id);

    List<User> GetUsers();
}
