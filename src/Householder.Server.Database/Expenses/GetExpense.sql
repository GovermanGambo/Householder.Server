SELECT e.id, 
       r.id AS residentId, 
       r.name AS residentName, 
       e.amount, 
       e.transaction_date AS transactionDate, 
       e.note, 
       (e.status_id - 1) AS status
FROM `expense` e 
LEFT JOIN `resident` r ON r.id=e.resident_id
WHERE e.id = @id
LIMIT 1;