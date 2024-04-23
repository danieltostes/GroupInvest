using GroupInvest.Microservices.Participantes.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Participantes.Infra.DataAccess.Contextos
{
    public class MicroserviceParticipantesDbContext : DbContext
    {
        private readonly string dbConnectionString;

        #region DbSets
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Adesao> Adesoes { get; set; }
        #endregion

        #region Construtores
        public MicroserviceParticipantesDbContext(string connectionString)
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

            modelBuilder.Entity<Participante>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Participante>()
                .Property(p => p.Ativo)
                .HasConversion(
                    v => v ? "S" : "N",
                    v => v == "S");

            modelBuilder.Entity<Participante>()
                .Property(p => p.UsuarioAplicativoId)
                .HasConversion(
                    v => v == null ? null : v.ToString(),
                    v => string.IsNullOrEmpty(v) ? default : new Guid(v));

            modelBuilder.Entity<Periodo>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Periodo>()
                .Property(p => p.Ativo)
                .HasConversion(
                    v => v ? "S" : "N",
                    v => v == "S");

            modelBuilder.Entity<Adesao>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
        }
        #endregion
    }
}
