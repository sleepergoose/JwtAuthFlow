namespace Application.Services;

public interface IReadUserService
{
    Task<bool> ExistsByEmailAsync(string email);
}
