using Microsoft.EntityFrameworkCore;

namespace Portfolio.Api
{
    public class PortfolioContext : DbContext
    {
        //construtor
        public PortfolioContext(DbContextOptions<PortfolioContext> options)
            : base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }

        //sobrescreve a classe para configurar conexao
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            //para conectar com o servidor no caso de teste DB local no arquivo appsetings.json
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
        }
    }
}
