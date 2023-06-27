# Doamin Models

- [Answer Domain Model](#answer)

## Answer
<!-- 
- Class looks in C#

```csharp
public class Answer
{
    public Guid Id { get ; set; }
    public string Title { get ; set; }
    public Guid CreatorId { get ; set; }
    public User Creator { get ; set; }
    public IEnumerator<Questions> Questions { get set; }
}
``` -->

- Class looks in JSON:

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "title",
    "description": "description",
    "ownerId": "00000000-0000-0000-0000-000000000000",
    "questions": [
        {
            "question": "a question example",
            "answers": [
                "answer-1",
                "answer-2"
            ]
        },
        {
            "question": "a question example two",
            "answers": [
                "answer-1",
                "answer-2"
            ]
        },
        {
            "question": "a question example three",
            "mostPopularAnswer": "answer-1"
        },
        {
            "question": "a question example four",
            "rateOfAnswers": 3.8
        }
    ],
}
```
