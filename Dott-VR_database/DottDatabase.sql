-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : jeu. 27 oct. 2022 à 21:49
-- Version du serveur : 10.4.24-MariaDB
-- Version de PHP : 8.1.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

--
-- Base de données : `dott-db`
--
CREATE DATABASE IF NOT EXISTS `dott-db` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `dott-db`;

-- --------------------------------------------------------

--
-- Structure de la table `era`
--

DROP TABLE IF EXISTS `era`;
CREATE TABLE IF NOT EXISTS `era` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(7) NOT NULL,
  `game_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `game_foreignerkey` (`game_id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `era`
--

INSERT INTO `era` (`id`, `name`, `game_id`) VALUES
(60, 'Present', 34),
(61, 'Past', 34),
(62, 'Futur', 34),
(63, 'Present', 35),
(64, 'Past', 35),
(65, 'Futur', 35);

-- --------------------------------------------------------

--
-- Structure de la table `game`
--

DROP TABLE IF EXISTS `game`;
CREATE TABLE IF NOT EXISTS `game` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  `isNew` tinyint(1) DEFAULT NULL,
  `last_save` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  UNIQUE KEY `GameName_unique` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `game`
--

INSERT INTO `game` (`id`, `name`, `isNew`, `last_save`) VALUES
(34, 'Partie 34', 0, '2022-10-15 13:11:49'),
(35, 'Partie 35', 0, '2022-10-15 16:43:27');

-- --------------------------------------------------------

--
-- Structure de la table `grapable_object`
--

