using TheCopy.Shared.DataTransferObjects;
namespace TheCopy.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid id);
}