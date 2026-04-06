insert into admin (admin_id, admin_fname, admin_lname, admin_email, admin_password) values (1, 'Eyan', 'Doe', 'eyanadmin1@gmail.com', 'admin123');
insert into admin (admin_id, admin_fname, admin_lname, admin_email, admin_password) values (2, 'John', 'Smith', 'johnadmin2@gmail.com', 'admin456');
insert into admin (admin_id, admin_fname, admin_lname, admin_email, admin_password) values (3, 'Sara', 'Wayn', 'saraadmin3@gmail.com', 'admin789');
insert into payment (payment_id) values (1);
insert into payment (payment_id) values (2);
insert into payment (payment_id) values (3);
insert into transactions (transaction_id, transaction_datetime, transaction_type, transaction_status, payment_id) values (1, '2026-03-01 10:00:00', 'visa', 'completed', 1);
insert into transactions (transaction_id, transaction_datetime, transaction_type, transaction_status, payment_id) values (2, '2026-04-02 14:30:00', 'paypal', 'pending', 2);
insert into transactions (transaction_id, transaction_datetime, transaction_type, transaction_status, payment_id) values (3, '2026-01-03 09:15:00', 'cash', 'failed', 3);

