SELECT
    u.id AS id,
    u.first_name AS firstName,
    u.last_name AS lastName,
    u.email AS email,
    u.is_admin AS isAdmin,
    u.hashed_password AS hashedPassword,
    u.register_date AS registerDate
FROM
    user u
WHERE u.email = @email 
LIMIT 1;
