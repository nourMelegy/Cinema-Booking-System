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
        SELECT @payment_id = ISNULL(MAX(payment_id), 0) + 1 FROM payment;
        INSERT INTO payment (payment_id)
        VALUES (@payment_id);
    END
	declare @next_id INT;
    select @next_id = ISNULL(MAX(transaction_id), 0) + 1
from transactions;
	insert into transactions (transaction_id,transaction_datetime, transaction_type, transaction_status, payment_id) values (@next_id,GETDATE(), @transaction_type, @transaction_status, @payment_id)
	end;
	--exec log_new_transaction @transaction_type = 'Payment', @transaction_status = 'Completed', @payment_id = null;
	--select * from transactions;