// Show Designer, Test Survey, JSON Editor and additionally Logic tabs
var options = {
    showLogicTab: true
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
var hostId = localStorage.getItem("id");
console.log(hostId)
var jwt = localStorage.getItem("jwt");
console.log(jwt);
document.getElementById("ok").addEventListener("click", () => { 
    
    var result = Object.assign({}, creator.JSON);
    var surveyRequestObject = {
        elements: [],
        title: "",
        description: ""
    };
    result.pages.forEach(element => {
        surveyRequestObject.elements = element.elements;
        surveyRequestObject.title = element.title;
        surveyRequestObject.description = element.description;
    });
    
    surveyRequest = JSON.stringify(surveyRequestObject);
    console.log(surveyRequest)
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
