import { Component, OnInit } from '@angular/core';
import {Post} from "../models/post";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(private http: HttpClient) { }

  posts?: Post[]
  currentPost: Post = {}

  ngOnInit(): void {
    this.http.get<Post[]>('https://diary-log-easv.herokuapp.com/api/Posts').subscribe(posts => {
      this.posts = posts
    })
  }

  submitPost(){
    console.log(this.currentPost)
    this.currentPost.userId = 1
    this.http.post<Post>('https://diary-log-easv.herokuapp.com/api/Posts', this.currentPost).subscribe(post => {
      this.posts?.push(post)
    })
  }
}
