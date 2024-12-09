USE Membuku;

ALTER TABLE Users
ADD [Role] VARCHAR(20) NOT NULL DEFAULT 'User';

--set default password 'indocyber', dan disimpan sebagai argon
UPDATE USERS
SET [Password] = '$argon2id$v=19$m=65536,t=3,p=1$xlhTEXcZBWuFkqORsAAWOg$r0erMXaAhZhpDCEgsw5MUCCM8JU371B4Rmmd+uTQwSU'

GO

INSERT INTO USERS(Username, Password, Email, FirstName, LastName, BirthDate, Gender, JoinDate, LastActiveDate, [Role])
VALUES(
'admin', '$argon2id$v=19$m=65536,t=3,p=1$xlhTEXcZBWuFkqORsAAWOg$r0erMXaAhZhpDCEgsw5MUCCM8JU371B4Rmmd+uTQwSU',
'admin@gmail.com', 'admin','admin', '1990-01-01', 'M', '2000-01-01', null, 'Admin'
);