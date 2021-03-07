import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  ROOT_URL: string = "https://localhost:44318"

  constructor(private http: HttpClient) { }

  createPost(data){
    return new Promise<string>(resolve => {
      return this.http.post<any>(this.ROOT_URL + '/post/AddNewPost', data).subscribe((data)=>{ 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully created post.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }

  getAllPosts(userId){
		return new Promise<string>(resolve => {
      return this.http.get(this.ROOT_URL + '/post/GetPostsByUserId?id=' + userId).subscribe((data)=>
          { 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully retrieve all posts.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }

  addComment(data){
    return new Promise<string>(resolve => {
      return this.http.post<any>(this.ROOT_URL + '/post/AddNewComment', data).subscribe((data)=>{ 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully added a comment.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }
  
  getPost(postId){
    return new Promise<string>(resolve => {
      return this.http.get(this.ROOT_URL + '/post/GetPost?postId=' + postId).subscribe((data)=>
          { 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully retrieved post.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }

  getNewPostId(postCount){
    return new Promise<string>(resolve => {
      return this.http.get(this.ROOT_URL + '/post/GetNewPostId?id=' + postCount).subscribe((data)=>
          { 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully retrieved new post id.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }

  


}
