import { Component, OnInit } from '@angular/core';
import { PostService } from '../post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-view',
  templateUrl: './post-view.component.html',
  styleUrls: ['./post-view.component.css']
})
export class PostViewComponent implements OnInit {

  postPicture: string;
  postComments: any = [];
  localStorageCredentialsObj;
  userId : Int32Array;

  constructor(private postService: PostService, private router: Router) { }

  ngOnInit(): void {
    // Invalid attempt to access page, kick them out
    if (!this.verifyCanLogin() ) {
      console.log("ERROR: Not logged in!")
      this.router.navigate(['../../auth/login']);
    }

    // If local storage contains valid userId then allow access
    else{
      console.log("Valid Viewing of Post View Page");

      //Retrieve user id from local storage to use for other methods
      this.userId = this.localStorageCredentialsObj.userId;

      this.postService.getAllPosts(this.userId).then((data)=>{
        let userPosts = JSON.parse(data);
        this.postComments = userPosts.data;
      })
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
