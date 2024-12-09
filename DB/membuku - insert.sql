USE Membuku;

INSERT INTO Users (Username, Password, Email, FirstName, LastName, BirthDate, Gender, JoinDate, LastActiveDate)
VALUES 
('johndoe', 'password123', 'john@example.com', 'John', 'Doe', '1990-05-15', 'M', '2024-08-15', '2024-08-15 10:30:00'),
('janedoe', 'password456', 'jane@example.com', 'Jane', 'Doe', '1988-03-22', 'F', '2024-08-14', '2024-08-15 09:15:00'),
('alicew', 'password789', 'alice@example.com', 'Alice', 'Williams', '1995-07-30', 'F', '2024-08-13', '2024-08-15 08:45:00');


INSERT INTO Authors (Name)
VALUES 
('George Orwell'),
('Harper Lee'),
('J.K. Rowling'),
('J.R.R. Tolkien'),
('Agatha Christie');


INSERT INTO Books (Name, Cover, ReleaseDate, AuthorId)
VALUES 
('1984', '1984_cover.jpg', '1949-06-08', 1),
('To Kill a Mockingbird', 'mockingbird_cover.jpg', '1960-07-11', 2),
('Harry Potter and the Philosopher''s Stone', 'hp1_cover.jpg', '1997-06-26', 3),
('The Lord of the Rings', 'lotr_cover.jpg', '1954-07-29', 4),
('Murder on the Orient Express', 'orient_express_cover.jpg', '1934-01-01', 5),
('Animal Farm', 'animal_farm_cover.jpg', '1945-08-17', 1),
('Go Set a Watchman', 'watchman_cover.jpg', '2015-07-14', 2),
('Harry Potter and the Chamber of Secrets', 'hp2_cover.jpg', '1998-07-02', 3),
('The Hobbit', 'hobbit_cover.jpg', '1937-09-21', 4),
('And Then There Were None', 'none_cover.jpg', '1939-11-06', 5);


INSERT INTO Reviews (Username, BookId, ReadStatus, ReadDate, AddedDate)
VALUES 
-- John Doe's Collection
('johndoe', 3,	'TO-READ', NULL, '2024-08-12 15:30:00'),
('johndoe', 5,	'TO-READ', NULL, '2024-08-13 16:00:00'),
('johndoe', 8, 'TO-READ', NULL, '2024-08-14 09:00:00'),
('johndoe', 2,	'CURRENTLY-READING', NULL, '2024-08-10 14:00:00'),
('johndoe', 6, 'CURRENTLY-READING', NULL, '2024-08-15 08:00:00'),
('johndoe', 7, 'READ', '2024-07-01', '2024-07-01 13:00:00'),
('johndoe', 9, 'READ', '2024-08-04', '2024-08-04 14:00:00'),

-- Jane Doe's Collection
('janedoe', 1,	'TO-READ', NULL, '2024-08-05 09:00:00'),
('janedoe', 7,	'TO-READ', NULL, '2024-08-12 10:15:00'),
('janedoe', 10, 'TO-READ', NULL, '2024-08-15 12:30:00'),
('janedoe', 3,	'CURRENTLY-READING', NULL, '2024-08-14 09:30:00'),
('janedoe', 6,	'READ', '2024-07-15', '2024-07-15 13:30:00'),

-- Alice Williams' Collection
('alicew', 2, 'TO-READ', NULL, '2024-08-14 10:30:00'),
('alicew', 4,	'TO-READ', NULL, '2024-08-14 11:00:00'),
('alicew', 7, 'TO-READ', NULL, '2024-08-15 11:00:00'),
('alicew', 10,	'TO-READ', NULL, '2024-08-13 18:00:00'),
('alicew', 6, 'CURRENTLY-READING', NULL, '2024-08-15 09:30:00'),
('alicew', 9,	'CURRENTLY-READING', NULL, '2024-08-13 17:00:00'),
('alicew', 1, 'READ', '2024-07-11', '2024-07-11 16:15:00'),
('alicew', 5, 'READ', '2024-08-09', '2024-08-09 17:00:00'),
('alicew', 8,	'READ', '2024-07-10', '2024-07-10 14:30:00');

INSERT INTO Reviews (Username, BookId, ReadStatus, ReadDate, AddedDate, Rating, Description, ReviewDate)
VALUES 
('johndoe', 1, 'READ', '2024-08-01', '2024-08-01 12:00:00',
	5, 'A thought-provoking and chilling portrayal of a dystopian future.', '2024-08-02 11:00:00'),
('johndoe', 4, 'READ', '2024-07-20', '2024-07-20 10:00:00',
	4, 'An epic adventure with a deep world and lore.', '2024-08-01 15:00:00'),
('janedoe', 2, 'READ', '2024-08-02', '2024-08-02 11:00:00',
	4, 'A powerful narrative on racial injustice and moral growth.', '2024-08-14 09:30:00'),
('janedoe', 9, 'READ', '2024-08-03', '2024-08-03 11:45:00',
	5, NULL, '2024-08-04 16:00:00'),
('alicew', 3, 'READ', '2024-08-12', '2024-08-12 19:00:00',
	5, 'A magical start to a beloved series.', '2024-08-13 17:45:00');

INSERT INTO HighlightedBooks (BookId, OrderNumber, AddedDate)
VALUES 
(1, 1, '2024-08-15 09:00:00'),
(3, 2, '2024-08-15 09:05:00'),
(9, 3, '2024-08-15 09:10:00');
