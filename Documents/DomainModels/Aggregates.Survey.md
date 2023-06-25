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
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "title",
    "description": "description",
    "creatorId": "00000000-0000-0000-0000-000000000000",
    "questions": [
        {
            "name": "question1",
            "title": "a question example",
            "type": "text",
        },
        {
            "name": "question2",
            "title": "a question example two",
            "type": "command",
        },
        {
            "name": "question3",
            "title": "a question example three",
            "type": "rating",
            "rateCount": 5,
            "rateMax": 5,
        },
        {
            "name": "question4",
            "title": "a question example four",
            "type": "tagbox",
            "choises": [
                "choise-1",
                "choise-2"
            ]
        }
        {
            "name": "question5",
            "title": "a question example five",
            "type": "radiogroup",
            "choises": [
                "choise-1",
                "choise-2"
            ]
        },
        {
            "name": "question6",
            "title": "a question example six",
            "type": "boolean",
        }
    ],
}
```
