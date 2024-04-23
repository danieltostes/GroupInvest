using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Contextos
{
    public class MicroserviceMensalidadesDbContext : DbContext
    {
        private readonly string dbConnectionString;

        #region DbSets
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Adesao> Adesoes { get; set; }
        public DbSet<Mensalidade> Mensalidades { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<PrevisaoPagamentoEmprestimo> PrevisoesPagamentoEmprestimo { get; set; }
        public DbSet<PagamentoParcialEmprestimo> PagamentosEmprestimos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ItemPagamento> ItensPagamento { get; set; }
        public DbSet<DistribuicaoCotas> DistribuicaoCotas { get; set; }
        public DbSet<DistribuicaoParticipante> DistribuicaoParticipante { get; set; }
        #endregion

        #region Construtores
        public MicroserviceMensalidadesDbContext(string connectionString)
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

            // Periodo
            modelBuilder.Entity<Periodo>()
                .Property(p => p.Ativo)
                .HasConversion(
                    v => v ? "S" : "N",
                    v => v == "S");

            // Adesão
            modelBuilder.Entity<Adesao>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            // Mensalidade
            modelBuilder.Entity<Mensalidade>()
                .Property(m => m.Id)
                .ValueGeneratedOnAdd();

            // Emprestimo
            modelBuilder.Entity<Emprestimo>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Emprestimo>()
                .HasMany(e => e.PagamentosParciais)
                .WithOne(p => p.Emprestimo);

            modelBuilder.Entity<Emprestimo>()
                .Property(e => e.Quitado)
                .HasConversion(
                    v => v ? "S" : "N",
                    v => v == "S");

            // Previsão de Pagamento de Empréstimo
            modelBuilder.Entity<PrevisaoPagamentoEmprestimo>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PrevisaoPagamentoEmprestimo>()
                .Property(p => p.Realizada)
                .HasConversion(
                    v => v ? "S" : "N",
                    v => v == "S");

            modelBuilder.Entity<PrevisaoPagamentoEmprestimo>()
                .Property(p => p.Consolidada)
                .HasConversion(
                    v => v ? "S" : "N",
                    v => v == "S");

            // Pagamento Parcial Emprestimo
            modelBuilder.Entity<PagamentoParcialEmprestimo>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            // Pagamento
            modelBuilder.Entity<Pagamento>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Pagamento>()
                .HasMany(p => p.ItensPagamento)
                .WithOne(i => i.Pagamento);

            // Item Pagamento
            modelBuilder.Entity<ItemPagamento>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            // Distribuição de cotas
            modelBuilder.Entity<DistribuicaoCotas>()
                .HasMany(d => d.DistribuicaoParticipantes)
                .WithOne(dp => dp.DistribuicaoCotas);
        }
        #endregion
    }
}
