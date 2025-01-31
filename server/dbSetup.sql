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

-- CREATE VIEW keeps_with_kept_count_view AS
-- SELECT
-- keeps.*,
-- COUNT(keep.id) AS kept_count
-- FROM keeps
-- LEFT JOIN 
-- ??????????????????????????



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

        DELETE FROM vaults;
        DELETE FROM keeps;

INSERT into `vaultKeeps` (vault_id, keep_id, creator_id) VALUES (239, 193, '6758bbc002d980ff6cdb96c2');

-- SELECT
--             keeps.*,
--             vaultKeeps.id AS vaultKeepId,
--             accounts.*
--         FROM keeps
--         JOIN accounts ON accounts.id = keeps.creator_id
--         JOIN vaultKeeps ON vaultKeeps.keep_id = keeps.id
--         JOIN vaults ON vaults.id = vaultKeeps.vault_id
--         WHERE vaultKeeps.vault_id = 242;


 SELECT
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        LEFT JOIN vaultKeeps ON keeps.id = vaultKeeps.keep_id
        JOIN accounts ON keeps.creator_id = accounts.id
        WHERE keeps.id = @keepId;
 SELECT
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        LEFT JOIN vaultKeeps ON keeps.id = vaultKeeps.keep_id
        WHERE keeps.id = @keepId
        GROUP BY(keeps.id);
    


--         keeps.*,
--         COUNT(vaultKeeps.id) AS kept,
--         accounts.*
--         FROM keeps
--         LEFT JOIN vaultKeeps ON keeps.id = vaultKeeps.keep_id
--         JOIN accounts ON keeps.creator_id = accounts.id
--         WHERE keeps.id = @keepId;

    --SELECT
    --=  cryptids.*,
    --  COUNT(cryptid_encounters.id) AS encounter_count,
    --=  accounts.*
    --= FROM cryptids
    --  LEFT JOIN cryptid_encounters ON cryptids.id = cryptid_encounters.cryptid_id
    --=  JOIN accounts ON cryptids.discoverer_id = accounts.id
    -- GROUP BY(cryptids.id)
    -- ORDER BY cryptids.created_at ASC;";


    --  SELECT
    -- cryptids.*,
    -- COUNT(cryptid_encounters.id) AS encounter_count,
    -- accounts.*
    -- FROM cryptids
    -- JOIN accounts ON cryptids.discoverer_id = accounts.id
    -- LEFT JOIN cryptid_encounters ON cryptids.id = cryptid_encounters.cryptid_id
    -- WHERE cryptids.id = @cryptidId
    -- GROUP BY (cryptids.id);