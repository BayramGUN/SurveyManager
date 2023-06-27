namespace SurveyManager.Contracts.Surveys;

public record SurveyResponse(
    string Id,
    string HostId,
    string Description,
    string Title,
    List<QuestionResponse> Questions
);

public record QuestionResponse(
    string Name,
    string Title,
    string Type,
    int? RateCount,
    int? RateMax,
    List<string>? Choices
);