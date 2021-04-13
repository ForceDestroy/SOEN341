import { Component, OnInit } from '@angular/core';
import { HeaderService } from './header.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  userId: string;
  searchQuery: string;
  users: any = [];

  constructor(private headerService: HeaderService) {
    this.userId = localStorage.getItem('userId');
   }

  ngOnInit(): void {
  }

  search(result){
    //Checks if result string is empty
    if(result !== ""){
      this.headerService.searchUsers(result).then((data)=>{
        let searchData = JSON.parse(data);
        this.users = searchData.data;
        console.log(this.users);
      })
    }
    else{
      console.log('empty string');
    }
    
  }

}
