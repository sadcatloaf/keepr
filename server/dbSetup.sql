CREATE TABLE accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture',
  cover_img VARCHAR(1000) NOT NULL
) default charset utf8mb4 COMMENT '';

  SELECT
        accounts.*
        FROM accounts
        WHERE accounts.id = LAST_INSERT_ID()

DROP TABLE accounts; 
ALTER TABLE accounts ADD COLUMN cover_img VARCHAR(1000) NOT NULL;

CREATE TABLE keeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  views INT NOT NULL,
  creator_id VARCHAR(255) NOT NULL
);
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        WHERE keeps.id = LAST_INSERT_ID()

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id

CREATE TABLE vaultKeeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  keep_id INT NOT NULL,
  vault_id INT NOT NULL,
  creator_id VARCHAR(255) NOT NULL
);


CREATE TABLE vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  is_private BOOLEAN NOT NULL DEFAULT false,
  creator_id VARCHAR(255) NOT NULL
);

DROP TABLE vaults;




SELECT
        accounts.*,
        keeps.*
        FROM accounts
        JOIN keeps ON keeps.creator_id = accounts.id
        WHERE accounts.id = '6758bbc002d980ff6cdb96c2';

         SELECT
            keeps.*,
            vaultKeeps.id AS vaultKeepId,
            accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creator_id
        JOIN vaultKeeps ON vaultKeeps.keep_id = keeps.id
        JOIN vaults ON vaults.id = vaultKeeps.vault_id
        WHERE vaultKeeps.vault_id = 199 AND vaults.is_private = false;

        SELECT * FROM vaults WHERE is_private = false;