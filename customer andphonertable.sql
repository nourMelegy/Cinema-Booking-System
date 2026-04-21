
CREATE TABLE Customerfinal (
    Customer_ID  INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE, 
    Password NVARCHAR(10) NOT NULL,
    CONSTRAINT CHK_Customer_Email 
    CHECK (Email LIKE '%_@_%._%')
);
CREATE TABLE CustomerPhonefinal (
    CustomerID INT NOT NULL,
    PhoneNo NVARCHAR(20) NOT NULL,
    CONSTRAINT PKCustomerPhone 
    PRIMARY KEY (CustomerID, PhoneNo),
    CONSTRAINT CHKCustomerPhone CHECK (PhoneNo LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    CONSTRAINT FK_CustomerPhone_Customer FOREIGN KEY (CustomerID) REFERENCES Customerfinal(Customer_ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE );
