SELECT
    r.id AS id,
    r1.id AS creatorId,
    r1.name AS creatorName,
    r.creation_date AS creation_date
FROM
    reconciliation r
LEFT JOIN resident r1 ON r.creator_id=r1.id;