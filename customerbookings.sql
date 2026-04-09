SELECT 
    c.customer_ID,
    c.name,
    cp.phone_number,
    b.Booking_ID,
    b.Booking_date,
    b.Total_Amount
FROM Customer c
JOIN Booking b 
    ON c.customer_ID = b.Customer_ID
LEFT JOIN CustomerPhone cp 
    ON c.customer_ID = cp.customer_id;