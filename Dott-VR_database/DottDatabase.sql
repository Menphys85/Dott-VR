-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le : ven. 14 oct. 2022 à 23:56
-- Version du serveur : 10.4.24-MariaDB
-- Version de PHP : 8.1.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

--
-- Base de données : `dott-db`
--

-- --------------------------------------------------------

--
-- Structure de la table `era`
--

CREATE TABLE `era` (
  `id` int(11) NOT NULL,
  `name` varchar(7) NOT NULL,
  `game_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `game`
--

CREATE TABLE `game` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `isNew` tinyint(1) DEFAULT NULL,
  `last_save` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `grapable_object`
--

CREATE TABLE `grapable_object` (
  `id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL,
  `era_id` int(11) NOT NULL,
  `position` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL,
  `rotation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`rotation`))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `npc`
--

CREATE TABLE `npc` (
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
-- Index pour la table `grapable_object`
--
ALTER TABLE `grapable_object`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Era_foreignerKey` (`era_id`);

--
-- Index pour la table `npc`
--
ALTER TABLE `npc`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_idEra_npc` (`era_id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `era`
--
ALTER TABLE `era`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=57;

--
-- AUTO_INCREMENT pour la table `game`
--
ALTER TABLE `game`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT pour la table `grapable_object`
--
ALTER TABLE `grapable_object`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=473;

--
-- AUTO_INCREMENT pour la table `npc`
--
ALTER TABLE `npc`
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
