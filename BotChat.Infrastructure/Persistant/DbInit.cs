using BotChat.Domain;
using BotChat.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BotChat.Infrastructure.Persistant;

public class DbInit
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        if (await db.Users.AnyAsync())
            return;

        db.Users.Add(new User("Real Human Being", UserType.Human));

        await db.SaveChangesAsync();
    }
}