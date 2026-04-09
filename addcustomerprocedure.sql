CREATE PROCEDURE AddCustomer
    @customer_ID INT,
    @name NVARCHAR(100),
    @email NVARCHAR(100),
    @password NVARCHAR(100),
    @phone NVARCHAR(20),
    @phone_id INT
AS
BEGIN
    INSERT INTO Customer (customer_ID, name, email, password)
    VALUES (@customer_ID, @name, @email, @password);

    INSERT INTO CustomerPhone (phone_id, customer_id, phone_number)
    VALUES (@phone_id, @customer_ID, @phone);
END;