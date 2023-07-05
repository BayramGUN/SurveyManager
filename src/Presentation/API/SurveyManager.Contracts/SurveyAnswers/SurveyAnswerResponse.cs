namespace SurveyManager.Contracts.SurveyAnswers;

public record SurveyAnswerResponse(
    string Id,
    string SurveyId,
    List<AnswerResponse> Answers
);

public record AnswerResponse(
    string QuestionName,
    string Type,
    List<string>? Answer
);