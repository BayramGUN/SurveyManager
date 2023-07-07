// Show Designer, Test Survey, JSON Editor and additionally Logic tabs
var options = {
    showLogicTab: false,
};
//create the SurveyJS Creator and render it in div with id equals to "creatorElement"
var creator = new SurveyCreator.SurveyCreator("creatorElement", options);
//Show toolbox in the right container. It is shown on the left by default
creator.showToolbox = "right";
//Show property grid in the right container, combined with toolbox
creator.showPropertyGrid = "right";
//Make toolbox active by default
creator.rightContainerActiveItem("toolbox");
var firstname = localStorage.getItem("firstname");
var lastname = localStorage.getItem("lastname");
var hostId = localStorage.getItem("hostId");
var jwt = localStorage.getItem("jwt");

document.getElementById("logout").addEventListener("click", () => { 
    localStorage.removeItem("jwt");
    window.location.href = `./Authentication/login.html`;   
})
console.log(hostId);
document.getElementById("getBack").addEventListener("click", () => { 
    window.location.href = `./index.html`;   
})
console.log(hostId);

document.getElementById("ok").addEventListener("click", () => { 
    
    var result = Object.assign({}, creator.JSON);
    var surveyRequestObject = {
        elements: [],
        title: "",
        description: "",
        expiryDate: ""
    };
    var expiryDate = document.getElementById("expiryDate").value;
    result.pages.forEach(element => {
        surveyRequestObject.elements = element.elements;
        surveyRequestObject.title = element.title;
        surveyRequestObject.description = element.description;
        surveyRequestObject.expiryDate = expiryDate;
    });
    surveyRequest = JSON.stringify(surveyRequestObject);
    const req = new Request(`https://localhost:7146/hosts/${hostId}/surveys`, {
      method: 'POST',
      mode: 'cors',
      headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Authorization': 'Bearer ' + jwt
      },
      body: surveyRequest
    }) 
    fetch(req)
        .then(response => {
            response.json().then(data => {
                window.location.href = `./surveyPage.html?=` + data.id;
            })
        });
});

