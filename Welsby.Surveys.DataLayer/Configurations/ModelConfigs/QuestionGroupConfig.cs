using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Welsby.Surveys.DataLayer.Models;

namespace Welsby.Surveys.DataLayer.Configurations.ModelConfigs
{
    internal class QuestionGroupConfig : IEntityTypeConfiguration<QuestionGroup>
    {
        public void Configure(EntityTypeBuilder<QuestionGroup> entity)
        {
            entity.Metadata.FindNavigation(nameof(QuestionGroup.Questions)).SetPropertyAccessMode(PropertyAccessMode.Field);
            entity.HasQueryFilter(m => !m.SoftDelete);
        }
    }
}
