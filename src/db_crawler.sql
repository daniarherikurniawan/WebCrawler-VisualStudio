-- phpMyAdmin SQL Dump
-- version 4.1.6
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Mar 27, 2014 at 04:50 PM
-- Server version: 5.6.16
-- PHP Version: 5.5.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `db_crawler`
--

-- --------------------------------------------------------

--
-- Table structure for table `link_keywords`
--

CREATE TABLE IF NOT EXISTS `link_keywords` (
  `keyword_id` int(11) NOT NULL AUTO_INCREMENT,
  `link_id` int(11) NOT NULL,
  `keyword` varchar(100) NOT NULL,
  PRIMARY KEY (`keyword_id`),
  KEY `keyword` (`keyword`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `reserved_words`
--

CREATE TABLE IF NOT EXISTS `reserved_words` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `word` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 COMMENT='Berisi kata-kata yang tidak boleh dijadikan keyword, ex: dan, yang,' AUTO_INCREMENT=11 ;

--
-- Dumping data for table `reserved_words`
--

INSERT INTO `reserved_words` (`id`, `word`) VALUES
(1, 'and'),
(2, 'atau'),
(3, 'dan'),
(4, 'dari'),
(5, 'di'),
(6, 'itu'),
(7, 'ke'),
(8, 'nbsp'),
(9, 'or'),
(10, 'yang');

-- --------------------------------------------------------

--
-- Table structure for table `visited_links`
--

CREATE TABLE IF NOT EXISTS `visited_links` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `link` text NOT NULL,
  `title` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
