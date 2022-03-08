import User from "./user";
import PostCategory from "./postCategory";
import Rating from "./rating";

export default interface Post {
  id?: number;
  userId?: number;
  date?: Date;
  title?: string;
  content?: string;
  user?: User;
  comments?: Comment[];
  postCategories?: PostCategory[];
  ratings?: Rating[];
}
