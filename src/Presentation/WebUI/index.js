var hostId = localStorage.getItem("hostId");
var jwt = localStorage.getItem("jwt");
var username = localStorage.getItem("firstname") + " " + localStorage.getItem("lastname");

var surveysUrl = `https://localhost:7146/hosts/${hostId}/surveys`;
var now = formatDate(new Date());
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
        if(element.isActive == false) 
            var isActiveOrElse = "#f42d2d";
        else  isActiveOrElse = "#beff33";
                  
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



function padTo2Digits(num) {
    return num.toString().padStart(2, '0');
}
  
function formatDate(date) {
    return (
      [
        date.getFullYear(),
        padTo2Digits(date.getUTCMonth() + 1),
        padTo2Digits(date.getUTCDate()),
      ].join('-') +
      ' ' +
      [
        padTo2Digits(date.getUTCHours() + 3),
        padTo2Digits(date.getUTCMinutes()),
        padTo2Digits(date.getUTCSeconds()),
      ].join(':')
    );
  }
  
  