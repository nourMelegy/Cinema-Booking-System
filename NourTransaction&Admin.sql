create table admin (
admin_id int identity(1,1) primary key,
admin_fname nvarchar(50) not null,
admin_lname nvarchar(50) not null,
admin_email nvarchar(100) unique not null check (admin_email like '%_@_%._%'),
admin_password nvarchar(100) not null
);	
create table transactions (
	transaction_id int identity(1,1) primary key,
	transaction_datetime datetime not null,
	transaction_type nvarchar(20) check (transaction_type in ('payment','refund')),
	transaction_status nvarchar(20) default 'pending' check (transaction_status in ('pending','completed','failed')),
	payment_id int not null,
	foreign key (payment_id) references payment(payment_id) 
	ON DELETE SET NULL
	ON UPDATE NO ACTION
);
