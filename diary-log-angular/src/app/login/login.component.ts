import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  hide = true;
  username = '';
  password = '';

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  onLogin() {
    console.log(this.username)

    this.http.post<any>('https://diary-log-easv.herokuapp.com/api/Authentication', {
      username: this.username,
      password: this.password
    }).subscribe(res => {
      localStorage.setItem('token', res.token);
      this.router.navigateByUrl('/');
    });
  }
}
