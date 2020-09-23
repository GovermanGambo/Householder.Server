SELECT e.id, 
       u.id AS userId, 
       u.name AS userName, 
       e.amount, 
       e.transactionDate, 
       e.note, 
       (e.status_id - 1) AS status
FROM `expense` e 
LEFT JOIN `user` u ON u.id=e.payee_id
WHERE status_id = (@status + 1);