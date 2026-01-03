using Microsoft.EntityFrameworkCore;
using RhPortal.Api.Domain.Entities;
using RhPortal.Api.Infrastructure.Tenancy;
using RHPortal.Api.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RhPortal.Api.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    private readonly ITenantContext _tenantContext;

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<JobPosition> JobPositions => Set<JobPosition>();
    public DbSet<Manager> Managers => Set<Manager>();
    public DbSet<Vaga> Vagas => Set<Vaga>();
    public DbSet<VagaBeneficio> VagaBeneficios => Set<VagaBeneficio>();
    public DbSet<VagaRequisito> VagaRequisitos => Set<VagaRequisito>();
    public DbSet<VagaEtapa> VagaEtapas => Set<VagaEtapa>();
    public DbSet<VagaPergunta> VagaPerguntas => Set<VagaPergunta>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(b =>
        {
            b.ToTable("Areas");
            b.HasKey(x => x.Id);

            b.Property(x => x.TenantId).HasMaxLength(64).IsRequired();
            b.Property(x => x.Code).HasMaxLength(40).IsRequired();
            b.Property(x => x.Name).HasMaxLength(120).IsRequired();

            b.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();
            b.HasQueryFilter(x => x.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<Department>(b =>
        {
            b.ToTable("Departments");
            b.HasKey(x => x.Id);

            b.Property(x => x.TenantId).HasMaxLength(64).IsRequired();
            b.Property(x => x.Code).HasMaxLength(40).IsRequired();
            b.Property(x => x.Name).HasMaxLength(120).IsRequired();

            b.Property(x => x.ManagerName).HasMaxLength(120);
            b.Property(x => x.ManagerEmail).HasMaxLength(180);
            b.Property(x => x.Phone).HasMaxLength(40);
            b.Property(x => x.CostCenter).HasMaxLength(60);
            b.Property(x => x.BranchOrLocation).HasMaxLength(80);
            b.Property(x => x.Description).HasMaxLength(1000);

            b.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();
            b.HasQueryFilter(x => x.TenantId == _tenantContext.TenantId);

            b.HasOne(x => x.Area)
             .WithMany()
             .HasForeignKey(x => x.AreaId)
             .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Unit>(b =>
        {
            b.ToTable("Units");
            b.HasKey(x => x.Id);

            b.Property(x => x.TenantId).HasMaxLength(64).IsRequired();

            b.Property(x => x.Code).HasMaxLength(40).IsRequired();
            b.Property(x => x.Name).HasMaxLength(140).IsRequired();

            b.Property(x => x.City).HasMaxLength(120);
            b.Property(x => x.Uf).HasMaxLength(2);

            b.Property(x => x.AddressLine).HasMaxLength(220);
            b.Property(x => x.Neighborhood).HasMaxLength(120);
            b.Property(x => x.ZipCode).HasMaxLength(12);

            b.Property(x => x.Email).HasMaxLength(180);
            b.Property(x => x.Phone).HasMaxLength(40);

            b.Property(x => x.ResponsibleName).HasMaxLength(140);
            b.Property(x => x.Type).HasMaxLength(120);

            b.Property(x => x.Notes).HasMaxLength(1000);

            b.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();
            b.HasQueryFilter(x => x.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<JobPosition > (b =>
        {
            b.ToTable("JobPositions");
            b.HasKey(x => x.Id);

            b.Property(x => x.TenantId).HasMaxLength(64).IsRequired();

            b.Property(x => x.Code).HasMaxLength(40).IsRequired();
            b.Property(x => x.Name).HasMaxLength(160).IsRequired();

            b.Property(x => x.Type).HasMaxLength(180);
            b.Property(x => x.Description).HasMaxLength(1000);

            b.HasOne(x => x.Area)
                .WithMany()
                .HasForeignKey(x => x.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();

            b.HasQueryFilter(x => x.TenantId == _tenantContext.TenantId);
        });

        modelBuilder.Entity<Manager>(b =>
        {
            b.ToTable("Managers");
            b.HasKey(x => x.Id);

            b.Property(x => x.TenantId).HasMaxLength(64).IsRequired();

            b.Property(x => x.Name).HasMaxLength(160).IsRequired();
            b.Property(x => x.Email).HasMaxLength(180).IsRequired();
            b.Property(x => x.Phone).HasMaxLength(40);
            b.Property(x => x.Notes).HasMaxLength(1000);
            b.Property(x => x.Headcount);

            b.HasOne(x => x.Unit)
                .WithMany()
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Area)
                .WithMany()
                .HasForeignKey(x => x.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.JobPosition)
                .WithMany()
                .HasForeignKey(x => x.JobPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Evitar duplicar gestor por email no tenant
            b.HasIndex(x => new { x.TenantId, x.Email }).IsUnique();

            b.HasQueryFilter(x => x.TenantId == _tenantContext.TenantId);
        });

        // VAGAS
        modelBuilder.Entity<Vaga>(b =>
        {
            b.ToTable("Vagas");
            b.HasKey(x => x.Id);

            b.Property(x => x.TenantId).HasMaxLength(64).IsRequired();

            b.Property(x => x.Codigo).HasMaxLength(40);
            b.Property(x => x.Titulo).HasMaxLength(160).IsRequired();

            b.Property(x => x.CodigoInterno).HasMaxLength(40);
            b.Property(x => x.CodigoCbo).HasMaxLength(20);

            b.Property(x => x.GestorRequisitante).HasMaxLength(120);
            b.Property(x => x.RecrutadorResponsavel).HasMaxLength(120);
            b.Property(x => x.PublicoAfirmativo).HasMaxLength(120);

            b.Property(x => x.ProjetoNome).HasMaxLength(160);
            b.Property(x => x.ProjetoClienteAreaImpactada).HasMaxLength(160);
            b.Property(x => x.ProjetoPrazoPrevisto).HasMaxLength(80);

            b.Property(x => x.Cep).HasMaxLength(12);
            b.Property(x => x.Logradouro).HasMaxLength(160);
            b.Property(x => x.Numero).HasMaxLength(20);
            b.Property(x => x.Bairro).HasMaxLength(120);
            b.Property(x => x.Cidade).HasMaxLength(120);
            b.Property(x => x.Uf).HasMaxLength(2);

            b.Property(x => x.PoliticaTrabalho).HasMaxLength(200);
            b.Property(x => x.ObservacoesDeslocamento).HasMaxLength(200);
            b.Property(x => x.ObservacoesRemuneracao).HasMaxLength(240);

            // Decimais
            b.Property(x => x.SalarioMinimo).HasPrecision(18, 2);
            b.Property(x => x.SalarioMaximo).HasPrecision(18, 2);

            // ✅ AREA (FK + Navegação)
            // Se você quer obrigar AreaId, deixe IsRequired()
            b.Property(x => x.AreaId).IsRequired();

            b.Property(x => x.DepartmentId).IsRequired();

            b.HasOne(x => x.Area)
                .WithMany() // ou .WithMany(a => a.Vagas) se você tiver coleção em Area
                .HasForeignKey(x => x.AreaId)
                .OnDelete(DeleteBehavior.Restrict); // ou NoAction se preferir

            b.HasOne(x => x.Department)
                 .WithMany()
                 .HasForeignKey(x => x.DepartmentId)
                 .OnDelete(DeleteBehavior.Restrict);

            // Relacionamentos (listas do modal)
            b.HasMany(x => x.Beneficios)
                .WithOne(x => x.Vaga)
                .HasForeignKey(x => x.VagaId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Requisitos)
                .WithOne(x => x.Vaga)
                .HasForeignKey(x => x.VagaId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Etapas)
                .WithOne(x => x.Vaga)
                .HasForeignKey(x => x.VagaId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.PerguntasTriagem)
                .WithOne(x => x.Vaga)
                .HasForeignKey(x => x.VagaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices úteis
            b.HasIndex(x => new { x.TenantId, x.Status });
            b.HasIndex(x => new { x.TenantId, x.DepartmentId });

            // ✅ antes era x.Area (enum) -> agora é AreaId (Guid)
            b.HasIndex(x => new { x.TenantId, x.AreaId });

            // Multi-tenant
            b.HasQueryFilter(x => x.TenantId == _tenantContext.TenantId);
        });


    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is ITenantEntity tenantEntity)
            {
                if (entry.State == EntityState.Added)
                    tenantEntity.TenantId = _tenantContext.TenantId;
            }

            if (entry.Entity is Department dep)
            {
                if (entry.State == EntityState.Added)
                    dep.CreatedAtUtc = now;

                if (entry.State is EntityState.Added or EntityState.Modified)
                    dep.UpdatedAtUtc = now;
            }

            if (entry.Entity is Unit unit)
            {
                if (entry.State == EntityState.Added)
                    unit.CreatedAtUtc = now;

                if (entry.State is EntityState.Added or EntityState.Modified)
                    unit.UpdatedAtUtc = now;
            }

            if (entry.Entity is JobPosition jp)
            {
                if (entry.State == EntityState.Added)
                    jp.CreatedAtUtc = now;

                if (entry.State is EntityState.Added or EntityState.Modified)
                    jp.UpdatedAtUtc = now;
            }

            if (entry.Entity is Manager m)
            {
                if (entry.State == EntityState.Added)
                    m.CreatedAtUtc = now;

                if (entry.State is EntityState.Added or EntityState.Modified)
                    m.UpdatedAtUtc = now;
            }

            if (entry.Entity is Vaga v)
            {
                if (entry.State == EntityState.Added) v.CreatedAtUtc = now;
                if (entry.State is EntityState.Added or EntityState.Modified) v.UpdatedAtUtc = now;
            }

            if (entry.Entity is VagaBeneficio vb)
            {
                if (entry.State == EntityState.Added) vb.CreatedAtUtc = now;
                if (entry.State is EntityState.Added or EntityState.Modified) vb.UpdatedAtUtc = now;
            }

            if (entry.Entity is VagaRequisito vr)
            {
                if (entry.State == EntityState.Added) vr.CreatedAtUtc = now;
                if (entry.State is EntityState.Added or EntityState.Modified) vr.UpdatedAtUtc = now;
            }

            if (entry.Entity is VagaEtapa ve)
            {
                if (entry.State == EntityState.Added) ve.CreatedAtUtc = now;
                if (entry.State is EntityState.Added or EntityState.Modified) ve.UpdatedAtUtc = now;
            }

            if (entry.Entity is VagaPergunta vp)
            {
                if (entry.State == EntityState.Added) vp.CreatedAtUtc = now;
                if (entry.State is EntityState.Added or EntityState.Modified) vp.UpdatedAtUtc = now;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
