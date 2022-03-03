using AutoMapper;
using DiaryLogDomain;
using DiaryLogDomain.DTOs;

namespace DiaryLogApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Rating, RatingDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<PostCategory, PostCategoryDto>().ReverseMap();
        CreateMap<CreatePostDto, Post>().ReverseMap();
        CreateMap<CreatePostDto, PostDto>().ReverseMap();
    }
}