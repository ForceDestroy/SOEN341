import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PostService } from '../post.service';
import { Router } from '@angular/router'

@Component({
  selector: 'app-newpost',
  templateUrl: './newpost.component.html',
  styleUrls: ['./newpost.component.css']
})
export class NewpostComponent implements OnInit {

  postForm: FormGroup;
  userId: string;
  newPostId: string;
  urlData: any;
  imgurUrl: string;
  clientId: string = "6af588629190cb3"

  constructor(private _formBuilder: FormBuilder, private postService: PostService, private router: Router) { 
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

  createFileUrl(event){
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is completed
        this.urlData = event.target.result.toString().split(',')[1];
      }
    }
  }

  createPostObject(){
    if(this.postForm.valid){
      let payload = {};
      payload = this.createPayload()

      console.log(payload);
      this.postService.createPost(payload).then((data)=>{
        let newData = JSON.parse(data);
        this.router.navigateByUrl("/user/"+this.userId);
      })
    }
  }

  createPayload(){
    let postDate = new Date();
    return{
      "userId": parseInt(this.userId),
      "postId": this.userId + '-' + this.newPostId,
      "image": this.urlData,
      "caption": this.postForm.controls.captionCtrl.value,
      "comments": [],
      "date": postDate.toISOString(),
      "likes": [],
      "hearts": [],
      "likesList": [],
      "heartsList": [],
    }
  }
}
