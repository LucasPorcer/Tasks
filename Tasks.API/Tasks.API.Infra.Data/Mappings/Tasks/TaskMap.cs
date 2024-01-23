using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tasks.API.Domain.Entities.Tasks;

namespace Tasks.API.InfraData.Mappings.Tasks
{
    public class TaskMap : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();

            builder
                .Property(c => c.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder
               .Property(c => c.Name)
               .HasColumnName("name")
               .HasColumnType("varchar(300)")
               .IsRequired();


            builder
               .Property(c => c.Done)
               .HasColumnName("done")
               .HasColumnType("boolean")
               .HasDefaultValue(false)
               .IsRequired();

            builder
                .Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime");

            builder
            .Property(c => c.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime")
            .IsRequired(false);

            builder
              .Property(c => c.EndedAt)
              .HasColumnName("ended_at")
              .HasColumnType("datetime")
              .IsRequired(false);
        }
    }
}
