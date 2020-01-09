create database testdsc

use testdsc

create table user_infor
(
	id int identity(1,2) primary key,
	username varchar(15) not null,
	first_name varchar(20) not null ,
	last_name varchar(15) not null ,
	age smallint ,
	gender varchar(5) check( gender = N'Nam' or gender = N'Nữ'), 
)
create table tests
(
	id int identity(1,3) ,
	test_code varchar(15) primary key,
	user_create int ,
	date_create datetime not null ,
	title nvarchar(255) ,
)
create table questions
(
	id int identity(1,5) ,
	test_code varchar(15) ,
	question_code varchar(15) primary key,
	title nvarchar(255) not null ,
)
create table answer
(
	id int identity(1,10) ,
	answer_code varchar(15) primary key,
	title nvarchar(255) not null ,
)
create table questions_answer
(
	id int identity(1,8) ,
	question_code varchar(15) primary key,
	answer_list varchar(50) not null ,
	answer_true varchar(15) not null ,
)
create table history_answer
(
	id int identity(1,2) primary key,
	userid int ,
	test_code varchar(15) ,
	date_answer datetime not null ,
	point int not null ,
)

