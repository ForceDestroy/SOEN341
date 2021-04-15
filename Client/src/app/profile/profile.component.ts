import { Component, OnInit } from '@angular/core';
import { ProfileService } from './profile.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userName: string;
  localStorageCredentialsObj;
  userId : string;
  posts: any = [];
  profilePicture: string;

  constructor(private profileService: ProfileService, private router: Router) { }

  ngOnInit(): void {
    console.log("YOOOOO MAMA")
     // Invalid attempt to access page, kick them out
     if (!this.verifyCanLogin() ) {
      console.log("ERROR: Not logged in!")
      this.router.navigate(['../auth/login']);
    }

    // If local storage contains valid userId then allow access
    else{
      console.log("Valid Viewing of Profile Page");

      //Retrieve user id from local storage to use for other methods
      this.userId = this.localStorageCredentialsObj.userId;

      this.profileService.getAllPosts(this.userId).then((data)=>{
        let userPosts = JSON.parse(data);
        this.posts = userPosts.data;
      })
    }
  }

  verifyCanLogin() : boolean {
    const loginCredentials = localStorage.getItem('loginCredentials');
    // if the credentials doesn't exist means that no one ever logged in
    // Return false
    if (!loginCredentials) {
      return false
    }

    this.localStorageCredentialsObj = JSON.parse(loginCredentials)
    const now = new Date()
    // compare the expiry time of the item with the current time
    if (now.getTime() > this.localStorageCredentialsObj.expiry) {
      // If the item is expired, delete the item from storage
      localStorage.removeItem(loginCredentials)
      return false
    }

    return true
  }

}
