CREATE DATABASE Kwizzotronic
go
use Kwizzotronic
go

create table Creator (
	IDCreator int identity primary key,
	FirstName varchar(255) not null,
	LastName varchar(255) not null,
	Email varchar(255) not null unique,
	Password varchar(255) not null
)

create table Quiz (
	IDQuiz int identity primary key,
	Title varchar(255) not null,
	CreatorId int foreign key references Creator(IDCreator)
)

create table QuizInstance (
	IDQuizInstance int identity primary key,
	CreatedAt datetime default current_timestamp,
	Code varchar(255) not null,
	NumberOfPlayers int,
	HasStarted int,
	QuizId int foreign key references Quiz(IDQuiz)
)

create table Nickname (
	IDNickname int identity primary key,
	Text varchar(255) not null,
	QuizInstanceId int foreign key references QuizInstance(IDQuizInstance)
)

create table Question (
	IDQuestion int identity primary key,
	Text varchar(255) not null,
	Answer1 varchar(255) not null,
	Answer2 varchar(255) not null,
	Answer3 varchar(255),
	Answer4 varchar(255),
	CorrectAnswer int not null,
	Time int,
	QuizId int foreign key references Quiz(IDQuiz)
)

create table RankList (
	IDRankList int identity primary key,
	Position int,
	Points int,
	Nickname varchar(255) not null,
	QuizInstanceId int foreign key references QuizInstance(IDQuizInstance)
)

go

------------------s

CREATE PROCEDURE createCreator
	@FirstName nvarchar(250),
	@LastName varchar(255),
	@Email varchar(255),
	@Password varchar(255),
	@Id int output
AS
insert into Creator values(@FirstName, @LastName, @Email, @Password)
set @Id = SCOPE_IDENTITY()

GO

CREATE PROCEDURE getCreatorWithId
	@Id int
AS
select * from Creator where IDCreator = @Id

GO

CREATE PROCEDURE getCreatorWithEmailAndPassword
	@Email nvarchar(250),
	@Password nvarchar(250)
AS
select * from Creator where Email = @Email and Password = @Password

GO


CREATE PROCEDURE UpdateCreator 
	@Id int,
	@FirstName nvarchar(250),
	@LastName varchar(255),
	@Email varchar(255),
	@Password varchar(255)
AS

update Creator
set FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password
where IDCreator = @Id
GO

-----------------------------

CREATE PROCEDURE createQuiz
	@Title nvarchar(250),
	@CreatorId int,
	@Id int output
AS
insert into Quiz values(@Title, @CreatorId)
set @Id = SCOPE_IDENTITY()
GO

CREATE PROCEDURE getQuizForUserWithId
	@CreatorId int
AS
select * from Quiz where CreatorId = @CreatorId

GO

CREATE PROCEDURE UpdateQuiz
	@Id int,
	@Title nvarchar(250),
	@CreatorId int
AS

update Quiz
set Title = @Title, CreatorId = @CreatorId
where IDQuiz = @Id
GO

CREATE PROCEDURE GetAllQuizes
AS
select * from Quiz
GO

---------------------------
CREATE PROCEDURE createQuizInstance
	@QuizId int,
	@Code varchar(255),
	@NumberOfPlayers int,
	@HasStarted int,
	@Id int output
AS
insert into QuizInstance values(GETDATE(), @Code, 0, 0, @QuizId)
set @Id = SCOPE_IDENTITY()
GO

CREATE PROCEDURE updateQuizInstance
	@QuizInstanceId int,
	@Code varchar(255),
	@NumberOfPlayers int,
	@HasStarted int
AS
update QuizInstance 
set Code = @Code, NumberOfPlayers = @NumberOfPlayers, HasStarted = @HasStarted
where IDQuizInstance = @QuizInstanceId
GO

CREATE PROCEDURE getQuizInstanceById
	@QuizInstanceId int
AS
select * from QuizInstance where IDQuizInstance = @QuizInstanceId
GO

CREATE PROCEDURE getQuizInstanceByCode
	@code nvarchar(250)
AS
select * from QuizInstance where Code = @code
GO

CREATE PROCEDURE getQuizInstanceWithQuizId
	@QuizId int
AS
select * from QuizInstance where QuizId = @QuizId
GO

CREATE PROCEDURE deleteQuizInstance
	@QuizInstanceId int
AS
delete from QuizInstance where IDQuizInstance = @QuizInstanceId
GO

--------------------------
CREATE PROCEDURE createNickname
	@Text nvarchar(250),
	@QuizInstanceId int,
	@Id int output
AS
insert into Nickname values(@Text, @QuizInstanceId)
set @Id = SCOPE_IDENTITY()
GO

CREATE PROCEDURE getNicknameWithQuizInstanceId
	@Text nvarchar(250),
	@QuizInstanceId int
AS
select * from Nickname where QuizInstanceId = @QuizInstanceId and Text = @Text
GO

--------------------------

CREATE PROCEDURE createQuestion
	@Text varchar(255),
	@Answer1 varchar(255),
	@Answer2 varchar(255),
	@Answer3 varchar(255),
	@Answer4 varchar(255),
	@CorrectAnswer int,
	@Time int,
	@QuizId int,
	@Id int output
AS
insert into Question values(@Text, @Answer1, @Answer2, @Answer3, @Answer4, @CorrectAnswer, @Time, @QuizId)
set @Id = SCOPE_IDENTITY()
GO

CREATE PROCEDURE getQuestionsForQuiz
	@QuizId int
AS
select * from Question where QuizId = @QuizId

GO

CREATE PROCEDURE UpdateQuestion
	@Id int,
	@Text varchar(255),
	@Answer1 varchar(255),
	@Answer2 varchar(255),
	@Answer3 varchar(255),
	@Answer4 varchar(255),
	@CorrectAnswer int,
	@Time int,
	@QuizId int
AS

update Question
set Text = @Text, Answer1 = @Answer1, Answer2 = @Answer2,
	Answer3 = @Answer3, Answer4 = @Answer4,
	CorrectAnswer = @CorrectAnswer, Time = @Time, QuizId = @QuizId
where IDQuestion = @Id
GO

-----------------------

CREATE PROCEDURE createRankList
	@Position int,
	@Points int,
	@Nickname varchar(255),
	@QuizInstanceId int,
	@Id int output
AS
insert into RankList values(@Position, @Points, @Nickname, @QuizInstanceId)
set @Id = SCOPE_IDENTITY()
GO

CREATE PROCEDURE getRankListForQuizInstance
	@QuizInstanceId int
AS
select * 
from RankList 
where QuizInstanceId = @QuizInstanceId
order by Position
GO

CREATE PROCEDURE UpdateRankList
	@Id int,
	@Position int,
	@Points int,
	@Nickname varchar(255),
	@QuizInstanceId int
AS

update RankList
set Position = @Position, Points = @Points, Nickname = @Nickname,
	QuizInstanceId = @QuizInstanceId
where IDRankList = @Id
GO

--------------------------


select * from Creator
select * from Quiz
select * from Question
select * from QuizInstance
select * from RankList
select * from Nickname

