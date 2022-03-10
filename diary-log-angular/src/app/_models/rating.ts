export default interface Rating {
  userId: number;
  postId: number;
  isLike: boolean | null;
}
