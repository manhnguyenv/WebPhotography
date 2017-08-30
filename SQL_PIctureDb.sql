
CREATE DATABASE PictureDb;
GO
USE PictureDb;
GO



CREATE TABLE Picture(
PictureId int IDENTITY(1,1) not null PRIMARY KEY,
PictureName nvarchar(50) not null,
PictureDescription nvarchar(100) null,
PictureType nvarchar(20) not null,
PictureBin varbinary(max) not null,
PictureCreationTime date not null
);


