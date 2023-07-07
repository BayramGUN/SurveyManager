var hostId = localStorage.getItem("hostId");
var jwt = localStorage.getItem("jwt");
var username = localStorage.getItem("firstname") + " " + localStorage.getItem("lastname");
console.log(username);

var surveysUrl = `https://localhost:7146/hosts/${hostId}/surveys`;

function getSurveysData(surveyUrl) {
    fetch(surveyUrl, {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + jwt
        }
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        surveyPage(data);
    })
    .catch(error => {
        console.log('Error fetching survey questions:', error);
	});
}
getSurveysData(surveysUrl);

function surveyPage(data) {
    document.getElementById('fname').innerText = username;
    data.hostSurveys.forEach(element => {
        var isActiveOrElse = "#f42d2d";
        if (element.isActive) isActiveOrElse = "#beff33";
        document.getElementById("surveys").innerHTML += 
        `<div class="col">
            <div class="card">
                <div class="card-header" style="background-color: ${isActiveOrElse};">
                    <h3 class="text-white">${element.title}</h3>
                </div>
                <div class="card-body" id=${element.id.value}>
                    <p>${element.description}</p>
                    <button type="button" class="btn btn-info" id=${element.id.value} onClick="getDetailsOfSurvey(this)">Get Details</button>
                </div>
            </div>
        </div>`
        
    });
        
}

function getDetailsOfSurvey(button) {
    var surveyId = button.parentNode.id;
    console.log("Survey ID: " + surveyId);
    localStorage.setItem("surveyId", surveyId)
    window.location.href = './surveyResult.html?=' + surveyId;
}


function logout(){
    localStorage.removeItem("hostId");
    localStorage.removeItem("jwt");
    window.location.href = 'Authentication/login.html';
}
function surveyCreator(){
    window.location.href = './surveyCreate.html';
}

