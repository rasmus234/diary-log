import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loggedIn: boolean = false

  constructor(private http: HttpClient) { }

  async isLoggedIn(): Promise<boolean> {
    const res = this.http.get('https://diary-log-easv.herokuapp.com/api/Authentication').toPromise();
    await res.then(() => {
      this.loggedIn = true;

      console.log('logged in')
    }).catch(() => {
      this.loggedIn = false;

      console.log('logged out')
    })

    /*
    const res = await this.http.get('https://diary-log-easv.herokuapp.com/api/Authentication').subscribe(
      async res => {
        this.loggedIn = true;

        console.log('logged in')
      },
      async err => {
        this.loggedIn = false;

        console.log('logged out')
      }).;
     */

    console.log('test ' + this.loggedIn)

    return this.loggedIn;
  }
}
