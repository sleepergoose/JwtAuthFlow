namespace Infrastructure.EFCore;

internal static class Constants
{
    public const string DbDefaultSchema = "JwtAothFlow";

    public const string UsersTableName = "Users";
    public static readonly Dictionary<string, string> UsersTableColumnNames = new Dictionary<string, string>
    {
        { "UserName", "Name" },
        { "UserEmail", "Email" },
        { "UserPasswordHash", "PasswordHash" },
        { "Role", "Role" }
    };
}
