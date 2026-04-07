-- Basic QUERIES 

-- Query 1: Get all movies by genre merge tables
-- This shows each movie with its genre
SELECT 
    m.EIDR,
    m.Title,
    m.Duration,
    m.Price,
    m.AgeRating,
    mg.Genre
FROM Movie m, Movie_Genre mg
WHERE m.EIDR = mg.EIDR;

-- Example usage (filter by genre):
-- SELECT * FROM Movie m, Movie_Genre mg 
-- WHERE m.EIDR = mg.EIDR AND mg.Genre = 'Action';


-- Query 2: Count number of movies in each genre
-- Groups movies and counts how many belong to each genre
SELECT 
    Genre,
    COUNT(*) AS NumberOfMovies
FROM Movie_Genre
GROUP BY Genre;


-- Query 3: Get all movies with their genres 
-- Shows movie title with each genre
SELECT 
    m.Title,
    mg.Genre
FROM Movie m, Movie_Genre mg
WHERE m.EIDR = mg.EIDR
ORDER BY m.Title;
