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

document.getElementById("ok").addEventListener("click", () => { 
    
    var result = Object.assign({}, creator.JSON);
    var surveyRequestObject = {
        elements: [],
        title: "",
        description: "",
        expiryDate: ""
    };
    var expiryDate = document.getElementById("expiryDate").value;
    const isValidDate = validateDateInput(expiryDate);
    
    result.pages.forEach(element => {
        surveyRequestObject.elements = element.elements;
        surveyRequestObject.title = element.title;
        surveyRequestObject.description = element.description;
        surveyRequestObject.expiryDate = expiryDate;
    });
    surveyRequestObject.elements.forEach(element => {
        console.log(element.type)
        if(element.type !== 'checkbox' && 
            element.type !== 'radiogroup' && 
            element.type !== 'text' && 
            element.type !== 'boolean' && 
            element.type !== 'rating' && 
            element.type !== 'comment')
            Swal.fire({
                title: 'Sorry! Unsupported question type!',
                text: `${element.type} is not supported on that survey!`,
                icon: 'error',
                confirmButtonText: 'OK'
            }).then(result => {
                if(result)
                    window.location.reload();
            });
    })
    surveyRequest = JSON.stringify(surveyRequestObject);
    if(isValidDate)
        createSurveyRequest(surveyRequest);
    else
        Swal.fire({
            title: 'Problem! Invalid expiry date!',
            text: `${expiryDate} is not smaller than UTC.Now + 30 min!`,
            icon: 'error',
            confirmButtonText: 'OK'
        }).then(result => {
            if(result)
                window.location.reload()
        });
});

async function createSurveyRequest(surveyRequest) {
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

    const response = await fetch(req);
    if(response.status == 201) {
        response.json().then(data => {
        Swal.fire({
            title: 'Success!',
            icon: 'success',
            text: `${data.title} is created`,
            confirmButtonText: 'OK'
        }).then(result => {
            if(result)
                    window.location.href = `./surveyPage.html?=${data.id}`
                });
        });
    }
        
}

function validateDateInput(date) {
    var userInput = new Date(date);
    var today = new Date();
    today.setMinutes(today.getMinutes() + 30);
    if(userInput.getTime() <= today.getTime())
        return false;
    console.log(userInput);
    return true;
}
