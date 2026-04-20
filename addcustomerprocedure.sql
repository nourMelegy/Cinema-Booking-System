CREATE PROCEDURE AddCustomer
    @customer_ID INT,
    @first_name NVARCHAR(50),
    @last_name NVARCHAR(50),
    @email NVARCHAR(100),
    @password NVARCHAR(100),
    @phone NVARCHAR(20),
    @phone_id INT
AS
BEGIN
    INSERT INTO Customer (customer_ID, first_name, last_name, email, password)
    VALUES (@customer_ID, @first_name, @last_name, @email, @password);

    INSERT INTO CustomerPhone (phone_id, customer_id, phone_number)
    VALUES (@phone_id, @customer_ID, @phone);
END;
