SELECT e.id, 
       r.id AS residentId, 
       r.name AS residentName, 
       e.amount, 
       e.transactionDate, 
       e.note, 
       (e.status_id - 1) AS status
FROM `expense` e 
LEFT JOIN `resident` r ON r.id=e.resident_id
WHERE status_id = @status AND resident_id = @residentId;