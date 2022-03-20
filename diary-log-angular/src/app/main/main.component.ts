import {Component, OnInit} from '@angular/core';
import Post from "../_models/post";
import {PostsService} from "../posts.service";
import {AuthService} from "../_services/auth.service";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  currentPost: Post = {}

  constructor(public readonly _postsService: PostsService, private readonly _authService: AuthService) {
  }

  ngOnInit(): void {
    this._postsService.refresh();
  }

  handlePost(): void {
    this._postsService.post(this.currentPost);
  }

  async handleLogout(): Promise<void> {
    await this._authService.logout();
  }
}
