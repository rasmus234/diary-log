import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  hide = true;

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get("https://localhost:7112/api/Posts").subscribe(value => console.log(value));
  }

}
