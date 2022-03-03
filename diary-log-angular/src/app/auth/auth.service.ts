import {Injectable} from '@angular/core';
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn = false;

  constructor(private router: Router) {
  }

  onLogin() {
    this.isLoggedIn = true;

    this.router.navigate(['/']);
  }

  onLogout() {
    this.isLoggedIn = false;

    this.router.navigate(['/login']);
  }
}
