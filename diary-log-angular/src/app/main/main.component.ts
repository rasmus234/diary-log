import { Component, OnInit } from '@angular/core';
import Post from "../_models/post";
import {HttpClient} from "@angular/common/http";
import {PostsService} from "../posts.service";
import {apiUrl} from "../app.component";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  constructor(private readonly _http: HttpClient, public readonly _postsService:PostsService) { }

  currentPost: Post = {}

  ngOnInit(): void {
    this._http.get<Post[]>(`${apiUrl}/posts`).subscribe(posts => {
      this._postsService.posts = posts
    })
  }

  handlePost(){
    console.log(this.currentPost)
    this.currentPost.userId = 1
    this._http.post<Post>(`${apiUrl}/posts`, this.currentPost).subscribe(post => {
      this._postsService.posts.push(post)
    })
  }
}
