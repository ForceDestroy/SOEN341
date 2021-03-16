import { stringify } from '@angular/compiler/src/util';
import { Injectable } from '@angular/core';
//import { ExecOptionsWithStringEncoding } from 'child_process';
import { of, Subject } from 'rxjs';
//import { User } from './user';
//import { UsersController } from '../../../../Server/Controllers/UsersController.cs'

 export interface User {
   userId: string;
   password: string;
 }

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private user$ =  new Subject<User>();

  constructor () {}

    login(userId: string, password: string){
      const loginCredentials = {userId, password};
      console.log('login credentials', loginCredentials);
      //CheckLogin( userId,  password)
      return of(loginCredentials);

  }
  get user() {
    return this.user$.asObservable();
  }

  register (user:any) {
    //make api call to dav in database
    //upodate user sunject
    this.user$.next(user);
    //console.log('registered user successfully', user);
    return of(user);
  }

  private setUser(user)
 {
   this.user$.next(user);
   }


}
