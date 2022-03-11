import { Injectable } from '@angular/core';
import Post from "./_models/post";

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  posts: Post[] = []
  constructor() { }
}
