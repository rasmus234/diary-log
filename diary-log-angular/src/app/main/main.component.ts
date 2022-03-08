import {Component, OnInit} from '@angular/core';
import Post from "../_models/post";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  posts?: Post[]
  currentPost: Post = {}

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get<Post[]>('https://diary-log-easv.herokuapp.com/api/Posts').subscribe(posts => {
      this.posts = posts;
    });
  }

  handlePost() {
    this.currentPost.userId = 1;

    this.http.post<Post>('https://diary-log-easv.herokuapp.com/api/Posts', this.currentPost).subscribe(post => {
      this.posts?.push(post)
    });
  }
}
