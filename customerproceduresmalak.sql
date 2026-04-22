--addcustomer to database(register)
create PROCEDURE AddCustomerfinal
   
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @Password NVARCHAR(10)
AS
BEGIN
    INSERT INTO Customerfinal (FirstName, LastName, Email, Password)
    VALUES ( @FirstName, @LastName, @Email, @Password);
END;

-- gives id, firstname, lastname,phone no,booking id, booking data, total amount
CREATE PROCEDURE CustomerLoginWithDetails
    @Email NVARCHAR(100),
    @Password NVARCHAR(10)
AS
BEGIN
    SELECT 
        c.Customer_ID,
        c.FirstName,
        c.LastName,
        c.Email,
        cp.PhoneNo,
        b.Booking_ID,
        b.Booking_date,
        b.Total_Amount
    FROM Customerfinal c
    LEFT JOIN CustomerPhonefinal cp 
        ON c.Customer_ID = cp.CustomerID
    LEFT JOIN Booking b 
        ON c.Customer_ID = b.Customer_ID
    WHERE c.Email = @Email
      AND c.Password = @Password;
END;

-- to add phone numbers
CREATE PROCEDURE AddCustomerPhone
    @CustomerID INT,
    @PhoneNo NVARCHAR(20)
AS
BEGIN
    INSERT INTO CustomerPhonefinal (CustomerID, PhoneNo)
    VALUES (@CustomerID, @PhoneNo);
END;





