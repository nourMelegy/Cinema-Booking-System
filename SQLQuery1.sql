--Creation of hall table 
create table Hall (
HallNumber int NOT NULL primary key,
HallCapacity int );

--creation of displayed-in
create table Displayed_in (
MOEIDR VARCHAR(20),
HNumber int,
foreign key (MOEIDR) References Movie (EIDR),
foreign key (HNumber) References Hall (HallNumber),
primary key(MOEIDR,HNumber));


--Insertion into halls table 
Insert into Hall
values(1, 10),
      (2, 20),
      (3, 30),
      (4, 40),
      (5, 50),
      (6, 60),
      (7, 70),
      (8, 80),
      (9, 90),
      (10,100);


--Insertion into Hall-Movie table (Display-in)
Insert into Displayed_in
values('EIDR-1', 1),
      ('EIDR-2', 1),
      ('EIDR-2', 2),
      ('EIDR-3', 3),
      ('EIDR-4', 4),
      ('EIDR-5', 5),
      ('EIDR-6', 6),
      ('EIDR-7', 7),
      ('EIDR-8', 8),
      ('EIDR-9', 9),
      ('EIDR-10',10);


--Query part
create procedure GetMoviesInHall
    @HallNumber int
AS
BEGIN
select Movie.EIDR, Movie.Title, Movie.Duration, Movie.Price, Movie.TimeSlots, Movie.AgeRating
from Movie
join Displayed_in on Displayed_in.MOEIDR = movie.EIDR 
where Displayed_in.HNumber = @HallNumber
END;

--To call
EXEC GetMoviesInHall @HallNumber = 1; --1 is for example


--Stored procedure part
create Function CheckHallCapacity
    (@HallNumberParameter int)
    Returns int
AS
BEGIN
Declare @ReturnHallCapacity int
select @ReturnHallCapacity = HallCapacity
from Hall
where HallNumber = @HallNumberParameter
return @ReturnHallCapacity;
END;

--To call 
select CheckHallCapacity(1) AS CapacityForRequiredHall; --1 is for example