import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import {FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { PostService } from '../post.service';

@Component({
  selector: 'app-newpost',
  templateUrl: './newpost.component.html',
  styleUrls: ['./newpost.component.css']
})
export class NewpostComponent implements OnInit {

  postForm: FormGroup;
  userId: string;
  newPostId: string;

  constructor(private _formBuilder: FormBuilder, private postService: PostService, private router: Router ) { 
    this.userId = localStorage.getItem('userId');
    this.postService.getNewPostId(this.userId).then((data)=>{
      let newData = JSON.parse(data);
      this.newPostId = newData.data;
    })
    this.postForm = this._formBuilder.group({
      imageFileCtrl: ['', Validators.required],
      captionCtrl: ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }

  createPostObject(){
    if(this.postForm.valid){
      let payload = {};
      payload = this.createPayload()
      console.log(payload);
      this.postService.createPost(payload).then(()=>{
        this.router.navigateByUrl("/user/"+this.userId);
      })
    }
  }

  createPayload(){
    return{
      "userId": parseInt(this.userId),
      "postId": this.userId + '-' + this.newPostId,
      "image": this.postForm.controls.imageFileCtrl.value,
      "caption": this.postForm.controls.captionCtrl.value,
      "comments": [],
      "date": "2021-02-26T02:42:51.233Z",
      "likes": [],
      "hearts": [],
      "likesList": [],
      "heartsList": [],
    }
  }
}
