import { Injectable } from '@angular/core';
import {CookieService} from "ngx-cookie-service";
import {HttpClient} from "@angular/common/http";
import {apiUrl} from "../app.component";
import {firstValueFrom} from "rxjs";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private readonly _cookieService: CookieService, private readonly _http: HttpClient, private readonly _router: Router) {
  }

  async login(username: string, password: string): Promise<void> {
    firstValueFrom(this._http.post(`${apiUrl}/authentication`, {username: username, password: password})).then(async (token: any) => {
      this._cookieService.set("diary-log-jwt", token.token)
      await this._router.navigate(['/']);
    });
  }

  async logout(): Promise<void> {
    this._cookieService.delete("diary-log-jwt");
    await this._router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return this._cookieService.check("diary-log-jwt");
  }

  userId(): number {
    return 1;
  }
}
