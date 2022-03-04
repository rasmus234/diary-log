import { Component, OnInit } from '@angular/core';
import {AuthService} from "../_auth/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';
  hide = true;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  onLogin(): void {
    this.authService.login(this.username, this.password);
  }
}
