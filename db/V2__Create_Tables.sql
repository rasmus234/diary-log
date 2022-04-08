use DiaryLog

go

create table [User]
(
    id       int identity
        constraint PK__User__3213E83F1FC15BDB
            primary key,
    password nvarchar(30) not null,
    name     nvarchar(60) not null,
    username nvarchar(30) not null
)
go

create table Category
(
    id            int identity
        constraint PK__Category__3213E83F99D09204
            primary key,
    user_id       int          not null
        constraint FK_Category_User
            references [User]
            on delete cascade,
    category_name nvarchar(30) not null
)
go

create table Post
(
    id      int identity
        constraint PK__Post__3213E83F0F945DE7
            primary key,
    user_id int            not null
        constraint FK_Post_User
            references [User]
            on delete cascade,
    date    datetime       not null,
    title   nvarchar(50)   not null,
    content nvarchar(3600) not null
)
go

create table Comment
(
    id      int identity
        constraint PK__Comment__3213E83F46E8C864
            primary key,
    post_id int           not null
        constraint FK_Comment_Post
            references Post,
    user_id int           not null
        constraint FK_Comment_User
            references [User]
            on delete cascade,
    content nvarchar(300) not null
)
go

create table PostCategory
(
    category_id int not null
        constraint FK_PostCategory_Category
            references Category
            on delete cascade,
    post_id     int not null
        constraint FK_PostCategory_Post
            references Post,
    constraint PK_PostCategory
        primary key (category_id, post_id)
)
go

create table Rating
(
    user_id int not null
        constraint FK_Rating_User
            references [User]
            on delete cascade,
    post_id int not null
        constraint FK_Rating_Post
            references Post,
    is_like bit,
    constraint PK__Rating__CA534F790EB06E18
        primary key (user_id, post_id)
)
go

create unique index User_username_uindex
    on [User] (username)
go

create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go