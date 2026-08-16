using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<MyUser> Users { get; set; }
    public DbSet<MyUserRole> UserRoles { get; set; }
    public DbSet<UserOTP> UserOTPs { get; set; }
    public DbSet<UserSms> UserSms { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await Database.BeginTransactionAsync(cancellationToken);
        return new EfTransaction(transaction);
    }
}