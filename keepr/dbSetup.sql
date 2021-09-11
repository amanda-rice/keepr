CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) NOT NULL COMMENT 'Keep Name',
  description VARCHAR(255) COMMENT 'Group Description',
  img varchar(255) COMMENT 'Group Primary Image Url',
  views INT COMMENT 'Keep view count',
  shares INT COMMENT 'Keep share count',
  keeps INT COMMENT 'Keep save count',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Foreign Key: User Account',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) NOT NULL COMMENT 'Vault Name',
  description varchar(2000) COMMENT 'Vault description',
  img varchar(255) COMMENT 'Vault Image',
  isPrivate TINYINT comment 'is Private',
  creatorId VARCHAR(255) NOT NULL COMMENT 'Foreign Key: User Account',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE vaultKeep(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
  vaultId int NOT NULL comment 'Vault ID',
  keepId int NOT NULL comment 'Keep ID',
  creatorId VARCHAR(255) NOT NULL comment 'Foreign Key: User Account',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE
) default charset utf8 comment '';