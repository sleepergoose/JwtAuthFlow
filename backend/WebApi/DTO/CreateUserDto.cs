namespace WebApi.DTO;

public sealed record class CreateUserDto(
    string Name,
    string Email,
    string Password
);
