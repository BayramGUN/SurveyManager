using Mapster;
using SurveyManager.Application.Surveys.Commands.CreateSurvey;
using SurveyManager.Application.Surveys.Common;
using SurveyManager.Application.Surveys.Queries;
using SurveyManager.Contracts.Surveys;
using SurveyManager.Domain.SurveyAggregate;

using Question = SurveyManager.Domain.SurveyAggregate.Entities.Question;

namespace SurveyManager.Api.Common.Mapping;

public class SurveyMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateSurveyRequest Request, Guid HostId), CreateSurveyCommand>()
            .Map(dest => dest.Questions, src => src.Request.Elements)
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<SurveyResult, SurveyResponse>()
            .Map(dest => dest.Id, src => src.Survey.Id.Value.ToString())
            .Map(dest => dest.HostId, src => src.Survey.HostId.Value.ToString())
            .Map(dest => dest.Description, src => src.Survey.Description)
            .Map(dest => dest.Title, src => src.Survey.Title)
            .Map(dest => dest.Questions, src => src.Survey.Questions);
        config.NewConfig<Survey, SurveyResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.HostId, src => src.HostId.Value.ToString());
       
        config.NewConfig<Question, QuestionResponse>()
            .Map(dest => dest.Type, src => src.Type);
    }
}

