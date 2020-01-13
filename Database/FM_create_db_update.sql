create database testdsc
use testdsc
create table user_infor
(
id int identity(1,1) primary key ,
username varchar(15) not null,
first_name varchar(20) not null ,
last_name varchar(15) not null ,
age smallint ,
gender varchar(5) check( gender = N'boy' or gender = N'girl'), 
)
create table tests
(
id int identity(1,1) ,
test_code varchar(15) primary key,
user_create int ,
date_create datetime not null ,
title nvarchar(255) ,
  CONSTRAINT fk_tests_user_info
   FOREIGN KEY (id)
   REFERENCES user_infor (id),
)
create table questions
(
id int identity(1,1) primary key ,
test_code varchar(15) ,
title text not null ,
answer_A text not null,
answer_B text not null,
answer_C text not null,
answer_D text not null,
answer_true char(1) not null,
constraint fk_question_tests    FOREIGN KEY (test_code)
   REFERENCES [dbo].[tests] (test_code),
)

create table history_answer
(
id int identity  primary key,
userid int ,
test_code varchar(15) ,
date_answer datetime not null ,
point int not null ,
constraint fk_history_answer_user_info    FOREIGN KEY (userid)
   REFERENCES user_infor (id),
constraint fk_history_answer_tests    FOREIGN KEY (test_code)
   REFERENCES [dbo].[tests] (test_code),

)


