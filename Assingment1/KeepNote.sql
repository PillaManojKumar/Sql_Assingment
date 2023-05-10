Create Database KeepNote

Use KeepNote

Create Table User1
(
User_Id int primary key,
User_Name varchar(20),
User_Added_Date Datetime,
User_Password varchar(10),
User_Mobile int
)

Alter Table User1 
Alter Column User_Added_Date Date



Create Table Note
(
Note_Id int primary key,
Note_title varchar(20),
Note_content varchar(20),
Note_status varchar(20),
Note_creation_date Datetime,
User_Id int references User1(User_Id)
)

Alter Table Note
ALter Column Note_creation_date Date

Create Table Category
(
Category_Id int primary key,
Category_name varchar(20),
Category_descr varchar(20),
Category_creation_date Datetime,
Category_creator int references User1(User_Id)
)

Alter Table Category
ALter Column Category_creation_date Date

Create Table Reminder
(
Reminder_Id int primary key,
Reminder_name varchar(20),
Reminder_descr varchar(20),
Reminder_type varchar(20),
Reminder_creation_date Datetime,
Reminder_Creator int references User1(User_Id)
)
Alter Table Reminder
Alter Column Reminder_creation_date Date


Create Table NoteCategory
(
NoteCategory_Id int primary key,
Note_Id int references Note(Note_Id),
Category_Id int references Category(Category_Id)
)

Create Table NoteReminder
(
NoteReminder_Id int primary key,
Note_Id int references Note(Note_Id),
Reminder_Id int references Reminder(Reminder_Id)
)
delete from Note
Insert into User1 values(112233,'Maya','2019-01-02','Maya1214',8012345679),(112244,'Nair','2019-01-11','Welcome',9023778467)

select * from User1

insert into Note values 
(1,'Today Tasks', '1.Check emails and reply to them 2. start the pro','InProgress','2019-01-21',112233),
(2,'Traning to plan', '1.Application related 2.Technical related','Yet To Start','2019-01-31',112244),
(3,'Things to have today', '1.Fruits 2.More water','InProgress','2019-01-25',112244)

select * from Note

Insert into Category values(1,'offical','office related nots','2019-01-21',112233),(2,'diet','health related notes','2019-01-24',112244)

select * from Category

insert into Reminder values 
(1,'KT reminder', 'Session on technical queries','Office Reminders','2019-01-12',112233),
(2,'Personal reminder', 'Pick children','Personal Reminders','2019-01-14',112244)

select * from Reminder

Insert into NoteCategory values(1,1,1),
                               (2,2,1),
                               (3,3,2) 

select * from NoteCategory

Insert into NoteReminder values(1,3,2),
                               (2,2,1)

select * from NoteReminder

--1st query
Select * from User1 where User_Id = 112233 and User_Password = 'Maya1214'

--2nd query
Select * from Note where Note_creation_date = '01/31/2019'

--3rd query
select * from Category where Category_creation_date > '01/22/2019' 

--4th query
select * from Category where Category_creator = (select User_Id from Note where Note_Id = 1)

--5th query
select * from Note where User_Id = 112244

--6th query
select * from Note where User_Id = (select Category.Category_creator from Category where Category_Id = 1)

--7th query
select * from Reminder where Reminder_Creator = (select User_Id from Note where Note_Id = 2)

--8th query
update Note set Note_status = 'Completed' where Note_Id = 3
select * from Note

--9th query
update Reminder set Reminder_type = 'Personal Reminders' where Reminder_Creator = (select User_Id from Note where Note_Id = 1)
select * from Reminder

--10th query
delete from NoteReminder where Note_Id = 2 
select * from NoteReminder

