var url = window.location.search;
url = url.replace("?", ''); // remove the ?
url = url.replace("=", ''); // remove the =
var reqUrl = 'https://localhost:7146/anonymsurveys/survey?id=' + url;
localStorage.removeItem("surveyId");
localStorage.setItem("id", url);


getSurveyData(reqUrl);

function getSurveyData(reqUrl) {
    fetch(reqUrl)
    .then(response => response.json())
    .then(data => {
        generateSurvey(data);
    })
    .catch(error => {
        console.log('Error fetching survey questions:', error);
	});
}

function generateSurvey(data) {
    createSurveyHeader(data);
    localStorage.setItem("hostId", data.hostId);
    localStorage.setItem("id", data.id);
	data.questions.forEach(function(question) {
        switch(question.type)
		{
			case 'text':
				createTextElement(question);
				break;
			case 'radiogroup':
				createRadioGroupElement(question);
				break;
			case 'comment':
				createCommentElement(question);
				break;
			case 'rating':
				createRatingElement(question);
				break;
			case 'checkbox':
				createCheckboxElement(question);
				break;
			case 'boolean':
				createBooleanElement(question)
				break;
		}
	});
}
function createSurveyHeader(data) {
	document.getElementById("title").innerHTML = data.title;
	document.getElementById("desc").innerHTML = data.description;
}


function createBooleanElement(question) {
	document.getElementById("surveyContainer").innerHTML += 
        `<label class="block text-gray-700 font-medium mb-2"> ${question.title} </label>`;
	document.getElementById("surveyContainer").innerHTML += 
		`<div class="mb-4">
			<div class="flex flex-wrap -mx-2">
				<div class="px-2 w-1/3">
					<label for"${question.type}_Yes" class="block text-gray-700 font-medium mb-2">
                        <input type="radio" id"${question.type}_Yes" name=${question.title} value="Yes" class="mr-2"> Yes
                    </label>
					<label for"${question.type}_No" class="block text-gray-700 font-medium mb-2">
                        <input type="radio" id"${question.type}_No" name=${question.title} value="No" class="mr-2"> No	
                    </label>
				</div>
			</div>
		</div>`
}

function createTextElement(question) {
    document.getElementById("surveyContainer").innerHTML += 
        `<div class="mb-4">
            <label for="${question.type}" class="block text-gray-700 font-medium mb-2">${question.title}</label>
            <input type="text" id="${question.type}" name="${question.title}" class="border border-gray-400 p-2 w-full rounded-lg focus:outline-none focus:border-blue-400" required>    
        </div>`;
}

function createCommentElement(question) {
    document.getElementById("surveyContainer").innerHTML += 
        `<div class="mb-4">
            <label for="${question.type}" class="block text-gray-700 font-medium mb-2">${question.title}</label>
            <textarea type="text" id="${question.type}" name="${question.title}" class="border border-gray-400 p-2 w-full rounded-lg focus:outline-none focus:border-blue-400" required></textarea> 
        </div>`;
}

function createCheckboxElement(question) {
    document.getElementById("surveyContainer").innerHTML += `<label class="block text-gray-700 font-medium mb-2">${question.title}</label>`;
    question.choices.forEach(choice => {
        document.getElementById("surveyContainer").innerHTML += `<div class="mb-4">
            <div class="flex flex-wrap -mx-2">
                <div class="px-2 w-1/3">
                    <label for="${question.type}_${choice}" class="block text-gray-700 font-medium mb-2">
                        <input type="checkbox" id="${question.type}_${choice}" name="${question.title}" value="${choice}" class="mr-2"> ${choice}
                    </label>
                </div>
            </div>`
    });
}

function createRatingElement(question) {
    document.getElementById("surveyContainer").innerHTML += 
        `<label class="block text-gray-700 font-medium mb-2">${question.title}</label>`;
    for(let i = 1; i <= question.rateMax; i++)
        document.getElementById("surveyContainer").innerHTML += 
            `<div class="mb-4">
                <div class="flex flex-wrap -mx-2">
                    <div class="px-2 w-1/3">
                        <label for="${question.type}_${i}" class="block text-gray-700 font-medium mb-2">
                            <input type="radio" id="${question.type}_${i}" name="${question.title}" value="${i}" class="mr-2"> ${i}
                        </label>
                    </div>
                </div>
            </div>`
}

function createRadioGroupElement(question) {
    document.getElementById("surveyContainer").innerHTML += 
        `<label class="block text-gray-700 font-medium mb-2">${question.title}</label>`;
    question.choices.forEach(choice => {
        document.getElementById("surveyContainer").innerHTML += `<div class="mb-4">
            <div class="flex flex-wrap -mx-2">
                <div class="px-2 w-1/3">
                    <label for="${question.type}_${choice}" class="block text-gray-700 font-medium mb-2">
                        <input type="radio" id="${question.type}_${choice}" name="${question.title}" value="${choice}" class="mr-2"> ${choice}
                    </label>
                </div>
            </div>`
    });
}


document.getElementById("btn").addEventListener("click", function(event) {
    event.preventDefault(); 
    processSurvey();
});


var hostId = localStorage.getItem("hostId");
var token = localStorage.getItem("jwt");
var surveyId = localStorage.getItem("id");
var surveyData = {
	surveyId: surveyId,
	hostId: hostId,
	answers: []
};
document.getElementById("getBack").addEventListener("click", function(event) {
    event.preventDefault();
    if (token) {
        window.location.href = `./index.html`;
        console.log(token)
    }
    else {
        Swal.fire({
            text: "Please submit the survey",
            icon: 'warning',
            confirmButtonText: 'OK'
        });
    }
});

function processSurvey() {
    var answerElements = document
        .querySelectorAll(
            `#surveyContainer input[type='text'], 
            #surveyContainer input[type='radio']:checked, 
            #surveyContainer input[type='checkbox']:checked, 
            #surveyContainer textarea`
        );
    
	answerElements.forEach(function(element) {
        var questionName = element.name;
	    var answerValue = element.value;
        var questionType = element.type;
        console.log(questionName);
	    var existingQuestion = surveyData.answers.find(function(question) {
            return question.questionName === questionName;
	    });
        
	    if (existingQuestion) {
	        existingQuestion.answer.push(answerValue);
	    } else {
	    	var newQuestion = {
                questionName: questionName,
                type: questionType,
                answer: [answerValue]
	    	};
	    	surveyData.answers.push(newQuestion);
	    }
    });

    console.log(surveyId);
    sendRequest(JSON.stringify(surveyData));
    
    
    
}

function sendRequest(jsonString) {
    const req = new Request(`https://localhost:7146/anonymsurveys/answers`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
        },
        body: jsonString
    }) 
    fetch(req)
        .then(response => {
            response.json().then(data => {
				console.log(data);
            })
        });
   
}
    
  

