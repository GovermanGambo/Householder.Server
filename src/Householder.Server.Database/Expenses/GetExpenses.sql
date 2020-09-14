SELECT e.id, 
       r.id AS resident_id, 
       r.name AS resident_name, 
       e.amount, 
       e.transaction_date, 
       e.note, 
       (e.status_id - 1) AS status_id
FROM `expense` e 
LEFT JOIN `resident` r ON r.id=e.resident_id;