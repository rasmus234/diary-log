import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: { username: string, password: string };
  hide: boolean;

  constructor(private router: Router) {
    this.user = {
      username: '',
      password: ''
    }

    this.hide = true;
  }

  ngOnInit(): void {
  }

  async handleLogin() {
    await this.router.navigateByUrl('/');
  }
}
