namespace Application.Services;

public interface IEncryptionService
{
    byte[] GetPasswordHash(string password, byte[] knownSecret);
    bool VerifyPassword(string password, byte[] hashedPassword, byte[] knownSecret);
}
