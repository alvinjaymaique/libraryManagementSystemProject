-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 30, 2024 at 05:31 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `library`
--

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `hash_password` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(100) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `role` varchar(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`id`, `username`, `hash_password`, `password`, `email`, `created_at`, `updated_at`, `role`) VALUES
(4, 'jay', '$2a$11$jSEF5JRy1FAZxkHcx.kfTehhemeNKEhV7CVMnPCNMcAsJ3HM4CdgW', 'jay', 'jay@gmail.com', '2024-12-29 17:46:06', '2024-12-29 17:46:06', 'Admin'),
(5, 'admin', '$2a$11$53TZO8oPJJsij7dTh.GJa.GhvPkNy5X0fDsJKSHAqdqp4birOMHCS', 'admin', 'admin@gmail.com', '2024-12-29 17:49:43', '2024-12-29 17:49:43', 'Admin'),
(6, 'alvin', '$2a$11$r/ocJmEYhsXyBmJJ4SJRt.Y5qext5HAS3n24Dy7ZtPYZwedktS3GK', 'alvin', 'alvin@gmail.com', '2024-12-29 17:56:06', '2024-12-29 17:56:06', 'Admin'),
(7, 'maique', '$2a$11$7IR0LlZ3G68MBFAMvl30S.4Q6S4jw/hltAokubgrjDNEWatnw/GcG', 'maique', 'maique@gmail.com', '2024-12-29 17:58:28', '2024-12-29 17:58:28', 'User');

-- --------------------------------------------------------

--
-- Table structure for table `book`
--

