import { Component, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PostService } from '../post.service';

@Component({
  selector: 'app-post-view',
  templateUrl: './post-view.component.html',
  styleUrls: ['./post-view.component.css']
})
export class PostViewComponent implements OnInit {

  postUsername: string;
  postPicture: string;
  postCaption: string;
  postId:string;
  postComments: any = [];

  username: string;
  userComment: string;
  


  constructor(private postService: PostService,private route: ActivatedRoute) { 
    this.username = localStorage.getItem('username');
  }

  ngOnInit(): void {
    this.route.url.subscribe(params => {
      this.postId = params[0].path;
      this.loadPageContent();
    })
  }

  loadPageContent(){
    this.postService.getPost(this.postId).then((data) =>{
      let postData = JSON.parse(data);
      this.postPicture = postData.data.image;
      this.postComments = postData.data.comments;
      this.postCaption = postData.data.caption;
    })
  }

  addComment(comment){
    let payload = {
      "username": this.username,
      "postId": this.postId,
      "content": comment
    };
    this.postService.addComment(payload).then((data) =>{
      this.loadPageContent();
    })
  }

  

}
