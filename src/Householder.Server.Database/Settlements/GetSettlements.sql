SELECT 
    s.id AS id,
    s.reconciliation_id AS reconciliationId,
    s.creditor_id AS creditorId,
    u1.name AS creditorName,
    s.debtor_id AS debtorId,
    u2.name AS debtorName,
    s.amount AS amount,
    (s.status_id - 1) AS statusId
FROM `settlement` s
LEFT JOIN `user` u1 AS s.creditor_id=u1.id
LEFT JOIN `user` u2 AS s.debtor_id=u2.id;