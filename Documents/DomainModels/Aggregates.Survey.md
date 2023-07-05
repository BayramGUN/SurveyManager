# Doamin Models

- [Survey Domain Model](#survey)

## Survey

<!-- ```csharp
public class Survey
{
    public Gu Id { get ; set; }
    public string Title { get ; set; }
    public Guid CreatorId { get ; set; }
    public User Creator { get ; set; }
    public IEnumerator<Questions> Questions { get set; }
}
``` -->

```json

{
    "title": "John's Survey",
    "description": "John has been created firs survey for the application. Please answer it once!",
    "elements": [
        {
            "name": "question1",
            "type": "text"
        },
        {
            "name": "question2",
            "type": "command"
        },
        {
            "name": "question3",
            "type": "rating",
            "rateCount": 5,
            "rateMax": 5
        },
        {
            "name": "question4",
            "type": "checkbox",
            "choices": [
                "choise-1",
                "choise-2"
            ]
        },
        {
            "name": "question7",
            "type": "checkbox",
            "choices": [
                "choise-1",
                "choise-2",
                "choise-3",
                "choise-4"
            ]
        },
        {
            "name": "question5",
            "type": "radiogroup",
            "choices": [
                "choise-3",
                "choise-4"
            ]
        },
        {
            "name": "question6",
            "type": "boolean"
        }
    ]
}
```
