import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProfileService } from './profile.service';

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
  numFollowers: any;
  numFollowing: any;
  numPosts: any;
  profileId: string;
  isFollowing: boolean = false;

  constructor(private profileService: ProfileService, private route: ActivatedRoute,private router: Router) {

  }

  ngOnInit(): void {
    console.log("YOOOOO MAMA")
     // Invalid attempt to access page, kick them out
     if (!this.verifyCanLogin() ) {
      console.log("ERROR: Not logged in!")
      this.router.navigate(['../auth/login']);
    }

    // If local storage contains valid userId then allow access
    else {
      console.log("Valid Viewing of Profile Page");

      //Retrieve user id from local storage to use for other methods
      this.userId = this.localStorageCredentialsObj.userId;

      this.route.url.subscribe(params => {
        this.profileId = params[0].path;
        this.loadPageContent();
      })

      /*this.profileService.getAllPosts(this.userId).then((data)=>{
        let userPosts = JSON.parse(data);
        this.posts = userPosts.data;
      })
      */
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

  loadPageContent(){
    this.profileService.getUserInfo(this.profileId).then((data)=>{
      let userInfo = JSON.parse(data);
      console.log(userInfo);
      this.posts = userInfo.data.posts;
      this.userName = userInfo.data.username;
      this.numFollowers = userInfo.data.followers.length;
      this.numFollowing = userInfo.data.following.length;
      this.numPosts = userInfo.data.posts.length;
      this.profilePicture = userInfo.data.profilePicture;
      for(let i = 0; i < userInfo.data.followers.length; i++){
        if(userInfo.data.followers[i].userId == this.userId){
          this.isFollowing = true;
        }
      }
    })
  }

  follow(){
    this.isFollowing = true;
    this.profileService.addFollowing(this.userId, this.profileId).then((data)=>{
      let newData = JSON.parse(data);
      console.log(newData);
      this.loadPageContent();
    })
  }

  unfollow(){
    this.isFollowing = false;
    this.profileService.removeFollowing(this.userId, this.profileId).then((data)=>{
      let newData = JSON.parse(data);
      console.log(newData);
      this.loadPageContent();
    })
  }
}
