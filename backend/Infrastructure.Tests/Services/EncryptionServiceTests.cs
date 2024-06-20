using Infrastructure.Services;
using System.Security.Cryptography;
using Shouldly;
using Infrastructure.Exceptions;
using NSubstitute;
using Microsoft.Extensions.Configuration;
using Application.Services.Interfaces;

namespace Infrastructure.Tests.Services;

public class EncryptionServiceTests
{
    private const string Password = "TeSt$PaSsWoRd123";

    private readonly byte[] _knownSecret;
    private readonly IEncryptionService _encryptionService;

    public EncryptionServiceTests()
    {

        _encryptionService = new EncryptionService(Substitute.For<IConfiguration>());
        _knownSecret = RandomNumberGenerator.GetBytes(32);
    }

    [Fact]
    public void GetPasswordHash_Throws_EmptyOrNullValueException_When_Password_Is_Empty()
    {
        var exception = Record.Exception(() => _encryptionService.GetPasswordHash(string.Empty, _knownSecret));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyOrNullValueException>();
    }

    [Fact]
    public void GetPasswordHash_Throws_InvalidValueLengthException_When_KnownSecret_Has_Invalid_Length()
    {
        var exception = Record.Exception(() => _encryptionService.GetPasswordHash(Password, [0b1]));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidValueLengthException>();
    }

    [Fact]
    public void VerifyPassword_Returns_True()
    {
        var passwordHash = _encryptionService.GetPasswordHash(Password, _knownSecret);
        var passwordValidation = _encryptionService.VerifyPassword(Password, passwordHash, _knownSecret);

        passwordValidation.ShouldBeTrue();
    }
}