-- ============================================================================
-- 2. INSERT SAMPLE DATA INTO MOVIE TABLE
-- ============================================================================
 
INSERT INTO Movie (EIDR, Title, Duration, Price, TimeSlots, AgeRating)
VALUES
    ('E001', 'The Matrix Reloaded', 138, 12.50, '14:00, 17:00, 20:00', 'R'),
    ('E002', 'Inception', 148, 13.00, '15:00, 18:00, 21:00', 'PG-13'),
    ('E003', 'The Notebook', 123, 11.00, '13:00, 16:00, 19:00', 'PG'),
    ('E004', 'Shrek', 90, 9.50, '10:00, 12:30, 15:00', 'PG'),
    ('E005', 'The Conjuring', 112, 12.00, '16:00, 19:00, 21:30', 'R'),
    ('E006', 'Toy Story 4', 100, 10.00, '11:00, 13:30, 16:00', 'G'),
    ('E007', 'Interstellar', 169, 14.00, '17:00, 20:30', 'PG-13'),
    ('E008', 'The Titanic', 194, 12.50, '18:00, 21:00', 'PG-13'),
    ('E009', 'Deadpool', 108, 13.50, '19:00, 21:30', 'R'),
    ('E010', 'Encanto', 102, 10.50, '14:00, 16:30, 19:00', 'PG');
 
-- ============================================================================
-- 3. INSERT SAMPLE DATA INTO MOVIE_GENRE TABLE
-- ============================================================================
 
INSERT INTO Movie_Genre (EIDR, Genre)
VALUES
    ('E001', 'Action'),
    ('E001', 'Sci-Fi'),
    ('E002', 'Sci-Fi'),
    ('E002', 'Thriller'),
    ('E002', 'Action'),
    ('E003', 'Romance'),
    ('E003', 'Drama'),
    ('E004', 'Animation'),
    ('E004', 'Comedy'),
    ('E004', 'Adventure'),
    ('E005', 'Horror'),
    ('E005', 'Thriller'),
    ('E006', 'Animation'),
    ('E006', 'Comedy'),
    ('E006', 'Adventure'),
    ('E007', 'Sci-Fi'),
    ('E007', 'Drama'),
    ('E007', 'Adventure'),
    ('E008', 'Romance'),
    ('E008', 'Drama'),
    ('E009', 'Action'),
    ('E009', 'Comedy'),
    ('E010', 'Animation'),
    ('E010', 'Comedy');
