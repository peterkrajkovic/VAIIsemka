using Microsoft.EntityFrameworkCore;

namespace ServerSide.Database
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {

        }
    }
}
