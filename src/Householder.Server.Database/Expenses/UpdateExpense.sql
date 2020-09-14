UPDATE expense 
SET 
    resident_id = @residentId, 
    amount = @amount, 
    transaction_date = @transactionDate, 
    note = @note,
    status_id = @status
WHERE 
    id = @id