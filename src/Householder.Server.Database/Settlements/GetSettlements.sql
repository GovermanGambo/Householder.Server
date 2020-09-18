SELECT 
    s.id AS id,
    s.reconciliation_id AS reconciliationId,
    s.creditor_id AS creditorId,
    r1.name AS creditorName,
    s.debtor_id AS debtorId,
    r2.name AS debtorName,
    s.amount AS amount,
    (s.status_id - 1) AS statusId
FROM `settlement` s
LEFT JOIN `resident` r1 ON s.creditor_id=r1.id
LEFT JOIN `resident` r2 ON s.debtor_id=r2.id;