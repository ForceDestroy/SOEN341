import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { HttpClient } from '@angular/common/http';
import { stringify } from '@angular/compiler/src/util';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  userDetails: string;

  userForm=new FormGroup({
    username:new FormControl('', [Validators.required]),
    password:new FormControl('',[Validators.required]),
  });

  constructor(private router: Router, private authService: AuthService) {
   console.log('userform', this.userForm);
  }

  login(){
    if (!this.userForm.valid) {
      return;
      console.log("Invalid login attempt!")
    }

    const userDetails = this.userForm.getRawValue();
    console.log(userDetails);

    this.authService.getNewUsernamePassword(userDetails).then(res => {
      let code = JSON.parse(res);
      let returnCode = code.returnCode;
      console.log(userDetails.username);
      console.log(returnCode);
      if (returnCode == "false") {
        console.log("Invalid login attempt");
        //Show error on screen here
      }
      else {
        //Valid login attemot
        console.log("Succesfully logged in");
        this.authService.subscribe(s=> this.router.navigate(['']));
      }
    })
  }

  ngOnInit(): void {
  }
}
  /*
  login(){
    this.authService.getNewusernamePassword(this.username,this.password).then(res => {
      let code = JSON.parse(res);
      let returnCode = code.returnCode;
      if (returnCode == "false") {
        this.authService.subscribe(s => this.router.navigate(['/login'])) ;
        console.log("Invalid login attempt");
      }
      else {
      this.authService
      .login(this.username, this.password)
      .subscribe(s => this.router.navigate(['']));
      console.log("Succesfully logged in");
      }
    })
    }
    */

      /*
  this.authService.getusername(this.username).then((data)=>{
    let username = JSON.parse(data);
    this.username = username.data;
  })

  this.authService.getPassword(this.password).then((data)=>{
    let password = JSON.parse(data);
    this.password = password.data;
  })
  */

