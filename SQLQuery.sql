create database LibraryDb
on primary
(name = LibraryDb_data,
filename = 'M:\Simplilearn\mphasis\Phase-2\day-7\Assign7\LibraryDb_data.mdf')
-----------
use LibraryDb
------------
Create table Books
(
    BookId int identity(1,1) primary key,
    Title nvarchar(100),
    Author nvarchar(100),
    Genre nvarchar(100),
    Quantity int
)

insert into Books (Title, Author, Genre, Quantity)
values ('One Piece', 'Echira Oda', 'Adventure', 10), ( 'Rocklee', 'Mang Lee', 'Fiction', 15), 
('Parsh', 'Nikola', 'Science Fiction', 12), ('Pride', 'Robin', 'Romance', 8), ('The Great King', 'Degaa', 'Fiction', 20)

select * from Books

