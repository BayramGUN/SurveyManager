var url = window.location.search;
url = url.replace("?", ''); // remove the ?
url = url.replace("=", ''); // remove the =
var reqUrl = 'https://localhost:7146/anonymsurveys/survey?id=' + url;
localStorage.removeItem("surveyId");
localStorage.setItem("id", url);
const jwt = localStorage.getItem("jwt");

getSurveyData(reqUrl);
async function getSurveyData(reqUrl) {
    try {
        
        const response = await fetch(reqUrl);
        const data = await response.json();
        
        if(data.isActive == false)
        window.location.href = './unexpiry.html';
    
    generateSurvey(data) 
    return data;
} catch (error) {
    Swal.fire ({
        title: `${error.message}`,
        icon: 'error',
        confirmButtonText: 'OK'
        });
    }
}

async function generateSurvey(data) {
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
            default:
                Swal.fire({
                    title: 'Sorry! Unsupported question type!',
                    text: `${question.type} is not supported on that survey!`,
                    confirmButtonText: 'OK'
                }).then(result => {
                    if(result)
                        window.location.href = './index.html'
                });
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
        `<label class="block text-gray-700 font-medium mb-2">${question.title}</label>`;
	document.getElementById("surveyContainer").innerHTML += 
		`<div class="mb-4">
			<div class="flex flex-wrap -mx-2">
				<div class="px-2 w-1/3">
					<label for"${question.type}_Yes" class="block text-gray-700 font-medium mb-2">
                        <input type="radio" id="${question.type}_Yes" name="${question.title}" value="Yes" class="mr-2"> Yes
                    </label>
					<label for"${question.type}_No" class="block text-gray-700 font-medium mb-2">
                        <input type="radio" id="${question.type}_No" name="${question.title}" value="No" class="mr-2"> No	
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


var surveyId = localStorage.getItem("id");
var surveyData = {
	surveyId: surveyId,
	answers: []
};
document.getElementById("getBack").addEventListener("click", function(event) {
    event.preventDefault();
    if (jwt) {
        window.location.href = `./index.html`;
    } else {
            Swal.fire({
                title: 'Unexpected operation!',
                text: "You are unauthorized to do that!",
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
    console.log(JSON.stringify(surveyData));
    sendRequest(JSON.stringify(surveyData));
     
}

async function sendRequest(jsonString) {
    const req = new Request(`https://localhost:7146/anonymsurveys/answers`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
        },
        body: jsonString
    }) 
    const response = await fetch(req);

    if(response.status != 201)
        Swal.fire({
            text: `there is a problem like ${response.status}`,
            icon: 'error',
            confirmButtonText: 'OK'
        });
    const data = await response.json();
    submitation();
    return data;

}

function submitation() {
        Swal.fire({
            text: `The survey has been answered!`,
            icon: 'success',
            confirmButtonText: 'OK'
        }).then(result => {
            if(result && !jwt)
                window.location.href = './submitSurveyPage.html';
            else
                window.location.href = './index.html';
        });
}


