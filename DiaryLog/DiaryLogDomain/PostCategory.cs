﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiaryLogDomain;

public partial class PostCategory
{
    public int CategoryId { get; set; }
    public int PostId { get; set; }

    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;
    [JsonIgnore]
    public virtual Post Post { get; set; } = null!;
}