# Survey Manager API

- [Survey Manager API](#survey-manager-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

example:

```json
{
    "firstname": "first-name",
    "lastname": "last-name",
    "email": "email@example.com",
    "password": "password!"
}
```

[Go HTTP file](../../Requests/Authentication/Register.http)

#### Register Response

```js
201 CREATED
```

example:

```json
{
    "id": "28f43a72-c730-4032-b753-0c3c0e6de586",
    "firstname": "first-name",
    "lastname": "last-name",
    "email": "email@example.com",
    "token": "ey...ads"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

example:

```json
{
    "email": "email@example.com",
    "password": "password!"
}
```

[Go HTTP file](../../Requests/Authentication/Login.http)

#### Login Response

```js
200 OK
```

example:

```json
{
    "id": "28f43a72-c730-4032-b753-0c3c0e6de586",
    "firstname": "first-name",
    "lastname": "last-name",
    "email": "email@example.com",
    "token": "ey...ads"
}
```
