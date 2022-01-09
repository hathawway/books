using FileExchanger.Domain.Models.Content;
using FileExchanger.Domain.Models.Dictionaries;
using FileExchanger.Domain.Models.Dictionaries.Codes;
using FileExchanger.Domain.Models.People;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FileExchanger.Domain.DB
{
    /// <summary>
    /// Контекст для работы с бд
    /// </summary>
    public class FileExchangerDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public FileExchangerDbContext(DbContextOptions<FileExchangerDbContext> options) 
            : base(options) 
        {
            //Database.EnsureCreated();
        }
        /// <summary>
        /// Пользовательские модели
        /// </summary>
        public override DbSet<User> Users { get; set; }
        /// <summary>
        /// Данные о пользователях
        /// </summary>
        public DbSet<Employee> Employees { get; private set; }

        /// <summary>
        /// Приобретение, заказ
        /// </summary>
        public DbSet<Order> Orders { get; private set; }
        /// <summary>
        /// Публикации
        /// </summary>
        public DbSet<Publication> Publications { get; private set; }
        /// <summary>
        /// Дополнительная информация по публикациям (коды, даты и тд)
        /// </summary>
        public DbSet<PublicationInfo> PublicationInfo { get; private set; }
        
        /// <summary>
        /// Библиотечно-библиографическая классификация
        /// </summary>
        public DbSet<LBC> LBCs { get; private set; }

        /// <summary>
        /// Универсальная десятичная классификация
        /// </summary>
        public DbSet<UDC> UDCs { get; private set; }

        /// <summary>
        /// Тематики
        /// </summary>
        public DbSet<Thematic> Thematics { get; private set; }

        /// <summary>
        /// Названия типов публикаций
        /// </summary>
        public DbSet<PublicationTypeName> PublicationTypeNames { get; private set; }

        /// <summary>
        /// Языки
        /// </summary>
        public DbSet<Language> Languages { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User/Employee
            modelBuilder.Entity<User>(x =>
            {
                x.HasOne(y => y.Employee)
                .WithOne()
                .HasForeignKey<User>("EmployeeGuid")
                .IsRequired(true);
                x.HasIndex("EmployeeGuid").IsUnique(true);
            });
            
            modelBuilder.Entity<Employee>(b =>
            {
                b.ToTable("Employee");
                b.Property(x => x.FirstName)
                .HasColumnName("FirstName");
                b.Property(x => x.LastName)
                .HasColumnName("LastName");
                b.Property(x => x.MiddleName)
                .HasColumnName("MiddleName");
                b.Property(x => x.AditionalInfo)
                .HasColumnName("AditionalInfo");
            });
            #endregion

            modelBuilder.Entity<User>()
                .HasMany(u => u.Cart)
                .WithMany(f => f.UsersCart)
                .UsingEntity(j => j.ToTable("Cart"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Acquired)
                .WithMany(f => f.UsersAcquired)
                .UsingEntity(j => j.ToTable("Acquired"));
        }
    }
}
