import { Category } from "./category";

export interface PostCategory {
  categoryId: number;
  postId: number;
  category: Category;
}
