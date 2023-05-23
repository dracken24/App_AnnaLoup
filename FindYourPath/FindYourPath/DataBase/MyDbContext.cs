using GoogleApi.Entities.Search.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FindYourPath.DataBase
{
	public class MyDbContext : DbContext
	{
		public DbSet<User> Utilisateurs { get; set; }
		// Ajoutez d'autres DbSet pour vos autres entités

		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configurez vos relations et autres configurations de modèle ici
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				string connectionString = "server=VotreServeur;database=VotreBaseDeDonnees;uid=VotreUtilisateur;password=VotreMotDePasse;";
				optionsBuilder.UseMySQL(connectionString);
			}
			base.OnConfiguring(optionsBuilder);
		}

	}
}