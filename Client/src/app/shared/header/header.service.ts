import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  ROOT_URL: string = "https://localhost:44318"

  constructor(private http: HttpClient) { }

  searchUsers(result){
		return new Promise<string>(resolve => {
      return this.http.get(this.ROOT_URL + '/profile/SearchUsers?username=' + result).subscribe((data)=>
          { 	
            resolve(JSON.stringify({"returnCode": "true","responseText": "successfully retrieve all posts.", "data":data}));
          },(err)=>{
            console.log(err);
            resolve(JSON.stringify({"returnCode": "false","responseText": "There seems to be an issue with the server."}));
          });		
    })
  }
}
