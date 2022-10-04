create database LibraryManagement
go
use LibraryManagement

create table Accounts(
AccountID int identity(1000,1),
UserName varchar(60) unique,
AccountType varchar(30),
AccountPassword varchar(60),
CONSTRAINT PK_Accounts PRIMARY KEY (AccountID)
)


create table Members(
MemberID int identity(1000,1) primary key,
MemberUserName varchar(60) unique,
MemberName varchar(60),
MemberGender varchar(20),
MemberJoinedDate date,
MemberPhone varchar(30),
MemberEmail varchar(70),
MemberDOB date
)

create table Admins(
AdminID int identity(1000,1) primary key,
AdminUserName varchar(60) unique,
AdminName varchar(60),
AdminGender varchar(20),
AdminJoinedDate date,
AdminPhone varchar(30),
AdminEmail varchar(70),
AdminDOB date
)

create table Books(
BookTblID int identity(10000,1) primary key,
BookID varchar(40) unique,
BookTitle varchar(60),
BookCategory varchar(50),
BookAuthor varchar(70),
BookPublisher varchar(70),
BookISBN varchar(30),
BookCopyright varchar(30),
BookDateAdded date,
BookStatus varchar(20)
BookImg  varchar(80)
)

create table Transactions(
TranID int identity(1,1) primary key,
BookID varchar(40),
BookTitle varchar(60),
BookISBN varchar(30),
TranStatus varchar(30),
TranDate date,
MemberID int,
MemberName varchar(60)
)
go
insert into Admins values ('blen','Amanuel Dereje','Male',GETDATE(),'09112234567','aman.dereje@gmail.com',Getdate())
insert into Accounts values('blen','Librarian','passcode')


insert into Members values ('blen12','Amanuel Dereje','Male',GETDATE(),'09112234567','aman.dereje@gmail.com',Getdate())
insert into Accounts values('blen12','Memeber','passcode')

