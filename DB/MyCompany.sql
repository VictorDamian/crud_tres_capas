create database MyCompany
go
use MyCompany
go

create table Users(
UserID int identity(1,1) primary key,
LoginName nvarchar (100) unique not null,
Password nvarchar (100) not null,
FirstName nvarchar (100) not null,
LastName nvarchar (100) not null,
Position nvarchar (100) not null,
Email nvarchar (150) not null
)
go

create table Employee(
IdPK int identity(1,1) primary key, 
IdNumber varchar (10) unique not null,
Name nvarchar(80) not null,
Mail nvarchar(150)not null,
Birthday date not null 
)
go

create table trabajos(
id int identity(1,1) primary key,
nom varchar(80)not null,
num varchar(20)not null,
dire varchar(50)not null,
tra varchar(max)not null,
anti varchar(20)not null,
sal varchar(20)not null,
total int,
fecha varchar(max)not null
)

insert into trabajos values('vic','4556','mena','silla','12','10','22','24/10/2010')

insert into Employee values ('6587542361','Juan Gabriel','Juan@gmail.com','24/05/2000')
insert into Employee values ('6587542362','Gonazalo','Gonazalo@gmail.com','24/05/2000')
insert into Employee values ('6587542363','Marta','Marta@gmail.com','24/05/2000')
insert into Employee values ('6587542364','Lisandro','Lisandro@gmail.com','24/05/2000')
insert into Employee values ('6587542365','Anna Maria','Anna@gmail.com','24/05/2000')

insert into Users values ('dantes','1234','Victor Manuel','Damian','Administrador','vim@gmail.com')
insert into Users values ('ashen','1234','Victor Manuel','Damian','Administrador','vim15damian@gmail.com')
select *from Employee
select*from Users
