import { Component, OnInit } from '@angular/core';
import { PostService } from '../post.service';

@Component({
  selector: 'app-post-view',
  templateUrl: './post-view.component.html',
  styleUrls: ['./post-view.component.css']
})
export class PostViewComponent implements OnInit {

  postPicture: string;
  postComments: any = [];
  userId: string;

  constructor(private postService: PostService) { 
    this.userId = localStorage.getItem('userId');
  }

  ngOnInit(): void {
    this.postService.getAllPosts(this.userId).then((data)=>{
      let userPosts = JSON.parse(data);
      this.postComments = userPosts.data;
    })
  }

}
