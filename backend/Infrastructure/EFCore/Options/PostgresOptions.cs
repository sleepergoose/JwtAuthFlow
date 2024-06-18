namespace Infrastructure.EFCore.Options;
internal sealed record PostgresOptions
{
    public string ConnectionString { get; set; } = string.Empty;
}
