namespace SurveyManager.Contracts.Surveys;


public record CreateSurveyRequest(
    string Title,
    string Description,
    DateTime ExpiryDate,
    List<Question> Elements);

public record Question(
    string Name,
    string Type,
    string Title,
    int? RateCount,
    int? RateMax,
    List<Choice>? Choices
);

public record Choice(
    string Value,
    string Text
);