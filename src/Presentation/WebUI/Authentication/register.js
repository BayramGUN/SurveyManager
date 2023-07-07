var jwt = localStorage.getItem("jwt");
/* if (jwt != null) {
  window.location.href = './index.html'
}  */

const objects = {
  messageOk: 'Registeration Success!',
  messageNo: 'Registeration Failed!',
  messageEmptyString: 'You must fill the blanks!' 
}

function register() {
  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;
  const firstname = document.getElementById("firstname").value;
  const lastname = document.getElementById("lastname").value;
  let isStrEmpty = true;
  if(!username || !password || !lastname || !firstname) {
    swalFire(objects.messageEmptyString, 'warning');
    isStrEmpty = false;
  }
  const input = {
    email: username,
    password: password,
    firstname: firstname,
    lastname: lastname
  }
  const jsonVal = JSON.stringify(input);
  
  if(isStrEmpty) {
  const request = new Request('https://localhost:7146/auth/register', {
      method: 'POST',
      mode: 'cors',
      headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
      },
      body: jsonVal
  }) 

  fetch(request).then(response => {
    response.json().then((data) => {
      let user = data; 
      localStorage.setItem("firstname", user.firstname);
      localStorage.setItem("lastname", user.lastname);
      localStorage.setItem("hostId", user.id);
      localStorage.setItem("jwt", user.token);
    });
    if (response['status'] == 201) {
      Swal.fire({
        text: objects.messageOk,
        icon: 'success',
        confirmButtonText: 'OK'
      }).then((result) => {
      if (result.isConfirmed) {
            window.location.href = './../index.html';
          }
      });
      } else {
        console.log()
        swalFire(objects.messageNo, 'error');
      }
    }).catch(err => {
        console.log(err)  
    })
  }
  return false;
}


function swalFire(message, icon) {
    Swal.fire({
      text: message,
      icon: icon,
      confirmButtonText: 'OK'
    });
}


