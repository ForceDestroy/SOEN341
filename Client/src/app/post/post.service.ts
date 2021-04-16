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
  getImageLink(data){
		return new Promise<string>(resolve => {
      return this.http.post<any>(this.ROOT_URL + '/post/GetImageLink', data).subscribe((data)=>
          { 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully retrieved image link.", "data":data}));
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

  addLikeReaction(data){
    return new Promise<string>(resolve => {
      return this.http.post<any>(this.ROOT_URL + '/post/AddLikeReaction?userId=' + data.userId + '&postId=' + data.postId, {}).subscribe((data)=>{ 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully liked post.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }

  removeLikeReaction(data){
    return new Promise<string>(resolve => {
      return this.http.delete<any>(this.ROOT_URL + '/post/RemoveLikeReaction?userId=' + data.userId + '&postId=' + data.postId, {}).subscribe((data)=>{ 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully un-liked post.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }
  addHeartReaction(data){
    return new Promise<string>(resolve => {
      return this.http.post<any>(this.ROOT_URL + '/post/AddHeartReaction?userId=' + data.userId + '&postId=' + data.postId, {}).subscribe((data)=>{ 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully hearted post.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }

  removeHeartReaction(data){
    return new Promise<string>(resolve => {
      return this.http.delete<any>(this.ROOT_URL + '/post/RemoveHeartReaction?userId=' + data.userId + '&postId=' + data.postId, {}).subscribe((data)=>{ 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully removed Heart on post.", "data":data}));
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
