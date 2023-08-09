using aryamoneygrow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Stockmantras.Models
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
             : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Contact> tblContact { get; set; }
        public DbSet<Coin> tbl_Coins { get; set; }
        public DbSet<News> tblNews { get; set; }
        public DbSet<Events> tblEvents { get; set; }
        public DbSet<Equities> tbl_Equities { get; set; }
        public DbSet<EquityPage> tbl_EquitiesPage { get; set; }
        public DbSet<MarketOutlook> tbl_Marketoutlook { get; set; }
        public DbSet<MutualFund> tbl_Mutual_Funds { get; set; }
        public DbSet<Nifty50Stocks> tbl_Nifty_50 { get; set; }
        public DbSet<Insurance> tbl_Insurance { get; set; }
        public DbSet<Portfolio> tbl_Portfolio { get; set; }
        public DbSet<DemateAccount> tbl_Demate_Request { get; set; }
        public DbSet<Stockmantras.Models.SignUpUser> SignUpUser { get; set; }
    }
}
