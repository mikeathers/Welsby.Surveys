using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Welsby.Surveys.DataLayer.Models;

namespace Welsby.Surveys.DataLayer.Configurations.ModelConfigs
{
    internal class SurveyConfig : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> entity)
        {
            
            entity.Metadata.FindNavigation(nameof(Survey.QuestionGroups)).SetPropertyAccessMode(PropertyAccessMode.Field);
            entity.HasQueryFilter(m => !m.SoftDelete);
        }
    }
}
