using GroupInvest.Microservices.Auditoria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GroupInvest.Microservices.Auditoria.Infra.DataAccess.Contextos
{
    public class MicroserviceAuditoriaDbContext : DbContext
    {
        private readonly string dbConnectionString;

        #region DbSets
        public DbSet<AuditoriaBase> Auditorias { get; set; }
        #endregion

        #region Construtores
        public MicroserviceAuditoriaDbContext(string connectionString)
        {
            dbConnectionString = connectionString;
        }
        #endregion

        #region Configuração
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(dbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuditoriaBase>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
        }
        #endregion
    }
}
