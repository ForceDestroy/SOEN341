import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HomeService } from './home.service';
import { ProfileService } from '../profile/profile.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  localStorageCredentialsObj;
  userName: string;
  userId: string;
  followPosts: any = [];
  profileId: string;

  constructor(private homeService: HomeService, private profileService: ProfileService, private route: ActivatedRoute,private router: Router ) {
    //this.userId = localStorage.getItem('userId');
  //  this.userName = localStorage.getItem('username');
  }

  ngOnInit(): void {
    if (!this.verifyCanLogin()) {
      console.log("ERROR: Not logged in!");
      this.router.navigate(['../auth/login']);
    }

    // If local storage contains valid username then allow access
    else {
      console.log("Valid Viewing of Home Page");

      //Retrieve user id from local storage to use for other methods
      this.userId = this.localStorageCredentialsObj.userId;
      this.route.url.subscribe(params => {
        this.profileId = params[0].path;
        this.loadPageContent();
      })

    }
  }

  loadPageContent() {
    this.homeService.getFollowPosts(this.profileId).then((data) => {
      console.log(this.profileId);
      let follower = JSON.parse(data);
      console.log(follower);
      this.followPosts = follower.data;
      console.log(this.followPosts);
    })
  }

  verifyCanLogin() {
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
