using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Storage.Api.Models;

namespace Storage.Api.Context
{
    public partial class KnowledgeTestContext : DbContext
    {
        public KnowledgeTestContext()
        {
        }

        public KnowledgeTestContext(DbContextOptions<KnowledgeTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OrdersDetail> OrdersDetails { get; set; } = null!;
        public virtual DbSet<OrdersInfo> OrdersInfos { get; set; } = null!;
        public virtual DbSet<StorageInfo> StorageInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=KnowledgeTest;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.ToTable("Orders_Details");

                entity.Property(e => e.DetailsName)
                    .HasMaxLength(50)
                    .HasColumnName("Details_Name")
                    .HasComment("子订单名称");

                entity.Property(e => e.DetailsPrice)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("Details_Price")
                    .HasComment("子订单价格");

                entity.Property(e => e.OrdersId)
                    .HasColumnName("Orders_Id")
                    .HasComment("订单表主键");
            });

            modelBuilder.Entity<OrdersInfo>(entity =>
            {
                entity.ToTable("OrdersInfo");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .HasComment("测试名字你猜");

                entity.Property(e => e.OrderName)
                    .HasMaxLength(50)
                    .HasComment("订单名字");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(6, 2)")
                    .HasComment("订单价格");
            });

            modelBuilder.Entity<StorageInfo>(entity =>
            {
                entity.ToTable("StorageInfo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
