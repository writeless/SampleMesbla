using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Writeless.Data.EntityFramework.Extensions
{
    public static class ModelBuilderExtensions
    {
        //Reference: http://stackoverflow.com/questions/37493095/entity-framework-core-rc2-table-name-pluralization
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }
        }

        public static void SetDefaultColumnsPropertiesSqlServer(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var entityConfig = modelBuilder.Entity(entityType.ClrType);

                foreach (var property in entityType.GetProperties())
                {
                    entityConfig.Property(property.Name)
                        .IsRequired();

                    if (property.ClrType == typeof(String))
                    {
                        entityConfig.Property(property.Name)
                            .HasMaxLength(256);
                    }

                    if (property.ClrType == typeof(DateTime))
                    {
                        //Maior parte das vezes usamos apenas a data, e não a hora/minuto, então por default fica date, o que melhora consultas e outras coisas 
                        entityConfig.Property(property.Name)
                        .HasColumnType("date");
                    }

                    //TODO:
                    //modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(24, 8));
                }
            }
        }
    }
}
