using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryLogApi;
using DiaryLogApi.Controllers;
using DiaryLogDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DiaryLogApiTests;

public class UnitTest1
{
    PostsController _controller;
    IMapper _mapper;

    public UnitTest1()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        var connectionString = "Data Source=ralle.database.windows.net;Initial Catalog=DiaryLog;Persist Security Info=True;User ID=rasmus234;Password=rasmuS123";
        var dbContext = new DiaryLogContext(new DbContextOptions<DiaryLogContext>());
        _mapper = config.CreateMapper();
        _controller = new PostsController(dbContext,_mapper);
    }
    [Fact]
    public async Task Test1()
    {
        var posts = await _controller.GetPosts();
        
        Assert.Null(posts.Result);
    }
}