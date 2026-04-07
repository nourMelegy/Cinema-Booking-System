-- Stored Procedures 
-- Update movie price by percentage
CREATE PROCEDURE UpdateMoviePrice
    @MovieEIDR VARCHAR(20),
    @PricePercentage DECIMAL(5,2)
AS
BEGIN
    UPDATE Movie
    SET Price = Price + (Price * @PricePercentage / 100)
    WHERE EIDR = @MovieEIDR;
END;

-- Add a new movie
CREATE PROCEDURE AddMovie
    @EIDR VARCHAR(20),
    @Title VARCHAR(100),
    @Duration INT,
    @Price DECIMAL(8,2),
    @TimeSlots VARCHAR(100),
    @AgeRating VARCHAR(10)
AS
BEGIN
    INSERT INTO Movie (EIDR, Title, Duration, Price, TimeSlots, AgeRating)
    VALUES (@EIDR, @Title, @Duration, @Price, @TimeSlots, @AgeRating);
END;

-- Get movies for a specific genre
CREATE PROCEDURE GetMoviesByGenre
    @Genre VARCHAR(50)
AS
BEGIN
    SELECT 
        m.Title,
        m.Price,
        m.AgeRating
    FROM Movie m, Movie_Genre mg
    WHERE m.EIDR = mg.EIDR
    AND mg.Genre = @Genre;
END;

-- Delete a movie (genres deleted automatically by FK)
CREATE PROCEDURE DeleteMovie
    @MovieEIDR VARCHAR(20)
AS
BEGIN
    DELETE FROM Movie
    WHERE EIDR = @MovieEIDR;
END;

-- Add genre to a movie
CREATE PROCEDURE AddGenreToMovie
    @MovieEIDR VARCHAR(20),
    @Genre VARCHAR(50)
AS
BEGIN
    INSERT INTO Movie_Genre (EIDR, Genre)
    VALUES (@MovieEIDR, @Genre);
END;
