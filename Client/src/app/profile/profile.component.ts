import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
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

  constructor(private profileService: ProfileService, private router: Router) {
    this.userName = localStorage.getItem('username');
    this.userId = localStorage.getItem('userId');
    this.numFollowers = localStorage.getItem('followers');
    this.numFollowing = localStorage.getItem('following');
    this.numPosts = localStorage.getItem('numPosts');
  }

  ngOnInit(): void {
    this.profileService.getAllPosts(this.userId).then((data)=>{
      let userPosts = JSON.parse(data);
      this.posts = userPosts.data;
    })
  }
}
