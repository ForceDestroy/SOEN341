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
  userId: string;
  posts: any = [];
  profilePicture: string;
  numFollowers: any;
  numFollowing: any;
  numPosts: any;
  profileId: string;
  isFollowing: boolean = false;

  constructor(private profileService: ProfileService, private route: ActivatedRoute) {
    this.userId = localStorage.getItem('userId');
  }

  ngOnInit(): void {
    this.route.url.subscribe(params => {
      this.profileId = params[0].path;
      this.loadPageContent();
    })
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
    })
  }

  follow(){
    this.profileService.addFollowing(this.userId, this.profileId).then((data)=>{
      let newData = JSON.parse(data);
      console.log(newData);
    })
  }
}
