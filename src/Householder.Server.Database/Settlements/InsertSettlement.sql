INSERT INTO settlement (`reconciliation_id`, `creditor_id`, `debtor_id`, `amount`, `status_id`)
VALUES (@reconciliationId, @creditorId, @debtorId, @amount, @statusId + 1);