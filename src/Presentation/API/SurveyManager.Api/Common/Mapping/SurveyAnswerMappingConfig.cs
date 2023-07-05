using Mapster;

using SurveyManager.Application.SurveyAnswers.Commands.CreateSurveyAnswer;
using SurveyManager.Application.SurveyAnswers.Common;
using SurveyManager.Contracts.SurveyAnswers;
using SurveyManager.Domain.SurveyAnswerAggregate.Entities;
namespace SurveyManager.Api.Common.Mapping;

public class SurveyAnswerMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAnswerRequest, CreateSurveyAnswerCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest.SurveyId, src => src.SurveyId);

        config.NewConfig<SurveyAnswerResult, SurveyAnswerResponse>()
            .Map(dest => dest.Id, src => src.SurveyAnswer.Id.Value.ToString())
            .Map(dest => dest.SurveyId, src => src.SurveyAnswer.SurveyId.Value.ToString());
       
        config.NewConfig<Answer, AnswerResponse>()
            .Map(dest => dest.QuestionName, src => src.QuestionName)
            .Map(dest => dest.Type, src => src.Type)
            .Map(dest => dest.Answer, src => src.Choices);
    }
}

