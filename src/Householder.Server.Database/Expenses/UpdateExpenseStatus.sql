UPDATE expense 
SET
    status_id = (@status + 1)
WHERE 
    id = @id