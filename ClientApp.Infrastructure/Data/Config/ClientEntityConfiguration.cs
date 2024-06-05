using ClientApp.Core.Aggregates.Client.Entities;

namespace ClientApp.Infrastructure.Data.Config;

public class ClientEntityConfiguration : BaseEntityConfiguration<ClientEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.ToTable("Clients");

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(x => x.Address)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.PostCode)
            .HasMaxLength(8)
            .IsRequired();
    }
}
