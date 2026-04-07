-- ===========================================================================
-- 1. CREATION of TABLES
-- ============================================================================
 
-- Create Movie Table
CREATE TABLE Movie (
    EIDR VARCHAR(20) PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Duration INT NOT NULL,  -- Duration in minutes
    Price DECIMAL(8, 2) NOT NULL,
    TimeSlots VARCHAR(100),
    AgeRating VARCHAR(10) NOT NULL,
    CONSTRAINT CK_Price CHECK (Price > 0),
    CONSTRAINT CK_Duration CHECK (Duration > 0),
    CONSTRAINT CK_AgeRating CHECK (AgeRating IN ('G', 'PG', 'PG-13', 'R', '+13', '+18'))
);
-- Create Movie_Genre Table (because multivalued)
CREATE TABLE Movie_Genre (
    EIDR VARCHAR(20),
    Genre VARCHAR(50) NOT NULL,
    PRIMARY KEY (EIDR, Genre),
    FOREIGN KEY (EIDR) REFERENCES Movie(EIDR) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT CK_Genre CHECK (Genre IN ('Action', 'Comedy', 'Drama', 'Horror', 'Romance', 'Thriller', 'Sci-Fi', 'Animation', 'Adventure', 'Documentary'))
);
