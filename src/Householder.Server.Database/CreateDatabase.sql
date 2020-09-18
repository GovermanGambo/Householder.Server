SET FOREIGN_KEY_CHECKS=0;

DROP TABLE resident;
DROP TABLE reconciliation;
DROP TABLE expense_status;
DROP TABLE expense;
DROP TABLE settlement_status;
DROP TABLE settlement;

SET FOREIGN_KEY_CHECKS=1;

CREATE TABLE resident
(
    id INTEGER NOT NULL AUTO_INCREMENT,
    name VARCHAR(30) UNIQUE NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE reconciliation
(
    id INTEGER NOT NULL AUTO_INCREMENT,
    creator_id INTEGER NOT NULL,
    creation_date DATETIME NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (creator_id) REFERENCES resident(id)
);

CREATE TABLE expense_status
(
    id INTEGER NOT NULL,
    status_name VARCHAR(12) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE expense
(
    id INTEGER NOT NULL AUTO_INCREMENT,
    resident_id INTEGER NOT NULL,
    amount DECIMAL NOT NULL DEFAULT 0,
    transaction_date DATETIME NOT NULL,
    note VARCHAR(50) NOT NULL DEFAULT '',
    status_id INTEGER NOT NULL DEFAULT 1,
    reconciliation_id INTEGER NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (resident_id) REFERENCES resident(id),
    FOREIGN KEY (status_id) REFERENCES expense_status(id),
    FOREIGN KEY (reconciliation_id) REFERENCES reconciliation(id)
);

CREATE TABLE settlement_status
(
    id INTEGER NOT NULL,
    status_name VARCHAR(12) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE settlement
(
    id INTEGER NOT NULL AUTO_INCREMENT,
    reconciliation_id INTEGER NOT NULL,
    creditor_id INTEGER NOT NULL,
    debtor_id INTEGER NOT NULL,
    amount DECIMAL NOT NULL DEFAULT 0,
    status_id INTEGER NOT NULL DEFAULT 1,
    PRIMARY KEY (id),
    FOREIGN KEY (reconciliation_id) REFERENCES reconciliation(id),
    FOREIGN KEY (creditor_id) REFERENCES resident(id),
    FOREIGN KEY (debtor_id) REFERENCES resident(id),
    FOREIGN KEY (status_id) REFERENCES settlement_status(id)
);

INSERT INTO settlement_status (id, status_name)
VALUES (1, 'Pending');
INSERT INTO settlement_status (id, status_name)
VALUES (2, 'Approved');
INSERT INTO settlement_status (id, status_name)
VALUES (3, 'Declined');

INSERT INTO expense_status (id, status_name)
VALUES (1, 'Unresolved');
INSERT INTO expense_status (id, status_name)
VALUES (2, 'In Progress');
INSERT INTO expense_status (id, status_name)
VALUES (3, 'Resolved');
