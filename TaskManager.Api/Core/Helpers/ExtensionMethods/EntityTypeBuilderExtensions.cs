using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Core.Models;

namespace TaskManager.Api.Core.Helpers.ExtensionMethods;

public static class EntityTypeBuilderExtensions
{
    public static void BaseConfiguration<T>(this EntityTypeBuilder<T> builder) where T : BaseModel
    {
        builder.HasKey(model => model.Id);
    }
}
