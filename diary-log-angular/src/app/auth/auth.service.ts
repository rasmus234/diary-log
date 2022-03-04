import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

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
    }).catch(() => {
      this.loggedIn = false;
    })

    return this.loggedIn;
  }
}
