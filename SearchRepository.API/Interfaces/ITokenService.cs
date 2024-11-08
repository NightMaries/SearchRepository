using SearchRepository.API.Entities;
using System.Collections.Generic;

namespace SearchRepository.API.Interfaces;

public interface ITokenService
{
    string CreateToken(string login);
}