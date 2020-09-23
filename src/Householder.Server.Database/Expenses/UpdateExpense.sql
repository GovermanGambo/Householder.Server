UPDATE expense 
SET 
    payee_id = @payeeId, 
    amount = @amount, 
    transaction_date = @transactionDate, 
    note = @note,
    status_id = (@status + 1)
WHERE 
    id = @id