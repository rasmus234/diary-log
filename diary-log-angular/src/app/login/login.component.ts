import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {AuthService} from "../_services/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: { username: string, password: string };
  hide: boolean;

  constructor(private readonly _authService: AuthService, private readonly _router: Router) {
    this.user = {
      username: '',
      password: ''
    }

    this.hide = true;
  }

  ngOnInit(): void {
  }

  async handleLogin() {
    this._authService.login(this.user.username, this.user.password).then(async () => {
      await this._router.navigate(['/']);
    });
  }
}
