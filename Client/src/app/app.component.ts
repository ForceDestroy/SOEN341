import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Client';

  ROOT_URL: string = "https://localhost:44318";

  constructor(private http: HttpClient){}

  ngOnInit(){
    this.http.get(this.ROOT_URL + '/db/getAll').subscribe((data)=>{
      localStorage.setItem('username', data[2].username)
      localStorage.setItem('userId', data[2].userId)
      localStorage.setItem('profilePicture', data[2].profilePicture)
    })
  }
}
