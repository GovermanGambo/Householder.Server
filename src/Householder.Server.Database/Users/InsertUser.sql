INSERT INTO `user` (`email`, `first_name`, `last_name`, `register_date`, `hashed_password`)
VALUES (@email, @firstName, @lastName, @registerDate, @hashedPassword);