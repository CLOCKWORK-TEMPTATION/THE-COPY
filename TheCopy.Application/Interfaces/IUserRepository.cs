
using System;
using System.Threading.Tasks;
using TheCopy.Domain.Entities;

namespace TheCopy.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);
}
