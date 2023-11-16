-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 08, 2023 at 09:42 PM
-- Server version: 10.9.8-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `room_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

CREATE TABLE `clients` (
  `id` int(11) NOT NULL,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `phone` varchar(100) NOT NULL,
  `type` varchar(255) NOT NULL,
  `country` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;

--
-- Dumping data for table `clients`
--

INSERT INTO `clients` (`id`, `first_name`, `last_name`, `phone`, `type`, `country`) VALUES
(1, 'คนสวย', 'แม่นาง', '0658526858', 'ขาประจำ', 'จีน'),
(2, 'worachet', 'nolastname', '028221582', '', ''),
(3, 'worapakorn', 'jarusiriphot', '0891688776', '', ''),
(4, 'ป้าแม้น', 'มารวย', '081814488', '', 'ขายอาหารตามสั่ง'),
(9, 'วรปกร', 'จารุศิริพจน์', '0623844415', '', 'ขายลูกชิ้น');

-- --------------------------------------------------------

--
-- Table structure for table `reservations`
--

CREATE TABLE `reservations` (
  `id` int(11) NOT NULL,
  `room_number` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `date_in` date NOT NULL,
  `date_out` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;

--
-- Dumping data for table `reservations`
--

INSERT INTO `reservations` (`id`, `room_number`, `client_id`, `date_in`, `date_out`) VALUES
(16, 303, 3, '2023-11-10', '2023-11-30'),
(17, 205, 4, '2023-11-10', '2023-11-30'),
(19, 404, 9, '2023-11-10', '2023-11-21'),
(20, 402, 9, '2023-11-10', '2023-11-21'),
(21, 101, 9, '2023-11-10', '2023-11-21');

-- --------------------------------------------------------

--
-- Table structure for table `rooms`
--

CREATE TABLE `rooms` (
  `number` int(11) NOT NULL,
  `type` int(11) NOT NULL,
  `phone` varchar(100) NOT NULL,
  `free` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;

--
-- Dumping data for table `rooms`
--

INSERT INTO `rooms` (`number`, `type`, `phone`, `free`) VALUES
(101, 1, 'C101', 'NO'),
(102, 1, 'C102', 'YES'),
(103, 1, 'C103', 'YES'),
(104, 1, 'C104', 'YES'),
(105, 1, 'C105', 'YES'),
(201, 2, 'C201', 'YES'),
(202, 2, 'C202', 'YES'),
(203, 2, 'C203', 'YES'),
(204, 2, 'C204', 'YES'),
(205, 2, 'C205', 'NO'),
(206, 2, 'C206', 'YES'),
(301, 3, 'C301', 'YES'),
(302, 3, 'C302', 'YES'),
(303, 3, 'C303', 'NO'),
(304, 3, 'C304', 'YES'),
(401, 4, 'C401', 'YES'),
(402, 4, 'C402', 'NO'),
(403, 4, 'C403', 'YES'),
(404, 4, 'C404', 'NO');

-- --------------------------------------------------------

--
-- Table structure for table `rooms_type`
--

CREATE TABLE `rooms_type` (
  `id` int(11) NOT NULL,
  `label` varchar(200) NOT NULL,
  `price` decimal(9,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;

--
-- Dumping data for table `rooms_type`
--

INSERT INTO `rooms_type` (`id`, `label`, `price`) VALUES
(1, 'โซน1', 1000.00),
(2, 'โซน2', 2000.00),
(3, 'โซน3', 3000.00),
(4, 'โซน4', 5000.00);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_unicode_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`) VALUES
(1, 'admin', 'admin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `reservations`
--
ALTER TABLE `reservations`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_room_number` (`room_number`),
  ADD KEY `fk_client_id` (`client_id`);

--
-- Indexes for table `rooms`
--
ALTER TABLE `rooms`
  ADD PRIMARY KEY (`number`),
  ADD KEY `fk_type_id` (`type`);

--
-- Indexes for table `rooms_type`
--
ALTER TABLE `rooms_type`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clients`
--
ALTER TABLE `clients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `reservations`
--
ALTER TABLE `reservations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `rooms_type`
--
ALTER TABLE `rooms_type`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `reservations`
--
ALTER TABLE `reservations`
  ADD CONSTRAINT `fk_client_id` FOREIGN KEY (`client_id`) REFERENCES `clients` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_room_number` FOREIGN KEY (`room_number`) REFERENCES `rooms` (`number`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `rooms`
--
ALTER TABLE `rooms`
  ADD CONSTRAINT `fk_type_id` FOREIGN KEY (`type`) REFERENCES `rooms_type` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
