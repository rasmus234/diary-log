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

  constructor(private http: HttpClient, public postsService:PostsService) { }


  currentPost: Post = {}

  ngOnInit(): void {
    this.http.get<Post[]>(`${apiUrl}/Posts`).subscribe(posts => {
      this.postsService.posts = posts
    })
  }

  handlePost(){
    console.log(this.currentPost)
    this.currentPost.userId = 1
    this.http.post<Post>(`${apiUrl}/Posts`, this.currentPost).subscribe(post => {
      this.postsService.posts.push(post)
    })
  }
}
