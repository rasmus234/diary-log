﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DiaryLogApi.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        public virtual Post Post { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}