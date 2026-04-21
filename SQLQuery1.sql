--Creation of hall table 
create table Hall (
HallNumber int NOT NULL primary key,
HallCapacity int NOT NULL CHECK (HallCapacity >0)
);


------------------------------------------------------------------------------------------------------------
-- From Fatema code 
CREATE TABLE Movie (
    EIDR       VARCHAR(20)    PRIMARY KEY,
    Title      VARCHAR(100)   NOT NULL,
    Duration   INT            NOT NULL,
    Price      DECIMAL(8, 2)  NOT NULL,
    TimeSlots  VARCHAR(100),
    AgeRating  VARCHAR(10)    NOT NULL,
    CONSTRAINT CK_Price     CHECK (Price > 0),
    CONSTRAINT CK_Duration  CHECK (Duration > 0),
    CONSTRAINT CK_AgeRating CHECK (AgeRating IN ('G', 'PG', 'PG-13', 'R', '+13', '+18'))
);
------------------------------------------------------------------------------------------------------------

--creation of displayed-in
create table Displayed_in (
MOEIDR VARCHAR(20) NOT NULL,
HNumber int NOT NULL,
foreign key (MOEIDR) References Movie (EIDR),
foreign key (HNumber) References Hall (HallNumber),
primary key(MOEIDR,HNumber));





------------------------------------------------------------------------------------------------------------
--Fatema part
Insert into Movie values
('EIDR-1',  'The Beginning',    120, 12.99, '10:00 AM', 'PG'),
('EIDR-2',  'Dark Waters',      95,  8.99,  '12:00 PM', 'PG-13'),
('EIDR-3',  'Lost in Time',     110, 10.99, '02:00 PM', 'G'),
('EIDR-4',  'The Last Stand',   130, 14.99, '04:00 PM', 'R'),
('EIDR-5',  'Rising Sun',       105, 9.99,  '06:00 PM', 'PG'),
('EIDR-6',  'Midnight Run',     88,  7.99,  '08:00 PM', 'PG-13'),
('EIDR-7',  'Shadow Falls',     115, 11.99, '10:00 AM', '+13'),
('EIDR-8',  'Broken Wings',     100, 9.99,  '12:00 PM', '+18'),
('EIDR-9',  'Edge of Tomorrow', 125, 13.99, '02:00 PM', 'PG-13'),
('EIDR-10', 'Final Horizon',    140, 15.99, '04:00 PM', 'R');
------------------------------------------------------------------------------------------------------------

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
CREATE PROCEDURE GetMoviesInHall
    @HallNumber int
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Hall WHERE HallNumber = @HallNumber)
    BEGIN
        RAISERROR('Hall not found!', 16, 1);
        RETURN;
    END

    SELECT Movie.EIDR, Movie.Title, Movie.Duration, Movie.Price, Movie.TimeSlots, Movie.AgeRating
    FROM Movie
    JOIN Displayed_in ON Displayed_in.MOEIDR = Movie.EIDR
    WHERE Displayed_in.HNumber = @HallNumber
END;

--To call
EXEC GetMoviesInHall @HallNumber = 1; --1 is for example


--Stored procedure part
create Function CheckHallCapacity
    (@HallNumberParameter int)
    Returns int
AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM Hall WHERE HallNumber = @HallNumberParameter)
        RETURN -1; 

Declare @ReturnHallCapacity int
select @ReturnHallCapacity = HallCapacity
from Hall
where HallNumber = @HallNumberParameter
return @ReturnHallCapacity;
END;

--To call 
select CheckHallCapacity(1) AS CapacityForRequiredHall; --1 is for example


--CRUD HALL PART 

--Update hall 
CREATE PROCEDURE UpdateHall
    @HallNumber INT,
    @HallCapacity INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Hall WHERE HallNumber = @HallNumber)
    BEGIN
        RAISERROR('Hall not found!', 16, 1);
        RETURN;
    END

    UPDATE Hall
    SET HallCapacity = @HallCapacity
    WHERE HallNumber = @HallNumber;
END;

--Delete hall
CREATE PROCEDURE DeleteHall
    @HallNumber INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Hall WHERE HallNumber = @HallNumber)
    BEGIN
        RAISERROR('Hall not found!', 16, 1);
        RETURN;
    END

    DELETE FROM Hall
    WHERE HallNumber = @HallNumber;
END;


