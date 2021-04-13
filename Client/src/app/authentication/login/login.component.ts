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
      let code = JSON.parse(res);
      let returnCode = code.returnCode;
      console.log(userDetails.username);
      console.log(returnCode);

      if (returnCode == 'false') {
        console.log('Invalid login attempt');
        //Show error on screen here
      }
      else {
        //Valid login attempt
        console.log('Succesfully logged in');
        //this.authService.setLoggedIn(true);
        const currentTime = new Date();
        const localStorageItem = {
          value: userDetails.username,
          expiry: currentTime.getTime() + 3600000, // expire in 1hr (=3600000 milliseconds)
        }
        localStorage.setItem('loginCredentials', JSON.stringify(localStorageItem));
        this.authService.subscribe(s=> this.router.navigate(['']));
      }
    });
  }

  ngOnInit(): void { }
}
