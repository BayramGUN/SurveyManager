var jwt = localStorage.getItem("jwt");
/* if (jwt != null) {
  //window.location.href = './index.html'
} 
 */
const objects = {
  "messageOk": 'Login Success!',
  "messageNo": 'Login Failed!',
  "messageEmptyString": 'You must fill the blanks!' 
}

function login() {
  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;
  localStorage.setItem("useremail", username);
  
  
  let isStrEmpty = true;
  if(!username || !password) {
    swalFire(objects.messageEmptyString, 'warning');
    isStrEmpty = false;
  }
  const input = {
    "email": username,
    "password": password,
    
  }
  const jsonVal = JSON.stringify(input);
  
  if(isStrEmpty) {
  const req = new Request('https://localhost:7146/auth/login', {
      method: 'POST',
      mode: 'cors',
      headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
      },
      body: jsonVal
  }) 

  fetch(req).then(response => {
    response.json().then((data) => {
      const user = data; 
      localStorage.setItem("firstname", user.firstname);
      localStorage.setItem("lastname", user.lastname);
      localStorage.setItem("hostId", user.id);
      localStorage.setItem("jwt", user.token)
    });
    if (response['status'] == 200) {
      Swal.fire({
        text: objects['messageOk'],
        icon: 'success',
        confirmButtonText: 'OK'
      }).then((result) => {
      if (result.isConfirmed) {
        window.location.href = '../index.html';
      }
      });
      } else {
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


