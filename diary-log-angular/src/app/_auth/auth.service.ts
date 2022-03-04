import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, map, of, timeout} from "rxjs";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn: boolean;

  constructor(private http: HttpClient, private router: Router) {
    this.isLoggedIn = false;
  }

  login(username: string, password: string) {
    this.http.post<string>('https://diary-log-easv.herokuapp.com/api/authentication', {
      username: username,
      password: password
    }).pipe(map(jwt => {
      localStorage.setItem('jwt', jwt);
      this.isLoggedIn = true;
      this.router.navigateByUrl('/');
    }), catchError(() => of(null))).subscribe();
  }

  logout() {
    localStorage.removeItem('jwt')
    this.isLoggedIn = false;
    this.router.navigateByUrl('/login');
  }
}
