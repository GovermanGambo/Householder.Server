SELECT
    u.id AS id,
    u.email AS email,
    u.hashed_password AS hashedPassword
FROM
    user u
WHERE u.email = @email 
LIMIT 1;
