import { stringify } from '@angular/compiler/src/util';
import { Injectable } from '@angular/core';
import { of, Subject } from 'rxjs';
import { User} from './user';


@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private user$ = new Subject<User>();
  constructor () {}

    login(userId: string, password: string){
      const loginCredentials = {userId, password};
      console.log('login credentials', loginCredentials);
      return of(loginCredentials);

  }
  get user() {
    return this.user$.asObservable();
  }

  register (user:any) {
    this.setUser(user);
    console.log('registered user successfully', user);

    return of(user);
  }
  private setUser(user){
    this.user$.next(user);
  }
}
