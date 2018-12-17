using Microsoft.EntityFrameworkCore;
using OTC.Data.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTC.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// 全局定义数据连接字符串
        /// </summary>
        public static string ConStr { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseMySql("");
                optionsBuilder.UseMySql(ConStr);//读取配置文件中的链接字符串
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().ToTable("Log");
            //modelBuilder.Entity<Log>(entity =>
            //{
            //    entity.ToTable("log");

            //    entity.Property(e => e.Application).HasColumnType("varchar(50)");

            //    entity.Property(e => e.Callsite).HasColumnType("varchar(512)");

            //    entity.Property(e => e.Exception).HasColumnType("varchar(512)");

            //    entity.Property(e => e.Level).HasColumnType("varchar(50)");

            //    entity.Property(e => e.Logged).HasColumnType("datetime");

            //    entity.Property(e => e.Logger).HasColumnType("varchar(250)");

            //    entity.Property(e => e.Message).HasColumnType("varchar(512)");
            //});
        }
    }
}
