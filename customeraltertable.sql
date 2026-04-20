ALTER TABLE Customer
ADD 
    first_name NVARCHAR(50),
    last_name NVARCHAR(50),
    email NVARCHAR(100) unique,
    password NVARCHAR(100);
