import { User } from "./user";
import { Comment } from "./comment";
import { PostCategory } from "./postCategory";
import { Rating } from "./rating";

export interface Post {
  id: number;
  userId: number;
  date: string;
  title: string;
  content: string;
  user: User;
  comments: Comment[];
  postCategories: PostCategory[];
  ratings: Rating[];
}
