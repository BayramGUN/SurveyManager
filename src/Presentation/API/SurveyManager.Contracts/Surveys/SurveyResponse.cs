namespace SurveyManager.Contracts.Surveys;

public record SurveyResponse(
    string Id,
    string HostId,
    string Description,
    string Title,
    bool IsActive,
    List<QuestionResponse> Questions
);

public record QuestionResponse(
    string Id,
    string Name,
    string Type,
    int? RateCount,
    int? RateMax,
    List<string>? Choices
);