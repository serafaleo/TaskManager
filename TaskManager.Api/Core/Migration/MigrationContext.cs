﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Features.Tarefas.Models;

namespace TaskManager.Api.Core.Migration;

public class MigrationContext : DbContext
{
    public DbSet<Tarefa> Tarefas { get; set; }

    public MigrationContext(DbContextOptions<MigrationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MigrationContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
