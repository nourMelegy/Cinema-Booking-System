Create procedure get_transaction_status
@status nvarchar(20)
as
begin
IF @status NOT IN ('pending','failed', 'completed')
BEGIN
	print('Invalid transaction status');
	RETURN;
END;
select transaction_id, transaction_datetime, transaction_type, transaction_status, payment_id
from transactions
where transaction_status = @status
end;