create procedure log_new_transaction
	@transaction_type nvarchar(20),
	@transaction_status nvarchar(20),
	@payment_id int = NULL
	as
	begin
	IF @transaction_type NOT IN ('payment', 'refund')
    BEGIN
        print('Invalid transaction type');
        RETURN;
    END
    IF @transaction_status NOT IN ('pending','failed', 'completed')
    BEGIN
        print('Invalid transaction status');
        RETURN;
    END;
    IF @payment_id IS NULL OR NOT EXISTS (SELECT 1 FROM payment WHERE payment_id = @payment_id)
    BEGIN
        PRINT 'Invalid payment_id (must exist already)';
        RETURN;
    END
	insert into transactions (transaction_datetime, transaction_type, transaction_status, payment_id) values (GETDATE(), @transaction_type, @transaction_status, @payment_id)
	end;
	--exec log_new_transaction @transaction_type = 'Payment', @transaction_status = 'Completed', @payment_id = null;
	--select * from transactions;
