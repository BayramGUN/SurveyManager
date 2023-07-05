namespace SurveyManager.Contracts.SurveyAnswers;


public record CreateAnswerRequest(
    Guid SurveyId,
    Guid HostId,
    List<AnswerRequest> Answers);

public record AnswerRequest(
    string QuestionName,
    List<string>? Answer
);
