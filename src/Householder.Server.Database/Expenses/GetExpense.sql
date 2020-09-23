SELECT e.id AS id, 
       u.id AS userId, 
       u.name AS userName, 
       e.amount AS amount, 
       e.transaction_date AS transactionDate, 
       e.note AS note, 
       (e.status_id - 1) AS status
FROM `expense` e 
LEFT JOIN `user` u ON u.id=e.payee_id
WHERE e.id = @id
LIMIT 1;