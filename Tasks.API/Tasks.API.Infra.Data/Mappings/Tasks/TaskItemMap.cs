using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tasks.API.Domain.Entities.Tasks;

namespace Tasks.API.InfraData.Mappings.Tasks
{
    public class TaskItemMap : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
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
                .Property(c => c.Type)
                .HasColumnName("type")
                .HasColumnType("int")
                .IsRequired();

            builder
               .Property(c => c.Data)
               .HasColumnName("name")
               .HasColumnType("varchar(300)")
               .IsRequired();


            builder
               .Property(c => c.Data)
               .HasColumnName("data")
               .HasColumnType("varchar(300)")
               .IsRequired();
        }
    }
}
