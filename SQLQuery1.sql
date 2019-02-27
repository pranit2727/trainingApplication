use [WebApi]
go

create table RoomDetails(
RoomId int identity primary key,
RoomName nvarchar(50) not null,
CreatedAt datetime,
UpdatedAt datetime,
DeletedAt datetime
)
go

drop table RoomDetails
go

create table Schedules(
ScheduleId int identity primary key,
StartTime datetime ,
EndTime datetime,
RoomId int references RoomDetails(RoomId)
)
go 

drop table Schedules
go


create table Roles(
RoleId int identity primary key,
RoleName nvarchar(50) not null
)
go

drop table Roles
go

create table Users(
UserId int identity primary key,
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null,
IsActive bit,
RoleId int references Roles(RoleId),
CreatedAt datetime,
UpdatedAt datetime,
DeletedAt datetime
)
go

drop table Users
go

create table UserCredentials(
UserCreadID int identity primary key,
Email nvarchar(50) unique,
Password nvarchar(50) not null,
IsEmailVerified bit,
ActivationCode uniqueidentifier,
IsActivated bit,
UserId int references Users(UserId)
)
go

drop table UserCredentials
go


create table Trainings(
TrainingId int identity primary key,
Topic nvarchar(50) not null,
Description nvarchar(200) not null,
ScheduleId int references Schedules(ScheduleId) ,
UserId int references Users(UserId),
CreatedAt datetime,
UpdatedAt datetime,
DeletedAt datetime
)
go


drop table Trainings
go

create table TrainingsAttendees(
Id int identity primary key,
TrainingId int references Trainings(TrainingId),
UserId int references Users(UserId),
CreatedAt datetime,
UpdatedAt datetime,
DeletedAt datetime
)
go

drop table TrainingsAttendees
go

create table Meetings(
MeetingId int identity primary key,
MeetingName nvarchar(200) not null,
Agenda nvarchar(300) not null,
ScheduleId int references Schedules(ScheduleId) ,
UserId int references Users(UserId),
CreatedAt datetime,
UpdatedAt datetime,
DeletedAt datetime
)
go




create table MeetingsAttendees(
Id int identity primary key,
MeetingId int references Meetings(MeetingId),
UserId int references Users(UserId),
CreatedAt datetime,
UpdatedAt datetime,
DeletedAt datetime
)
go

drop table MeetingsAttendees
go