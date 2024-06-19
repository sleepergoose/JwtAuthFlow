using Application.Services;
using Infrastructure.Exceptions;
using Infrastructure.Options;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Shared;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public sealed class EncryptionService(IConfiguration configuration) : IEncryptionService
{
    private readonly int _byteSize = 32;
    private readonly int _hashSize = 128;

    private const int DegreeOfParallelism = 16;
    private const int MemorySize = 8192;
    private const int Iterations = 40;

    private readonly IConfiguration _configuration = configuration;

    public string GetPasswordHash(string password)
    {
        var argonOptions = _configuration.GetOptions<ArgonOptions>("Argon2")
            ?? throw new OptionsNotFoundException("Argon2", typeof(ArgonOptions));

        byte[] knownSecret =  Convert.FromBase64String(argonOptions.KnownSecret);

        return GetPasswordHash(password, knownSecret);
    }

    public string GetPasswordHash(string password, byte[] knownSecret)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new EmptyOrNullValueException(nameof(password), password.GetType());
        }

        if (knownSecret.Length != _byteSize)
        {
            throw new InvalidValueLengthException(nameof(knownSecret), knownSecret.GetType(), _byteSize);
        }

        byte[] passwordInBytes = Encoding.UTF8.GetBytes(password);
        byte[] salt = RandomNumberGenerator.GetBytes(_byteSize);
        byte[] userUuid = RandomNumberGenerator.GetBytes(_byteSize);

        var argon2 = new Argon2id(passwordInBytes)
        {
            DegreeOfParallelism = EncryptionService.DegreeOfParallelism,
            MemorySize = EncryptionService.MemorySize,
            Iterations = EncryptionService.Iterations,
            Salt = salt,
            AssociatedData = userUuid,
            KnownSecret = knownSecret,
        };

        var hash = argon2.GetBytes(_hashSize);

        byte[] hashWithKeys = new byte[hash.Length + _byteSize * 2 + knownSecret.Length];

        int destinationIndex = 0;
        Array.Copy(salt, 0, hashWithKeys, destinationIndex, salt.Length);

        destinationIndex += salt.Length;
        Array.Copy(hash, 0, hashWithKeys, destinationIndex, hash.Length);

        destinationIndex += hash.Length;
        Array.Copy(userUuid, 0, hashWithKeys, destinationIndex, userUuid.Length);

        destinationIndex += userUuid.Length;
        Array.Copy(knownSecret, 0, hashWithKeys, destinationIndex, knownSecret.Length);

        return Convert.ToBase64String(hashWithKeys);
    }

    public bool VerifyPassword(string password, string hashedPassword, byte[] knownSecret)
    {
        byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[_byteSize];
        byte[] userUuid = new byte[_byteSize];
        byte[] hash = new byte[_hashSize];

        int sourceIndex = 0;
        Array.Copy(hashedPasswordBytes, sourceIndex, salt, 0, _byteSize);

        sourceIndex = hashedPasswordBytes.Length - _byteSize;
        Array.Copy(hashedPasswordBytes, sourceIndex, knownSecret, 0, _byteSize);

        sourceIndex = hashedPasswordBytes.Length - 2 * _byteSize;
        Array.Copy(hashedPasswordBytes, sourceIndex, userUuid, 0, _byteSize);

        Array.Copy(hashedPasswordBytes, _byteSize, hash, 0, _hashSize);

        byte[] passwordInBytes = Encoding.UTF8.GetBytes(password);

        var argon2 = new Argon2id(passwordInBytes)
        {
            DegreeOfParallelism = EncryptionService.DegreeOfParallelism,
            MemorySize = EncryptionService.MemorySize,
            Iterations = EncryptionService.Iterations,
            Salt = salt,
            AssociatedData = userUuid,
            KnownSecret = knownSecret,
        };

        var computedHash = argon2.GetBytes(_hashSize);

        return hash.SequenceEqual(computedHash);
    }
}
