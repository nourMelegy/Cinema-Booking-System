CREATE TABLE CustomerPhone (
    phone_id INT PRIMARY KEY,
    customer_id INT,
    phone_number NVARCHAR(20),
    
    FOREIGN KEY (customer_id) REFERENCES Customer(customer_ID)
);