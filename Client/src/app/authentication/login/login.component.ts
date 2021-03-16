import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userId: string;
  password: string;

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {}

login(){
  this.authService
  .login(this.userId, this.password)
  .subscribe(s => this.router.navigate(['']));
}
}

