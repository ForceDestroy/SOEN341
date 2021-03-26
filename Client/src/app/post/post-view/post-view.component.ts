import { Component, OnInit} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PostService } from '../post.service';
import { ProfileService } from '../../profile/profile.service'

@Component({
  selector: 'app-post-view',
  templateUrl: './post-view.component.html',
  styleUrls: ['./post-view.component.css']
})
export class PostViewComponent implements OnInit {

  postUsername: string;
  postPicture: string;
  userId: any = localStorage.getItem('userId');
  postProfilePicture: string;
  postCaption: string;
  numLikes: any;
  postId:string;
  postUserId: string;
  postComments: any = [];
  postLiked: boolean = false;
  username: string;
  userComment: string;
  


  constructor(private postService: PostService,private route: ActivatedRoute, private profileService: ProfileService) { 
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
      for(let i = 0; i < postData.data.likes.length; i++){
        if(this.userId == postData.data.likes[i]){
          this.postLiked = true;
        }
      }
      console.log(this.postLiked);
      this.postUserId = postData.data.userId;
      this.postPicture = postData.data.image;
      this.postComments = postData.data.comments;
      this.postCaption = postData.data.caption;
      this.numLikes = postData.data.likes.length;
      this.profileService.getUserInfo(this.postUserId).then((data) =>{
        let profileData = JSON.parse(data);
        this.postUsername = profileData.data.name;
        this.postProfilePicture = profileData.data.profilePicture;
      })
    })
    
  }

  addComment(comment){
    let payload = {
      "username": this.username,
      "postId": this.postId,
      "content": comment
    };
    this.postService.addComment(payload).then(() =>{
      this.loadPageContent();
      this.userComment = "";
    })
  }

  likePost(){
    let payload = {
      "postId": this.postId,
      "userId": this.userId,
    };
    if(!this.postLiked){
      this.postService.addLikeReaction(payload).then(() =>{
        this.loadPageContent();
        this.postLiked = true;
      })
    }
    else{
      this.postService.removeLikeReaction(payload).then(() =>{
        this.loadPageContent();
        this.postLiked = false;
      })
    }
  }
  

}
