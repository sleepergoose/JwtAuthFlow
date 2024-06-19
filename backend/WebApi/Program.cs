using Application;
using Application.Commands;
using Application.Services;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Domain.Constants;
using WebApi.DTO;
using Shared.Commands;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddShared();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddCqrs();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapPost("/users", async ([FromBody] CreateUserDto dto, IEncryptionService _service, ICommandDispatcher _dispatcher) =>
        {
            var passwordHash = _service.GetPasswordHash(dto.Password);

            var command = new CreateUserCommand
            (
                Id: Guid.NewGuid(),
                Name: dto.Name,
                Email: dto.Email,
                PasswordHash: passwordHash,
                Role: Role.User
            );

            await _dispatcher.DispatchCommand(command);

            return Results.Ok(command.Id);
        })
        .WithName("CreateUser")
        .WithOpenApi();

        app.Run();
    }
}
