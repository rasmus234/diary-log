import {Injectable} from '@angular/core';
import Post from "./_models/post";
import {apiUrl} from "./app.component";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  posts: Post[] = []

  constructor(private readonly _http: HttpClient) {
  }

  refresh(): void {
    this._http.get<Post[]>(`${apiUrl}/posts`).subscribe(posts => {
      this.posts = posts
    });
  }

  post(post: Post): void {
    post.userId = 1;

    this._http.post<Post>(`${apiUrl}/posts`, post).subscribe(post => {
      this.posts.push(post)
    })
  }
}
