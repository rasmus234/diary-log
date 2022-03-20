import {Injectable} from '@angular/core';
import Post from "./_models/post";
import {apiUrl} from "./app.component";
import {HttpClient} from "@angular/common/http";
import {AuthService} from "./_services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  posts: Post[] = []

  constructor(private readonly _http: HttpClient, private readonly _authService: AuthService) {
  }

  refresh(): void {
    this._http.get<Post[]>(`${apiUrl}/posts`).subscribe(posts => {
      this.posts = posts
    });
  }

  post(post: Post): void {
    post.userId = this._authService.userId();

    this._http.post<Post>(`${apiUrl}/posts`, post).subscribe(post => {
      this.posts.push(post)
    })
  }
}
