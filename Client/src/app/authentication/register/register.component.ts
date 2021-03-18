import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  title = 'Client';
  userDetails: string;

  userForm=new FormGroup({
    username:new FormControl('', [Validators.required]),
    password:new FormControl('',[Validators.required]),
  });

  constructor(private router: Router, private authService: AuthService) {
    console.log('userform', this.userForm);
  }

  register() {
    if (!this.userForm.valid) {
      return;
      console.log("Invalid registration attempt")
    }
    const userDetails = this.userForm.getRawValue();
    console.log(userDetails);
    //Create user and add username, password to the database
    this.authService.createUser(userDetails).then(res => {
      let code = JSON.parse(res);
      let returnCode = code.returnCode;
      console.log(returnCode);
      if (returnCode == "false") {
        console.log("Invalid Register Attempt");
        //Show error on screen here
      }
      else {
        //Then redirect to the login page
        this.authService.subscribe(s=> this.router.navigate(['auth/login']));
        console.log("Succesfully registered");
      }
    });
  }
  ngOnInit(): void {}
}


