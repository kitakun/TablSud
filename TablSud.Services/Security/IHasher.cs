namespace TablSud.Services.Security
{
    public interface IHasher
    {
        string HashPassword(string password, byte[] salt);

        bool VerifyHashedPassword(string hashedPassword, string providedPassword);

        byte[] GenerateSalt();
    }
}
