using FluentValidation;
namespace SurveyManager.Application.SurveyAnswers.Commands.CreateSurveyAnswer;


public class CreateSurveyCommandValidator : AbstractValidator<CreateSurveyAnswerCommand>
{
    public CreateSurveyCommandValidator()
    {
    }
}