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
--delete customer from database(delete account)
CREATE PROCEDURE DeleteCustomer
    @CustomerID INT
AS
BEGIN
    DELETE FROM Customerfinal
    WHERE Customer_ID = @CustomerID;
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
        b.Booking_ID,
        b.Booking_date,
        b.Total_Amount
    FROM Customerfinal c
    LEFT JOIN Booking b 
        ON c.Customer_ID = b.Customer_ID
    WHERE c.Email    = @Email
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
--to delete phone number 
CREATE PROCEDURE DeleteCustomerPhone
    @CustomerID INT,
    @PhoneNo    NVARCHAR(20)
AS
BEGIN
    DELETE FROM CustomerPhonefinal
    WHERE CustomerID = @CustomerID
      AND PhoneNo    = @PhoneNo;
END;
--to get phone numbers 
CREATE PROCEDURE GetCustomerPhones
    @CustomerID INT
AS
BEGIN
    SELECT PhoneNo
    FROM CustomerPhonefinal
    WHERE CustomerID = @CustomerID;
END;




