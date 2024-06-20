namespace Application.Services.Interfaces;

public interface IReadUserService
{
    Task<bool> ExistsByEmailAsync(string email);
}
