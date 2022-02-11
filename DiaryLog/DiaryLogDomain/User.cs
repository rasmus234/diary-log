﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiaryLogDomain;

public partial class User
{
    public User()
    {
        Categories = new HashSet<Category>();
        Comments = new HashSet<Comment>();
        Posts = new HashSet<Post>();
        Ratings = new HashSet<Rating>();
    }

    public int Id { get; set; }
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;


    public virtual ICollection<Category> Categories { get; set; }
 
    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
  
    public virtual ICollection<Rating> Ratings { get; set; }
}