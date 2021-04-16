import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  userDetails: string;
  title = 'Client';

  ROOT_URL: string = "https://localhost:44318";

  userForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(private router: Router, private authService: AuthService) {
    console.log('userform', this.userForm);
  }

  login() {
    if (!this.userForm.valid) {
      console.log('Invalid login attempt!');
      return;
    }

    const userDetails = this.userForm.getRawValue();
    console.log(userDetails);

    this.authService.getNewUsernamePassword(userDetails).then((res) => {
      let returnObject = JSON.parse(res);
      let returnCode = returnObject.returnCode;
      console.log(userDetails.username);
      console.log(returnCode);

      if (returnCode == 'false') {
        console.log('Invalid login attempt');
        //Show error on screen here
      }

      //Valid login attempt
      else {
        console.log('Succesfully logged in');

        //Add userId and session expiry time to local storage
        let loginUserId = returnObject.data.userId; //Believe this is how to get userId

        const currentTime = new Date();
        const localStorageItem = {
          userId: loginUserId,
          expiry: currentTime.getTime() + 3600000, // expire in 1hr (=3600000 milliseconds)
        }
        localStorage.setItem('loginCredentials', JSON.stringify(localStorageItem));
        localStorage.setItem('userId', loginUserId);
        localStorage.setItem('username', userDetails.username);

        //Navigate to home page
        this.router.navigate(['../../home/'+loginUserId]);
      }
    });
  }

  ngOnInit(): void { }
}
