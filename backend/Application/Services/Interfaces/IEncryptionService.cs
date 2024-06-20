namespace Application.Services.Interfaces;

public interface IEncryptionService
{
    string GetPasswordHash(string password);
    string GetPasswordHash(string password, byte[] knownSecret);
    bool VerifyPassword(string password, string hashedPassword, byte[] knownSecret);
}
