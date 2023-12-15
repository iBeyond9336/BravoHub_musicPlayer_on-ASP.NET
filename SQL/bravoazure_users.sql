-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: bravohub.mysql.database.azure.com    Database: bravoazure
-- ------------------------------------------------------
-- Server version	8.0.34

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `hashedpassword` varchar(255) DEFAULT NULL,
  `role` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (2,'qq','example@com','$2a$11$Ncsm5vALbnOOM9A8DHwJdee6NfSoLkXfXG7zE4N0mzGzXImD99HUa','user'),(3,'aa','example@com','$2a$11$cpi55zFZOHoXPPhFjsvUv.CpbZfL1wCpDCDuXQkHLfbU4katmy5e.','user'),(4,'rr','example@com','$2a$11$LVdHewApPCsDpdlUdBqhseyT/ByqTedkXH59AdB6ydZPDTnlCAA9q','user'),(5,'eed','example@com','$2a$11$K3tw6irxa951sfELPwhi9e2zJw/9FwSUP2D0CDAJNW71D9tBS/Y.K','user'),(6,'zz','example@com','$2a$11$CS3nw1IQ9LirkPBa6W/UO.cV7tAY1CGqQXQ66/p5NByyB4BGnabEm','user'),(8,'admin','admin@bavohub.com','$2a$11$qTG9U85GTb2zCxzG0zbD1.lRvg18CjyaB0ZCpFqdZjup4nK7FmUxG','admin'),(9,'admin111','example@com','$2a$11$qTG9U85GTb2zCxzG0zbD1.lRvg18CjyaB0ZCpFqdZjup4nK7FmUxG','user'),(10,'a','example@com','$2a$11$681/BN.OyRwybs69h1zh4u1xJJ7I2XJQ/yqeaK75t6YOpL8hCxVsC','user'),(11,'edwin123','example@com','$2a$11$tv.eae7/Tnx2T0LpldKnvO4xfjmpvwiG06Iy1MWR1VXhHOi3cNTD6','user'),(12,'vsTesting','example@com','$2a$11$yjlyrR/ccMMYE6gb5t/tWuIm9Ee8ISgDrQn9v3eMDjYGEXRP7MCwu','user'),(13,'1','example@com','$2a$11$1CUs.OdwRyjctAk7mjocluLN/VzI7qNsnBQW4rUqKEnijdT.Pkz9a','user');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-15 14:04:03
