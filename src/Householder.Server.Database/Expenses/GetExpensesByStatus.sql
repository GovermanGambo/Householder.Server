SELECT e.id, 
       u.id AS payeeId, 
       u.first_name AS payeeName, 
       e.amount, 
       e.transaction_date AS transactionDate,  
       e.note, 
       (e.status_id - 1) AS status
FROM `expense` e 
LEFT JOIN `user` u ON u.id=e.payee_id
WHERE status_id = (@status + 1);