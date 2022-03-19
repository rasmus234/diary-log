import { Injectable } from '@angular/core';
import {CookieService} from "ngx-cookie-service";
import {HttpClient} from "@angular/common/http";
import {apiUrl} from "../app.component";
import {firstValueFrom} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Todo: Remove follow variable:
  placeholder: undefined;

  constructor(private readonly _cookieService: CookieService, private readonly _http: HttpClient) {
  }

  async login(username: string, password: string): Promise<void> {
    await firstValueFrom(this._http.post(`${apiUrl}/authentication`, {username: username, password: password}));
  }

  logout(): void {
    this._cookieService.delete("diary-log-jwt");
  }

  isLoggedIn(): boolean {
    return this._cookieService.check("diary-log-jwt");
  }
}
