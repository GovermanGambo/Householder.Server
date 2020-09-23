SELECT
    r.id AS id,
    u.id AS creatorId,
    u.first_name AS creatorName,
    r.creation_date AS creation_date
FROM
    reconciliation r
LEFT JOIN user u ON r.creator_id=u.id;