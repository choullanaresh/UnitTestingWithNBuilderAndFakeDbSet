using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestingWithNBuilderAndFakeDbSet.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<Product> Products { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public virtual IDbSet<Product> Products { get; set; }

        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
    }
}