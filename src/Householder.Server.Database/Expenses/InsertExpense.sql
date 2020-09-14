INSERT INTO `expense` (`resident_id`, `amount`, `transaction_date`, `note`, `status_id`) 
VALUES (@residentId, @amount, @transactionDate, @note, @status + 1)