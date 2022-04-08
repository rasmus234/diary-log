CREATE DATABASE DiaryLog;

go

USE DiaryLog;

go

CREATE TABLE [user]
  (
     id       INT IDENTITY CONSTRAINT pk__user__3213e83f1fc15bdb PRIMARY KEY,
     password NVARCHAR(30) NOT NULL,
     NAME     NVARCHAR(60) NOT NULL,
     username NVARCHAR(30) NOT NULL
  )

go

CREATE TABLE category
  (
     id            INT IDENTITY CONSTRAINT pk__category__3213e83f99d09204
     PRIMARY KEY,
     user_id       INT NOT NULL CONSTRAINT fk_category_user REFERENCES [user] ON
     DELETE
     CASCADE,
     category_name NVARCHAR(30) NOT NULL
  )

go

CREATE TABLE post
  (
     id      INT IDENTITY CONSTRAINT pk__post__3213e83f0f945de7 PRIMARY KEY,
     user_id INT NOT NULL CONSTRAINT fk_post_user REFERENCES [user] ON DELETE
     CASCADE,
     date    DATETIME NOT NULL,
     title   NVARCHAR(50) NOT NULL,
     content NVARCHAR(3600) NOT NULL
  )

go

CREATE TABLE comment
  (
     id      INT IDENTITY CONSTRAINT pk__comment__3213e83f46e8c864 PRIMARY KEY,
     post_id INT NOT NULL CONSTRAINT fk_comment_post REFERENCES post,
     user_id INT NOT NULL CONSTRAINT fk_comment_user REFERENCES [user] ON DELETE
     CASCADE,
     content NVARCHAR(300) NOT NULL
  )

go

CREATE TABLE postcategory
  (
     category_id INT NOT NULL CONSTRAINT fk_postcategory_category REFERENCES
     category
     ON DELETE
     CASCADE,
     post_id     INT NOT NULL CONSTRAINT fk_postcategory_post REFERENCES post,
     CONSTRAINT pk_postcategory PRIMARY KEY (category_id, post_id)
  )

go

CREATE TABLE rating
  (
     user_id INT NOT NULL CONSTRAINT fk_rating_user REFERENCES [user] ON DELETE
     CASCADE,
     post_id INT NOT NULL CONSTRAINT fk_rating_post REFERENCES post,
     is_like BIT,
     CONSTRAINT pk__rating__ca534f790eb06e18 PRIMARY KEY (user_id, post_id)
  )

go

CREATE UNIQUE INDEX user_username_uindex
  ON [User] (username)

go

CREATE TABLE __efmigrationshistory
  (
     migrationid    NVARCHAR(150) NOT NULL CONSTRAINT pk___efmigrationshistory
     PRIMARY
     KEY,
     productversion NVARCHAR(32) NOT NULL
  )

go 