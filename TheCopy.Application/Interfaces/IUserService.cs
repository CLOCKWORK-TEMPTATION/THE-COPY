using TheCopy.Shared.Models;
namespace TheCopy.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid id);
}