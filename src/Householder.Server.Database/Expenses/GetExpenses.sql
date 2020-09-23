SELECT e.id, 
       u.id AS userId, 
       u.name AS userName, 
       e.amount, 
       e.transaction_date AS transactionDate,  
       e.note, 
       (e.status_id - 1) AS status
FROM `expense` e 
LEFT JOIN `user` u ON u.id=e.payee_id;