import Category from "./category";

export default interface PostCategory {
  categoryId: number;
  postId: number;
  category: Category;
}
