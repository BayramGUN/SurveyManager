# Create Survey

```js
POST hosts/{hostId}/surveys
```

```json
{
    "title": "John's Survey 2",
    "description": "John has been created second survey for the application. Please answer it once!",
    "expiryDate": "2023-08-17T23:59:59",
    "elements": [
        {
            "name": "question1",
            "title": "title",
            "type": "text"
        },
        {
            "name": "question2",
            "title": "title",
            "type": "command"
        },
        {
            "name": "question3",
            "title": "title",
            "type": "rating",
            "rateCount": 5,
            "rateMax": 5
        },
        {
            "name": "question5",
            "title": "title",
            "type": "rating",
            "rateCount": 10,
            "rateMax": 10
        },
        {
            "name": "question4",
            "title": "title",
            "type": "checkbox",
            "choices": [
                "choise-1",
                "choise-2"
            ]
        },
        {
            "name": "question7",
            "title": "title",
            "type": "checkbox",
            "choices": [
                "choise-1",
                "choise-2",
                "choise-3",
                "choise-4"
            ]
        },
        {
            "name": "question8",
            "title": "title",
            "type": "radiogroup",
            "choices": [
                "choise-3",
                "choise-4"
            ]
        },
        {
            "name": "question6",
            "title": "title",
            "type": "boolean"
        }
    ]
}
```

[Go to HTTP File](../../Requests/Surveys/CreateSurvey.http)
