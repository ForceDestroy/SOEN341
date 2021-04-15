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
  localStorageCredentialsObj;

  constructor(private _formBuilder: FormBuilder, private postService: PostService, private router: Router) {
     // Invalid attempt to access page, kick them out
     if (!this.verifyCanLogin() ) {
      console.log("ERROR: Not logged in!")
      this.router.navigate(['../../auth/login']);
    }

    // If local storage contains valid userId then allow access
    else{
        console.log("Valid Viewing of New Post Page");

        //Retrieve user id from local storage to use for other methods
        this.userId = this.localStorageCredentialsObj.userId;

        /*
        this.postService.getNewPostId(this.userId).then((data)=>{
          let newData = JSON.parse(data);
          this.newPostId = newData.data;
        })
        this.postForm = this._formBuilder.group({
          imageFileCtrl: ['', Validators.required],
          captionCtrl: ['', Validators.required],
        });
        */

        this.postService.getNewPostId(this.userId).then((data)=>{
          let newData = JSON.parse(data);
          this.newPostId = newData.data;
         })
        this.postForm = this._formBuilder.group({
          imageFileCtrl: ['', Validators.required],
          captionCtrl: ['', Validators.required],
        });
    }
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
}
