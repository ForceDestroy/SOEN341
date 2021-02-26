import { Component, OnInit } from '@angular/core';
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

  constructor(private profileServer: ProfileService) {
    this.userName = localStorage.getItem('username');
    this.userId = localStorage.getItem('userId');
  }

  ngOnInit(): void {
    this.profileServer.getAllPosts(this.userId).then((data)=>{
      let userPosts = JSON.parse(data);
      this.posts = userPosts.data;
    })
  }

}
