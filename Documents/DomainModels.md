# Domain Models

- [User Domain Model](#user)
- [Survey Domain Model](#survey)

## User

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstname": "first-name",
    "lastname": "last-name",
    "email": "email@example.com",
    "password": "password!"
}
```

> Note: Password must be laying around like hashed. ⚠️

## Survey

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "title": "title",
    "creator": "someone else",
    "questions": [
        {
            "question": "a question example",
            "answerType": "input-text",
        },
        {
            "question": "a question example two",
            "answerType": "text-area",
        }
    ],
}
```
