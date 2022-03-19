import {Component, Input, OnInit} from '@angular/core';
import Post from "../_models/post";
import {HttpClient} from "@angular/common/http";
import {apiUrl} from "../app.component";
import {PostsService} from "../posts.service";

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  constructor(private http: HttpClient, private postsService: PostsService) {
  }

  @Input()
  post?: Post

  ngOnInit(): void {
  }

  delete() {
    this.http.delete(`${apiUrl}/Posts/${this.post?.id}`).subscribe({
      complete: () => this.postsService.posts = this.postsService.posts.filter(post => post.id !== this.post?.id),
      error: err => console.error(err)
    })
  }

}
