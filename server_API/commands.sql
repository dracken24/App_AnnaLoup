-- server: .\mysqld.exe --console
-- client: .\mysql.exe -u root -p
-- .\mysqladmin.exe -u root -p shutdown
-- dracken24-WIN

-- terminal: ipconfig et  IPv4 Address. . . . . . . . . . . : 192.168.*.*

SHOW DATABASE;
CREATE DATABASE name;
CREATE DATABASE IF NOT EXISTS name;

SHOW STATUS LIKE 'Ssl_cipher';

CREATE USER IF NOT EXISTS 'name'@'localhost' IDENTIFIED BY 'pass';

SET PASSWORD FOR 'name'@'localhost' = PASSWORD('pass');
ALTER USER 'name'@'localhost' IDENTIFIED BY 'new pass';

GRANT ALL ON test.* TO 'name'@'localhost';

SELECT USER FROM mysql.user; -- voir les user de la database
SELECT CURRENT_USER();

-- Lorsque 
-- tu es sur ton interface phpmyadmin, tu peux cliquer sur "Nouvelle base de données", tu choisis 
-- alors le nom de ta base de donnée et tu cliques sur "créer". Une fois cela fais, tu arrives sur 
-- une interface te proposant de créer une table. Tu choisis ensuite un nom tout en précisant le 
-- nombre de colonnes que tu souhaites créer dans cette table (dans la vidéo, j'ai ainsi sélectionné 
-- 3 colonnes). Tu arrives alors sur une liste de ligne contenant plusieurs paramètres. Ces lignes 
-- correspondent aux colonnes de ta table, pour chacune tu vas donc pouvoir renseigner le type de 
-- la colonne (int par exemple pour l'id, text pour username et userPassword) mais aussi le nom des 
-- colonnes. Pour ta colonne id et seulement pour celle-ci, il te faudra cocher la case A_I sur la 
-- ligne concernée. Une fois cela fais, tu cliques sur "Enregistrer" et tu as normalement créé 
-- toutes tes colonnes.