CREATE TABLE `book` (
  `id` int(11) NOT NULL,
  `title` varchar(255) NOT NULL,
  `author` varchar(255) DEFAULT NULL,
  `isbn` varchar(13) DEFAULT NULL,
  `genre` varchar(100) DEFAULT NULL,
  `publisher` varchar(255) DEFAULT NULL,
  `published_year` int(11) DEFAULT NULL,
  `quantity` int(11) NOT NULL CHECK (`quantity` >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `book`
--

INSERT INTO `book` (`id`, `title`, `author`, `isbn`, `genre`, `publisher`, `published_year`, `quantity`) VALUES
(1, 'One Piece', 'Eiichiro Oda', NULL, 'Action, Adventure', 'Shueisha', 1997, 127),
(2, 'Attack on Titan', 'Hajime Isayama', NULL, 'Dark Fantasy, Action', 'Kodansha', 2009, 108),
(3, 'Demon Slayer', 'Koyoharu Gotouge', NULL, 'Action, Fantasy', 'Shueisha', 2016, 107),
(4, 'Naruto', 'Masashi Kishimoto', NULL, 'Action, Fantasy', 'Shueisha', 1999, 108),
(5, 'Death Note', 'Tsugumi Ohba', NULL, 'Psychological Thriller', 'Shueisha', 2003, 108),
(6, 'My Hero Academia', 'Kohei Horikoshi', NULL, 'Superhero, Action', 'Shueisha', 2014, 109),
(7, 'Berserk', 'Kentaro Miura', NULL, 'Dark Fantasy', 'Hakusensha', 1989, 109),
(8, 'JoJo\'s Bizarre Adventure', 'Hirohiko Araki', NULL, 'Action, Fantasy', 'Shueisha', 1987, 110),
(9, 'Vagabond', 'Takehiko Inoue', NULL, 'Historical, Martial Arts', 'Kodansha', 1998, 110),
(10, 'Monster', 'Naoki Urasawa', NULL, 'Thriller, Mystery', 'Shogakukan', 1994, 110),
(11, 'Tokyo Ghoul', 'Sui Ishida', NULL, 'Dark Fantasy', 'Shueisha', 2011, 110),
(12, 'Sailor Moon', 'Naoko Takeuchi', NULL, 'Magical Girl, Fantasy', 'Kodansha', 1991, 110),
(13, 'Akira', 'Katsuhiro Otomo', NULL, 'Cyberpunk, Sci-fi', 'Kodansha', 1982, 110),
(14, 'Dragon Ball', 'Akira Toriyama', NULL, 'Action, Fantasy', 'Shueisha', 1984, 108),
(15, 'Hunter x Hunter', 'Yoshihiro Togashi', NULL, 'Adventure, Fantasy', 'Shueisha', 1998, 110),
(16, 'Steel Ball Run', 'Hirohiko Araki', NULL, 'Action, Adventure', 'Shueisha', 2004, 110),
(17, 'Neon Genesis Evangelion', 'Yoshiyuki Sadamoto', NULL, 'Mecha, Sci-fi, Drama', 'Khara', 1994, 110),
(18, 'Sword Art Online', 'Reki Kawahara', NULL, 'Sci-Fi, Action, Adventure', 'ASCII Media Works', 2009, 110),
(19, 'Great Teacher Onizuka', 'Tohru Fujisawa', NULL, 'Comedy, Drama, School', 'Shogakukan', 1997, 110),
(20, 'Black Clover', 'Yūki Tabata', NULL, 'Action, Adventure, Fantasy', 'Shueisha', 2015, 110),
(21, 'The Promised Neverland', 'Kaiu Shirai', NULL, 'Thriller, Mystery, Horror', 'Shueisha', 2016, 110),
(22, 'Fairy Tail', 'Hiro Mashima', NULL, 'Action, Adventure, Fantasy', 'Kodansha', 2006, 110),
(23, 'Your Name', 'Makoto Shinkai', NULL, 'Romance, Drama, Supernatural', 'Kadokawa', 2016, 110),
(24, 'Steins;Gate', '5bp', NULL, 'Sci-Fi, Thriller, Psychological', 'Mages', 2011, 110),
(25, 'Fullmetal Alchemist', 'Hiromu Arakawa', NULL, 'Action, Adventure, Fantasy', 'Square Enix', 2001, 109),
(26, 'Mob Psycho 100', 'ONE', NULL, 'Action, Comedy, Supernatural', 'Shogakukan', 2012, 110),
(27, 'No Game No Life', 'Yuu Kamiya', NULL, 'Isekai, Fantasy, Game', 'MF Books', 2012, 110),
(28, 'That Time I Reincarnated as a Slime', 'Fuse', NULL, 'Isekai, Fantasy, Action', 'Kodansha', 2013, 110),
(29, 'The Rising of the Shield Hero', 'Aneko Yusagi', NULL, 'Isekai, Fantasy, Action', 'Media Factory', 2013, 110),
(30, 'The Rising of the Shield Hero', 'Aneko Yusagi', NULL, 'Isekai, Action, Fantasy', 'Media Factory', 2012, 110),
(31, 'Harry Potter Series', 'J.K. Rowling', '9780747532743', 'Fantasy, Adventure', 'Bloomsbury', 1997, 110),
(32, 'Percy Jackson & The Olympians', 'Rick Riordan', '9781423101452', 'Fantasy, Adventure', 'Disney Hyperion', 2005, 110),
(33, 'The Lord of the Rings', 'J.R.R. Tolkien', '9780618640157', 'Fantasy, Adventure', 'Houghton Mifflin', 1954, 110),
(34, 'The Chronicles of Narnia', 'C.S. Lewis', '9780064471190', 'Fantasy, Adventure', 'HarperCollins', 1950, 110),
(35, 'The Witcher Series', 'Andrzej Sapkowski', '9780316029185', 'Fantasy, Adventure', 'Orbit', 1992, 110),
(36, 'The Hobbit', 'J.R.R. Tolkien', '9780547928227', 'Fantasy, Adventure', 'Houghton Mifflin', 1937, 110),
(37, 'Mad Max', 'George Miller', NULL, 'Action, Dystopian', 'Warner Bros.', 1979, 110),
(38, 'Fast & Furious', 'Rob Cohen, Justin Lin, James Wan', NULL, 'Action, Racing', 'Universal Pictures', 2001, 110),
(39, 'Days of Thunder', 'Tony Scott', NULL, 'Action, Racing', 'Paramount Pictures', 1990, 110),
(40, 'Batang Quiapo', 'Coco Martin', '54322345', 'Action, Drama', 'ABS-CBN', 2020, 5),
(42, 'FPJ\'s Ang Probinsyano', 'Coco Melon', '4372890', 'Action', 'ABS-CBN', 2018, 3),
(44, 'test', 'test', '32423', 'test', 'test', 3452432, 21);

-- --------------------------------------------------------

--
-- Table structure for table `fine`
--

CREATE TABLE `fine` (
  `fine_id` int(11) NOT NULL,
  `patron_id` int(11) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `date_applied` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `patron`
--

CREATE TABLE `patron` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `membership_id` varchar(50) NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `address` text DEFAULT NULL,
  `membership_type` varchar(50) DEFAULT NULL,
  `birthdate` date DEFAULT NULL,
  `status` varchar(9) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `patron`
--

INSERT INTO `patron` (`id`, `name`, `membership_id`, `email`, `phone`, `address`, `membership_type`, `birthdate`, `status`) VALUES
(1, 'John Doe', 'M12345', 'john.doe@example.com', '555-1234', '123 Main St, Cityville, NY', 'Standard', '1990-01-01', 'Active'),
(2, 'Jane Smith', 'M12346', 'jane.smith@example.com', '555-2345', '456 Oak St, Townsville, CA', 'Premium', '1985-05-20', 'Active'),
(3, 'Robert Brown', 'M12347', 'robert.brown@example.com', '555-3456', '789 Pine St, Villagetown, TX', 'Standard', '1980-09-15', 'Inactive'),
(4, 'Emily Davis', 'M12348', 'emily.davis@example.com', '555-4567', '101 Maple St, Countryville, FL', 'Premium', '1992-02-25', 'Active'),
(5, 'Michael Wilson', 'M12349', 'michael.wilson@example.com', '555-5678', '202 Birch St, Lakedale, WA', 'Standard', '1988-11-10', 'Active'),
(6, 'Sarah Johnson', 'M12350', 'sarah.johnson@example.com', '555-6789', '303 Cedar St, Hilltown, CO', 'Premium', '1991-07-30', 'Active'),
(7, 'David Lee', 'M12351', 'david.lee@example.com', '555-7890', '404 Elm St, Forestwood, OR', 'Standard', '1987-03-12', 'Inactive'),
(8, 'Laura Martinez', 'M12352', 'laura.martinez@example.com', '555-8901', '505 Redwood St, Riverdale, IL', 'Premium', '1990-06-18', 'Active'),
(9, 'James Miller', 'M12353', 'james.miller@example.com', '555-9012', '606 Ash St, Greenfield, OH', 'Standard', '1989-04-05', 'Active'),
(10, 'Maria Taylor', 'M12354', 'maria.taylor@example.com', '555-0123', '707 Willow St, Springside, MI', 'Premium', '1986-12-17', 'Inactive'),
(12, 'Alvin Jay Maique', 'M00001', 'alvin@gmail.com', '09221212', 'Cebu City', 'Premium', '2001-12-23', 'Active'),
(17, 'test', 'M00002', 'test', '0249120978', 'test', 'Standard', '2024-12-23', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `transaction`
--

CREATE TABLE `transaction` (
  `transaction_id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL,
  `patron_id` int(11) NOT NULL,
  `checkout_date` date NOT NULL,
  `due_date` date NOT NULL,
  `return_date` date DEFAULT NULL,
  `fine_amount` decimal(10,2) DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transaction`
--

INSERT INTO `transaction` (`transaction_id`, `book_id`, `patron_id`, `checkout_date`, `due_date`, `return_date`, `fine_amount`) VALUES
(33, 1, 1, '2024-12-01', '2024-12-10', '0001-01-01', 0.00),
(34, 2, 1, '2024-12-12', '2024-12-24', '0001-01-01', 0.00),
(35, 3, 1, '2024-12-10', '2024-12-22', '0001-01-01', 0.00),
(36, 1, 2, '2024-12-01', '2024-12-03', '0001-01-01', 0.00),
(37, 2, 2, '2024-12-02', '2024-12-04', '0001-01-01', 0.00),
(38, 3, 2, '2024-12-02', '2024-12-04', '0001-01-01', 0.00),
(39, 4, 2, '2024-12-01', '2024-12-02', '0001-01-01', 0.00),
(40, 5, 2, '2024-12-01', '2024-12-09', '0001-01-01', 0.00),
(41, 6, 2, '2024-12-01', '2024-12-11', '2024-12-28', 0.00);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indexes for table `book`
--
ALTER TABLE `book`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `isbn` (`isbn`);

--
-- Indexes for table `fine`
--
ALTER TABLE `fine`
  ADD PRIMARY KEY (`fine_id`),
  ADD KEY `patron_id` (`patron_id`);

--
-- Indexes for table `patron`
--
ALTER TABLE `patron`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `membership_id` (`membership_id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indexes for table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`transaction_id`),
  ADD KEY `book_id` (`book_id`),
  ADD KEY `patron_id` (`patron_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `book`
--
ALTER TABLE `book`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- AUTO_INCREMENT for table `fine`
--
ALTER TABLE `fine`
  MODIFY `fine_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `patron`
--
ALTER TABLE `patron`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `transaction_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `fine`
--
ALTER TABLE `fine`
  ADD CONSTRAINT `fine_ibfk_1` FOREIGN KEY (`patron_id`) REFERENCES `patron` (`id`);

--
-- Constraints for table `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `transaction_ibfk_1` FOREIGN KEY (`book_id`) REFERENCES `book` (`id`),
  ADD CONSTRAINT `transaction_ibfk_2` FOREIGN KEY (`patron_id`) REFERENCES `patron` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
