﻿using Application.Services;
using Infrastructure.Exceptions;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

internal sealed class EncryptionService : IEncryptionService
{
    private readonly int _byteSize = 32;

    public byte[] GetPasswordHash(string password, byte[] knownSecret)
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
            DegreeOfParallelism = 16,
            MemorySize = 8192,
            Iterations = 40,
            Salt = salt,
            AssociatedData = userUuid,
            KnownSecret = knownSecret,
        };

        var hash = argon2.GetBytes(128);

        byte[] hashWithKeys = new byte[hash.Length + _byteSize * 2 + knownSecret.Length];

        int destinationIndex = 0;
        Array.Copy(salt, 0, hashWithKeys, destinationIndex, salt.Length);

        destinationIndex += salt.Length;
        Array.Copy(hash, 0, hashWithKeys, destinationIndex, hash.Length);

        destinationIndex += hash.Length;
        Array.Copy(userUuid, 0, hashWithKeys, destinationIndex, userUuid.Length);

        destinationIndex += userUuid.Length;
        Array.Copy(knownSecret, 0, hashWithKeys, destinationIndex, knownSecret.Length);

        return hashWithKeys;
    }

    public bool VerifyPassword(string password, byte[] hashedPassword, byte[] knownSecret)
    {
        byte[] salt = new byte[_byteSize];
        byte[] userUuid = new byte[_byteSize];
        byte[] hash = new byte[hashedPassword.Length - _byteSize * 2 - knownSecret.Length];

        int sourceIndex = 0;
        Array.Copy(hashedPassword, sourceIndex, salt, 0, _byteSize);

        sourceIndex = hashedPassword.Length - _byteSize;
        Array.Copy(hashedPassword, sourceIndex, knownSecret, 0, _byteSize);

        sourceIndex = hashedPassword.Length - 2 * _byteSize;
        Array.Copy(hashedPassword, sourceIndex, userUuid, 0, _byteSize);

        int length = hashedPassword.Length - 3 * _byteSize;
        Array.Copy(hashedPassword, _byteSize, hash, 0, length);

        byte[] passwordInBytes = Encoding.UTF8.GetBytes(password);

        var argon2 = new Argon2id(passwordInBytes)
        {
            DegreeOfParallelism = 16,
            MemorySize = 8192,
            Iterations = 40,
            Salt = salt,
            AssociatedData = userUuid,
            KnownSecret = knownSecret,
        };

        var computedHash = argon2.GetBytes(128);

        return hash.SequenceEqual(computedHash); ;
    }
}
