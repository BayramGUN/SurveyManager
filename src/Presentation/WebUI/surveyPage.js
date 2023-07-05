

	var url = window.location.search;
	url = url.replace("?", ''); // remove the ?
	var req = 'https://localhost:7146/anonymsurveys/survey?id' + url;

	fetch(req)
	  	.then(response => response.json())
	  	.then(data => {
	    	// Call a function to generate the form based on the received questions
			console.log(data)
	    	generateSurvey(data);
	  	})
	 	.catch(error => {
		  console.log('Error fetching survey questions:', error);
		});

	
	
	function generateSurvey(data) {
		createSurveyHeader(data);
		localStorage.setItem("hostId", data.hostId);
		localStorage.setItem("surveyId", data.id);
		// console.log(answerReq)
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
		console.log(question.name)
		document.getElementById("surveyContainer").innerHTML += `<label class="block text-gray-700 font-medium mb-2"> ${question.name} </label>`;
		document.getElementById("surveyContainer").innerHTML += 
						`<div class="mb-4">
							<div class="flex flex-wrap -mx-2">
								<div class="px-2 w-1/3">
									<label for="f" class="block text-gray-700 font-medium mb-2">
										<input type="radio" id="f" name=${question.name} value="Yes" class="mr-2"> Yes
										<input type="radio" id="f" name=${question.name} value="No" class="mr-2"> No	
			                    	</label>
								</div>
							</div>
						</div>`

	question.choices.forEach(choice => {
		document.getElementById("surveyContainer").innerHTML += 
		`<div class="mb-4">
			<div class="flex flex-wrap -mx-2">
				<div class="px-2 w-1/3">
					<label for="deger" class="block text-gray-700 font-medium mb-2">
						<input type="radio" id="deger" name=${question.name} value=${choice} class="mr-2"> ${choice}
						</label>
				</div>
		</div>`
	});
	}
	function createTextElement(question) {
		document.getElementById("surveyContainer").innerHTML += 
				`<div class="mb-4">
					<label for="message" class="block text-gray-700 font-medium mb-2">${question.name}</label>
					<input type="text" id=${question.name} name=${question.name} class="border border-gray-400 p-2 w-full rounded-lg focus:outline-none focus:border-blue-400" required>	
	        	</div>`;
			
	}
	function createCheckboxElement(question) {

		console.log(question);
		document.getElementById("surveyContainer").innerHTML += `<label class="block text-gray-700 font-medium mb-2"> ${question.name} </label>`;

		question.choices.forEach(choice => {
			document.getElementById("surveyContainer").innerHTML += `<div class="mb-4">
				<div class="flex flex-wrap -mx-2">
				    <div class="px-2 w-1/3">
				        <label for="deger" class="block text-gray-700 font-medium mb-2">
				            <input type="checkbox" id="deger" name=${question.name} value=${choice} class="mr-2"> ${choice}
							</label>
				    </div>
				</div>`
		});

	}
	function createRatingElement(question) {
		document.getElementById("surveyContainer").innerHTML += `<label class="block text-gray-700 font-medium mb-2"> ${question.name} </label>`;
				for(let i = 1; i <= question.rateMax; i++)
					document.getElementById("surveyContainer").innerHTML += 
						`<div class="mb-4">
							<div class="flex flex-wrap -mx-2">
								<div class="px-2 w-1/3">
									<label for="radiogroup" class="block text-gray-700 font-medium mb-2">
										<input type="radio" id="radiogroup" name=${question.name} value=${i} class="mr-2"> ${i}
			                    	</label>
								</div>
							</div>
						</div>`
	}
	function createRadioGroupElement(question) {
		document.getElementById("surveyContainer").innerHTML += `<label class="block text-gray-700 font-medium mb-2"> ${question.name} </label>`;

		question.choices.forEach(choice => {
			document.getElementById("surveyContainer").innerHTML += `<div class="mb-4">
				<div class="flex flex-wrap -mx-2">
				    <div class="px-2 w-1/3">
				        <label for="deger" class="block text-gray-700 font-medium mb-2">
				            <input type="radio" id="deger" name=${question.name} value=${choice} class="mr-2"> ${choice}
						</label>
				    </div>
				</div>`
		});

	}
	function createCommentElement(question) {	
	    document.getElementById("surveyContainer").innerHTML += 
			`<div class="mb-4">
				<label for="name" class="block text-gray-700 font-medium mb-2"> ${question.name} </label>
				<textarea id=${question.name} name=${question.name}
					class="border border-gray-400 p-2 w-full rounded-lg focus:outline-none focus:border-blue-400" rows="5"></textarea>
			</div>`
	}

	document.getElementById("btn").addEventListener("click", function(event) {
	  event.preventDefault(); 

	  processSurvey();
	});

	var hostId = localStorage.getItem("hostId");
	var surveyId = localStorage.getItem("surveyId");
	console.log(hostId);
	console.log(surveyId);
    var surveyData = {
		surveyId: surveyId,
		hostId: hostId,
		answers: []
  	};
  
  function processSurvey() {
	var answerElements = document.querySelectorAll("#surveyContainer input[type='text'], #surveyContainer input[type='radio']:checked, #surveyContainer input[type='checkbox']:checked, #surveyContainer textarea");
  
	answerElements.forEach(function(element) {
	  var questionName = element.name;
	  var answerValue = element.value;
  
	  // Check if a question with the same name already exists in the answers array
	  var existingQuestion = surveyData.answers.find(function(question) {
		return question.questionName === questionName;
	  });
  
	  if (existingQuestion) {
		existingQuestion.answer.push(answerValue);
	  } else {
		var newQuestion = {
		  questionName: questionName,
		  answer: [answerValue]
		};
		surveyData.answers.push(newQuestion);
	  }
	});
  
	// Print the generated JSON value
	sendRequest(JSON.stringify(surveyData))
  }
  
  // ...
  
	// Print the collected answers as JSON (you can modify this part to suit your needs)
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
    
  // ...
  

