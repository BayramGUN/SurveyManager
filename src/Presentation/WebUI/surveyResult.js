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
    let checkboxChoices = new Map();
    let radioChoices = new Map();
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
                if (answer.type === "checkbox") {
                    if (!checkboxChoices.has(answer.questionName)) {
                        checkboxChoices.set(answer.questionName, []);
                    }
                    checkboxChoices.get(answer.questionName).push(choice);
                }

                if (answer.type === "radio") {
                    if (!radioChoices.has(answer.questionName)) {
                        radioChoices.set(answer.questionName, []);
                    }
                    radioChoices.get(answer.questionName).push(choice);
                }
                questionDiv.appendChild(answerDiv);
            }

            surveyDiv.appendChild(questionDiv);
        }

        surveyResultDiv.appendChild(surveyDiv);
    }
    const checkboxMode = calculateMode(checkboxChoices)[0];
    const radioMode = calculateMode(radioChoices)[0];
    const checkboxPercent = calculateMode(checkboxChoices)[1];
    const radioPercent = calculateMode(radioChoices)[1];
    if(checkboxMode.values().next().value)
        displayResults("checkboxResult", "CheckBox Questions Result:", checkboxMode, checkboxPercent);
    if(radioMode.values().next().value)
        displayResults("radioResult", "Radio Questions Result:", radioMode, radioPercent);
}
function calculateMode(choicesMap) {
    let modeMap = new Map();
    let percentMap = new Map();
    
    for (const [questionName, choices] of choicesMap) {
        let choiceCountMap = new Map();
        let maxCount = 0;
        let modeChoice = null;
        
        for (const choice of choices) {
            if (!choiceCountMap.has(choice)) {
                choiceCountMap.set(choice, 0);
            }
            
            const count = choiceCountMap.get(choice) + 1;
            choiceCountMap.set(choice, count);
            
            if (count > maxCount) {
                maxCount = count;
                modeChoice = choice;
            }
        }
        var percent = (maxCount / choices.length) * 100;
        modeMap.set(questionName, modeChoice);
        percentMap.set(questionName, percent.toFixed(2));
    }
    return [modeMap, percentMap];
}


function displayResults(resultElementId, resultHeader, resultMode, resultPercent) {
    var resultDiv = document.getElementById(resultElementId);
    resultDiv.innerHTML = "";
    var header = document.createElement("h4");
    header.innerHTML = resultHeader;
    resultDiv.appendChild(header);
    let percents = []
    for(const [questionName, percent] of resultPercent) {
        if(resultPercent.has(questionName))
            percents.push(percent)
    }
    console.log(percents)
    let i = 0;
    for (const [questionName, modeChoice] of resultMode) {
        var questionResult = document.createElement("p");
        questionResult.innerHTML = questionName + ": " + modeChoice + " & percentage:" + percents[i] + "%";
        i++;
        resultDiv.appendChild(questionResult);
    }
}