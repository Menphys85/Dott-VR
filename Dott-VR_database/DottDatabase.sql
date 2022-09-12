-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : jeu. 08 sep. 2022 à 22:26
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

CREATE TABLE `era` (
  `id` int(11) NOT NULL,
  `name` varchar(7) NOT NULL,
  `game_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `era`
--

INSERT INTO `era` (`id`, `name`, `game_id`) VALUES
(6, 'Present', 4),
(7, 'Futur', 4),
(8, 'Past', 5),
(9, 'Present', 5),
(10, 'Futur', 5),
(11, 'past', 4);

-- --------------------------------------------------------

--
-- Structure de la table `game`
--

CREATE TABLE `game` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `last_save` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `game`
--

INSERT INTO `game` (`id`, `name`, `last_save`) VALUES
(4, 'Partie 1', '2022-09-08 17:51:40'),
(5, 'Partie 2', '2022-09-08 17:51:40');

-- --------------------------------------------------------

--
-- Structure de la table `prehensile_object`
--

CREATE TABLE `prehensile_object` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `era_id` int(11) NOT NULL,
  `position` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`position`)),
  `rotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`rotation`))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `era`
--
ALTER TABLE `era`
  ADD PRIMARY KEY (`id`),
  ADD KEY `game_foreignerkey` (`game_id`);

--
-- Index pour la table `game`
--
ALTER TABLE `game`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `GameName_unique` (`name`);

--
-- Index pour la table `prehensile_object`
--
ALTER TABLE `prehensile_object`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Era_foreignerKey` (`era_id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `era`
--
ALTER TABLE `era`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT pour la table `game`
--
ALTER TABLE `game`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT pour la table `prehensile_object`
--
ALTER TABLE `prehensile_object`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `era`
--
ALTER TABLE `era`
  ADD CONSTRAINT `game_foreignerkey` FOREIGN KEY (`game_id`) REFERENCES `game` (`id`) ON DELETE CASCADE;

--
-- Contraintes pour la table `prehensile_object`
--
ALTER TABLE `prehensile_object`
  ADD CONSTRAINT `Era_foreignerKey` FOREIGN KEY (`era_id`) REFERENCES `era` (`id`) ON DELETE CASCADE;


COMMIT;