DROP TABLE IF EXISTS `grapable_object`;
CREATE TABLE IF NOT EXISTS `grapable_object` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  `era_id` int(11) NOT NULL,
  `position` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL,
  `rotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`rotation`)),
  PRIMARY KEY (`id`),
  KEY `Era_foreignerKey` (`era_id`)
) ENGINE=InnoDB AUTO_INCREMENT=506 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `grapable_object`
--

INSERT INTO `grapable_object` (`id`, `name`, `era_id`, `position`, `rotation`) VALUES
(483, 'Bouteille', 61, '{\"x\":-1.6289677619934082,\"y\":1.2060149908065796,\"z\":-1.9377691745758057}', '{\"x\":-8.8514138951723e-7,\"y\":8.583820090279914e-7,\"z\":0.00001100582903745817,\"w\":1}'),
(484, 'Chapeau de cowboy', 60, '{\"x\":-2.804777145385742,\"y\":0.6190000176429749,\"z\":0.28299999237060547}', '{\"x\":0.06311273574829102,\"y\":0,\"z\":0,\"w\":0.9980064630508423}'),
(485, 'Chapeau de sorcière', 60, '{\"x\":-2.13863468170166,\"y\":1.5410000085830688,\"z\":0.5640000104904175}', '{\"x\":-0.6286149024963379,\"y\":0,\"z\":0,\"w\":0.7777167558670044}'),
(487, 'Remote', 60, '{\"x\":-5.405630111694336,\"y\":0.5929999947547913,\"z\":-0.8597723841667175}', '{\"x\":0,\"y\":0,\"z\":0,\"w\":1}'),
(488, 'Stylo', 60, '{\"x\":0.0766599178314209,\"y\":1.2350000143051147,\"z\":-2.085353374481201}', '{\"x\":0,\"y\":0,\"z\":0,\"w\":1}'),
(490, 'Livre', 61, '{\"x\":10.676799774169922,\"y\":-1112.2913818359375,\"z\":-19.42743492126465}', '{\"x\":0.44242507219314575,\"y\":0.6431643962860107,\"z\":0.0902615413069725,\"w\":-0.6184273958206177}'),
(491, 'Parchemin', 62, '{\"x\":-0.44363757967948914,\"y\":-1109.2120361328125,\"z\":-0.2991211414337158}', '{\"x\":0.4233497679233551,\"y\":0.728981614112854,\"z\":0.41753190755844116,\"w\":-0.3391576111316681}'),
(492, 'Carte magnétique', 62, '{\"x\":0.6499491333961487,\"y\":0.0015998691087588668,\"z\":-1.0938512086868286}', '{\"x\":-0.955475389957428,\"y\":-0.0000132342220240389,\"z\":-0.2950710356235504,\"w\":0.00000993871708487859}'),
(493, 'Clé sécurisée', 60, '{\"x\":-4.300000190734863,\"y\":1,\"z\":-4.300000190734863}', '{\"x\":0,\"y\":0,\"z\":0,\"w\":0.8999999761581421}'),
(494, 'Bouteille', 64, '{\"x\":-1.6289677619934082,\"y\":1.2060149908065796,\"z\":-1.9377691745758057}', '{\"x\":-8.8514138951723e-7,\"y\":8.583820090279914e-7,\"z\":0.00001100582903745817,\"w\":1}'),
(495, 'Chapeau de cowboy', 63, '{\"x\":-2.804777145385742,\"y\":0.6190000176429749,\"z\":0.28299999237060547}', '{\"x\":0.06311273574829102,\"y\":0,\"z\":0,\"w\":0.9980064630508423}'),
(496, 'Chapeau de sorcière', 63, '{\"x\":-2.13863468170166,\"y\":1.5410000085830688,\"z\":0.5640000104904175}', '{\"x\":-0.6286149024963379,\"y\":0,\"z\":0,\"w\":0.7777167558670044}'),
(498, 'Remote', 63, '{\"x\":-5.405630111694336,\"y\":0.5929999947547913,\"z\":-0.8597723841667175}', '{\"x\":0,\"y\":0,\"z\":0,\"w\":1}'),
(499, 'Stylo', 63, '{\"x\":0.0766599178314209,\"y\":1.2350000143051147,\"z\":-2.085353374481201}', '{\"x\":0,\"y\":0,\"z\":0,\"w\":1}'),
(501, 'Livre', 64, '{\"x\":7.865478038787842,\"y\":-5204.66162109375,\"z\":-9.818155288696289}', '{\"x\":-0.3423082232475281,\"y\":-0.5917893052101135,\"z\":0.6202224493026733,\"w\":0.38462287187576294}'),
(503, 'Parchemin', 65, '{\"x\":23.44167709350586,\"y\":-39455.75390625,\"z\":-1.9149973392486572}', '{\"x\":-0.07474078238010406,\"y\":0.4860403835773468,\"z\":0.3969985246658325,\"w\":0.7749649882316589}'),
(504, 'Carte magnétique', 65, '{\"x\":1.7153282165527344,\"y\":0.0015999635215848684,\"z\":-0.11811469495296478}', '{\"x\":0.0000012954486692251521,\"y\":0.08243370801210403,\"z\":0.0000017339139048999641,\"w\":0.9965965747833252}'),
(505, 'Clé sécurisée', 63, '{\"x\":-4.300000190734863,\"y\":1,\"z\":-4.300000190734863}', '{\"x\":0,\"y\":0,\"z\":0,\"w\":0.8999999761581421}');

-- --------------------------------------------------------

--
-- Structure de la table `npc`
--

DROP TABLE IF EXISTS `npc`;
CREATE TABLE IF NOT EXISTS `npc` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  `era_id` int(11) NOT NULL,
  `position` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`position`)),
  `rotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`rotation`)),
  PRIMARY KEY (`id`),
  KEY `fk_idEra_npc` (`era_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `npc`
--

INSERT INTO `npc` (`id`, `name`, `era_id`, `position`, `rotation`) VALUES
(6, 'John Doeuf', 61, '{\"x\":0.31769225001335144,\"y\":0,\"z\":-4.632556915283203}', '{\"x\":0.035539411008358,\"y\":0.8139964938163757,\"z\":0.025289401412010193,\"w\":0.5792297720909119}'),
(7, 'Hervé Concombre', 62, '{\"x\":-6.914870262145996,\"y\":-0.08155876398086548,\"z\":-0.11562111973762512}', '{\"x\":-0.03639595955610275,\"y\":0.997980535030365,\"z\":-0.03499125689268112,\"w\":0.038547247648239136}'),
(8, 'John Doeuf', 64, '{\"x\":2.106656789779663,\"y\":0,\"z\":-5.090189456939697}', '{\"x\":0.03144100308418274,\"y\":0.7201212644577026,\"z\":0.03023398295044899,\"w\":0.6924757957458496}'),
(9, 'Hervé Concombre', 65, '{\"x\":-6.914870262145996,\"y\":-0.08155876398086548,\"z\":-0.11562111973762512}', '{\"x\":-0.03639595955610275,\"y\":0.997980535030365,\"z\":-0.03499125689268112,\"w\":0.038547247648239136}');

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `era`
--
ALTER TABLE `era`
  ADD CONSTRAINT `game_foreignerkey` FOREIGN KEY (`game_id`) REFERENCES `game` (`id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `grapable_object`
--
ALTER TABLE `grapable_object`
  ADD CONSTRAINT `Era_foreignerKey` FOREIGN KEY (`era_id`) REFERENCES `era` (`id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `npc`
--
ALTER TABLE `npc`
  ADD CONSTRAINT `fk_idEra_npc` FOREIGN KEY (`era_id`) REFERENCES `era` (`id`) ON DELETE CASCADE;
COMMIT;
