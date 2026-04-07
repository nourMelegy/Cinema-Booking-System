create table admin (
admin_id int primary key,
admin_fname nvarchar(50) not null,
admin_lname nvarchar(50) not null,
admin_email nvarchar(100) unique not null,
admin_password nvarchar(100) not null
);	
create table payment (
	payment_id int primary key,
);
create table transactions (
	transaction_id int primary key,
	transaction_datetime datetime not null,
	transaction_type nvarchar(20) check (transaction_type in ('payment','refund')),
	transaction_status nvarchar(20) check (transaction_status in ('pending','completed','failed')),
	payment_id int not null,
	foreign key (payment_id) references payment(payment_id)
);
