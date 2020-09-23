INSERT INTO `expense` (`payee_id`, `amount`, `transaction_date`, `note`, `status_id`) 
VALUES (@payeeId, @amount, @transactionDate, @note, @status + 1);
