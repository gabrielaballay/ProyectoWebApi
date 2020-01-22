-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 09-01-2020 a las 23:16:35
-- Versión del servidor: 10.1.35-MariaDB
-- Versión de PHP: 7.2.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `mascotas`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `encontradas`
--

CREATE TABLE `encontradas` (
  `EncontradaId` int(11) NOT NULL,
  `Foto` varchar(70) NOT NULL,
  `Descripcion` text NOT NULL,
  `Fecha` varchar(30) NOT NULL,
  `Estado` int(11) NOT NULL,
  `UbicacionId` int(11) NOT NULL,
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mascotas`
--

CREATE TABLE `mascotas` (
  `MascotaId` int(11) NOT NULL,
  `NombreMascota` varchar(40) NOT NULL,
  `Raza` varchar(30) NOT NULL,
  `Tamanio` varchar(30) NOT NULL,
  `Edad` varchar(40) NOT NULL,
  `Descripcion` text NOT NULL,
  `Foto` varchar(40) NOT NULL,
  `Estado` int(11) NOT NULL,
  `Fecha` varchar(30) NOT NULL,
  `UbicacionId` int(11) NOT NULL,
  `RecompensaId` int(11) DEFAULT NULL,
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `provincias`
--

CREATE TABLE `provincias` (
  `ProvinciaId` int(11) NOT NULL,
  `NombreProvincia` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `provincias`
--

INSERT INTO `provincias` (`ProvinciaId`, `NombreProvincia`) VALUES
(1, 'Buenos Aires'),
(2, 'Catamarca'),
(3, 'Chacos'),
(4, 'Chubut'),
(5, 'Córdoba'),
(6, 'Corrientes'),
(7, 'Entre Ríos'),
(8, 'Formosa'),
(9, 'Jujuy'),
(10, 'La Pampa'),
(11, 'La Rioja'),
(12, 'Mendoza'),
(13, 'Misiones'),
(14, 'Neuquén'),
(15, 'Río Negro'),
(16, 'Salta'),
(17, 'San Juan'),
(18, 'San Luis'),
(19, 'Santa Cruz'),
(20, 'Santa Fe'),
(21, 'Santiago del Estero'),
(22, 'Tierra del Fuego'),
(23, 'Tucumán');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `recompensas`
--

CREATE TABLE `recompensas` (
  `RecompensaId` int(11) NOT NULL,
  `Monto` decimal(10,0) NOT NULL,
  `Tiempo` varchar(40) NOT NULL,
  `Estado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ubicaciones`
--

CREATE TABLE `ubicaciones` (
  `UbicacionId` int(11) NOT NULL,
  `Latitud` varchar(20) NOT NULL,
  `Longitud` varchar(20) NOT NULL,
  `zona` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `UsuarioId` int(11) NOT NULL,
  `Apellido` varchar(30) NOT NULL,
  `Nombre` varchar(30) NOT NULL,
  `Ciudad` varchar(40) NOT NULL,
  `Telefono` varchar(30) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Clave` varchar(100) NOT NULL,
  `Estado` int(11) NOT NULL,
  `Confirma` varchar(100) NOT NULL,
  `ProvinciaId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `encontradas`
--
ALTER TABLE `encontradas`
  ADD PRIMARY KEY (`EncontradaId`),
  ADD KEY `UsuarioId` (`UsuarioId`),
  ADD KEY `UbicacionId` (`UbicacionId`);

--
-- Indices de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD PRIMARY KEY (`MascotaId`),
  ADD KEY `RecompensaId` (`RecompensaId`),
  ADD KEY `UsuarioId` (`UsuarioId`),
  ADD KEY `UbicacionId` (`UbicacionId`);

--
-- Indices de la tabla `provincias`
--
ALTER TABLE `provincias`
  ADD PRIMARY KEY (`ProvinciaId`);

--
-- Indices de la tabla `recompensas`
--
ALTER TABLE `recompensas`
  ADD PRIMARY KEY (`RecompensaId`);

--
-- Indices de la tabla `ubicaciones`
--
ALTER TABLE `ubicaciones`
  ADD PRIMARY KEY (`UbicacionId`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`UsuarioId`),
  ADD UNIQUE KEY `Email` (`Email`),
  ADD KEY `ProvinciaId` (`ProvinciaId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `encontradas`
--
ALTER TABLE `encontradas`
  MODIFY `EncontradaId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  MODIFY `MascotaId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `provincias`
--
ALTER TABLE `provincias`
  MODIFY `ProvinciaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `recompensas`
--
ALTER TABLE `recompensas`
  MODIFY `RecompensaId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ubicaciones`
--
ALTER TABLE `ubicaciones`
  MODIFY `UbicacionId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `UsuarioId` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `encontradas`
--
ALTER TABLE `encontradas`
  ADD CONSTRAINT `encontradas_ibfk_2` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`UsuarioId`),
  ADD CONSTRAINT `encontradas_ibfk_3` FOREIGN KEY (`UbicacionId`) REFERENCES `ubicaciones` (`UbicacionId`);

--
-- Filtros para la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD CONSTRAINT `mascotas_ibfk_1` FOREIGN KEY (`RecompensaId`) REFERENCES `recompensas` (`RecompensaId`),
  ADD CONSTRAINT `mascotas_ibfk_3` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`UsuarioId`),
  ADD CONSTRAINT `mascotas_ibfk_4` FOREIGN KEY (`UbicacionId`) REFERENCES `ubicaciones` (`UbicacionId`);

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `usuarios_ibfk_1` FOREIGN KEY (`ProvinciaId`) REFERENCES `provincias` (`ProvinciaId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
