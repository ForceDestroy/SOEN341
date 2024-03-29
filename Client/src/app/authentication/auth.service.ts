import { Injectable } from '@angular/core';
import { of, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';


export interface User {
  userId: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  ROOT_URL: string = 'https://localhost:44318';

  private user$ = new Subject<User>();
  subscribe: any;

  constructor(private http: HttpClient) {}

  createUser(data) {
    return new Promise<string>((resolve) => {
      return this.http
        .post<any>(this.ROOT_URL + '/user/CreateNewUser', data)
        .subscribe(
          (data) => {
            resolve(
              JSON.stringify({
                returnCode: 'true',
                responseText: 'Successfully added User.',
                data: data,
              })
            );
          },
          (err) => {
            console.log(err);
            resolve(
              JSON.stringify({
                returnCode: 'false',
                responseText: 'There seems to be an issue with the server.',
              })
            );
          }
        );
    });
  }

  get user() {
    return this.user$.asObservable();
  }

  register(user: any) {
    //make api call to dav in database
    //upodate user sunject
    this.user$.next(user);
    //console.log('registered user successfully', user);
    return of(user);
  }

  private setUser(user) {
    this.user$.next(user);
  }

  getUserId(userId) {
    return new Promise<string>((resolve) => {
      return this.http
        .get(this.ROOT_URL + '/auth/GetUserId?id=' + userId)
        .subscribe(
          (data) => {
            resolve(
              JSON.stringify({
                returnCode: 'true',
                responseText: 'successfully retrieve user id.',
                data: data,
              })
            );
          },
          (err) => {
            console.log(err);
            resolve(
              JSON.stringify({
                returnCode: 'false',
                responseText: 'There seems to be an issue with the server.',
              })
            );
          }
        );
    });
  }

  getPassword(password) {
    return new Promise<string>((resolve) => {
      return this.http
        .get(this.ROOT_URL + '/auth/GetPassword?id=' + password)
        .subscribe(
          (data) => {
            resolve(
              JSON.stringify({
                returnCode: 'true',
                responseText: 'successfully retrieve password.',
                data: data,
              })
            );
          },
          (err) => {
            console.log(err);
            resolve(
              JSON.stringify({
                returnCode: 'false',
                responseText: 'There seems to be an issue with the server.',
              })
            );
          }
        );
    });
  }

  getNewUsernamePassword(data) {
    return new Promise<string>((resolve) => {
      return this.http
        .post(
          this.ROOT_URL +
            `/user/checkLogin?username=${data.username}&password=${data.password}`,
          data
        )
        .subscribe(
          (data) => {
            resolve(
              JSON.stringify({
                returnCode: 'true',
                responseText: 'successfully retrieve userId and password.',
                data: data,
              })
            );
          },
          (err) => {
            console.log(err);
            resolve(
              JSON.stringify({
                returnCode: 'false',
                responseText: 'There seems to be an issue with the server.',
              })
            );
          }
        );
    });
  }
}
