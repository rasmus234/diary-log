import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'diary-log-angular';
}

const localUrl = 'https://localhost:7112/api';
const prodUrl = 'http://20.123.8.20:8060/api';

export const apiUrl = prodUrl;
