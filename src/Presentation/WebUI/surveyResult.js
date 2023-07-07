var url = window.location.search;
url = url.replace("?", ''); // remove the ?
var hostId = localStorage.getItem("hostId");
var jwt = localStorage.getItem("jwt");
var reqUrl = `https://localhost:7146/hosts/${hostId}/surveys/getAnswers?surveyId` + url;

fetch(reqUrl, {
    method: 'GET',
    mode: 'cors',
    headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + jwt
    }
})
.then((response) => response.json())
.then((data) => {
    generateSurveyResult(data);
})
.catch((error) => {
    console.error('Error:', error);
});

function generateSurveyResult(data) {
    var surveyResultDiv = document.getElementById("res");
    
    for (var i = 0; i < data.surveyAnswers.length; i++) {
        var survey = data.surveyAnswers[i];
        var surveyDiv = document.createElement("div");
        surveyDiv.className = "survey-container";

        var answers = survey.answers;
        for (var j = 0; j < answers.length; j++) {
            var answer = answers[j];

            var questionDiv = document.createElement("div");
            questionDiv.className = "question";

            var questionTitle = document.createElement("p");
            questionTitle.className = "question-title";
            questionTitle.innerHTML = answer.questionName;
            questionDiv.appendChild(questionTitle);

            var answerChoices = answer.choices;
            for (var k = 0; k < answerChoices.length; k++) {
                var choice = answerChoices[k];

                var answerDiv = document.createElement("div");
                answerDiv.className = "answer";

                var answerLabel = document.createElement("span");
                answerLabel.className = "answer-label";
                answerDiv.appendChild(answerLabel);

                var answerValue = document.createElement("span");
                answerValue.className = "answer-value";
                answerValue.innerHTML = choice;
                answerDiv.appendChild(answerValue);

                questionDiv.appendChild(answerDiv);
            }

            surveyDiv.appendChild(questionDiv);
        }

        surveyResultDiv.appendChild(surveyDiv);
    }
}
