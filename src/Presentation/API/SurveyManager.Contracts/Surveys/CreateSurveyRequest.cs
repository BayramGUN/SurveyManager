namespace SurveyManager.Contracts.Surveys;


public record CreateSurveyRequest(
    string Title,
    string Description,
    DateTime ExpiryDate,
    List<Question> Elements);

public record Question(
    string Name,
    string Type,
    int? RateCount,
    int? RateMax,
    List<string>? Choices
);
