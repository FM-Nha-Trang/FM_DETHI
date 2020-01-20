create database testdsc;
use testdsc;
dbcc checkident(tests,reseed,1);
-- username:admin , password: admin12345
insert into users
VALUES
   ('admin', 'Ma', 'Hung', 22, N'Nữ', '7488e331b8b64e5794da3fa4eb10ad5d');
SET IDENTITY_INSERT tests ON;
insert into tests
   (test_code,user_create,date_create,title)
VALUES
   ('TU10979967', 1, '2020-01-20T08:05:00', 'DE KIEM TRA KET THUC HOC PHAN');
insert into questions
   (test_code,title,answer_A,answer_B,answer_C,answer_D,answer_true)
VALUES
   ('TU10979967', 'Hung: Make yourself at home. Bang:________________________', 'Not at all. Don’t mention it.', 'Yes. Can I help you?', 'That’s very kind. Thank you ', 'Thanks! Same to you', 'c');
insert into questions
   (test_code,title,answer_A,answer_B,answer_C,answer_D,answer_true)
VALUES
   ('TU10979967', 'Hung: I got 8.0/9.0 for the IELTS test! Bang:________________________', 'Not at all. Don’t mention it.', 'Good for you. Thank you', 'You can do it', 'Well done, son! I’m very proud of you', 'd');
insert into questions
   (test_code,title,answer_A,answer_B,answer_C,answer_D,answer_true)
VALUES
   ('TU10979967', 'Hung: ________________________ Bang:I have a terrible headache.', 'What’s the problem to you?', 'What’s the matter with you?', 'What happens with you?', 'What causes you?', 'b');
insert into questions
   (test_code,title,answer_A,answer_B,answer_C,answer_D,answer_true)
VALUES
   ('TU10979967', 'Hung: I don’t think I can do this. Bang:________________________', 'Oh, come on. Give it a try. ', 'Yeah, it’s not easy.', 'No, I hope not. ', 'Sure, no way!', 'a');
insert into questions
   (test_code,title,answer_A,answer_B,answer_C,answer_D,answer_true)
VALUES
   ('TU10979967', 'Hung: I wonder if you could help me?. Bang:________________________', 'No, what is it? ', 'Really? How nice', 'Don‟t mention it.', 'I‟ll do my best.', 'd');\

