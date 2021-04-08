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


  userName: string;
  userId: string;
  followPosts: any = [];
  profileId: string;



  constructor(private homeService: HomeService, private profileService: ProfileService, private route: ActivatedRoute) {
    this.userId = localStorage.getItem('userId');
    this.userName = localStorage.getItem('username');
  }

  ngOnInit(): void {
    this.route.url.subscribe(params => {
      this.profileId = params[0].path;
      this.loadPageContent();
    })
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
}
