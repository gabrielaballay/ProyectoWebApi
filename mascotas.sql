-- phpMyAdmin SQL Dump
<<<<<<< HEAD
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 26-11-2019 a las 15:58:41
-- Versión del servidor: 10.1.35-MariaDB
-- Versión de PHP: 7.2.9
=======
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 19-11-2019 a las 03:59:28
-- Versión del servidor: 10.4.6-MariaDB
-- Versión de PHP: 7.3.8
>>>>>>> 347700ac7e0f3e361b2eed4be3e4c6994a987b4a

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
-- Estructura de tabla para la tabla `encontrada`
--

CREATE TABLE `encontrada` (
  `EncontradaId` int(11) NOT NULL,
  `Foto` varchar(40) NOT NULL,
  `Descripcion` text NOT NULL,
  `Fecha` datetime NOT NULL,
  `LocalizacionId` int(11) NOT NULL,
  `MascotaId` int(11) NOT NULL,
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `localizacion`
--

CREATE TABLE `localizacion` (
  `LocalizacionId` int(11) NOT NULL,
  `Latitud` decimal(10,0) NOT NULL,
  `Longitud` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mascotas`
--

CREATE TABLE `mascotas` (
  `MascotaId` int(11) NOT NULL,
  `NombreMascota` varchar(40) NOT NULL,
  `Raza` varchar(30) NOT NULL,
  `Tamaño` varchar(30) NOT NULL,
  `Edad` int(11) NOT NULL,
  `Descripcion` text NOT NULL,
  `Foto` varchar(40) NOT NULL,
  `Estado` varchar(30) NOT NULL,
  `Fecha` datetime NOT NULL,
  `RecompensaId` int(11) DEFAULT NULL,
  `PerdidoEn` int(11) NOT NULL,
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `provincia`
--

CREATE TABLE `provincia` (
  `ProvinciaId` int(11) NOT NULL,
  `NombreProvincia` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `provincia`
--

INSERT INTO `provincia` (`ProvinciaId`, `NombreProvincia`) VALUES
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
-- Estructura de tabla para la tabla `recompensa`
--

CREATE TABLE `recompensa` (
  `RecompensaId` int(11) NOT NULL,
  `Monto` decimal(10,0) NOT NULL,
  `Tiempo` int(11) NOT NULL,
  `Estado` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `UsuarioId` int(11) NOT NULL,
  `Apellido` varchar(30) NOT NULL,
  `Nombre` varchar(30) NOT NULL,
<<<<<<< HEAD
  `Ciudad` varchar(40) NOT NULL,
=======
>>>>>>> 347700ac7e0f3e361b2eed4be3e4c6994a987b4a
  `Direccion` varchar(45) NOT NULL,
  `Telefono` varchar(30) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Clave` varchar(100) NOT NULL,
  `ProvinciaId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `usuarios`
--

<<<<<<< HEAD
INSERT INTO `usuarios` (`UsuarioId`, `Apellido`, `Nombre`, `Ciudad`, `Direccion`, `Telefono`, `Email`, `Clave`, `ProvinciaId`) VALUES
(12, 'lopez', 'juan jose', '', '9 de julio 810', '2664334455', 'juanlopez@mail.com', '1234', 1),
(13, 'sosa', 'maria', '', 'alla 123', '2664444444', 'sosamaria@mail.com', '1234', 4),
(15, 'Lopez', 'Juan', 'San Luis', 'Aca 123', '2664-000000', 'Juan@lopez.com', '1234', 18);
=======
INSERT INTO `usuarios` (`UsuarioId`, `Apellido`, `Nombre`, `Direccion`, `Telefono`, `Email`, `Clave`, `ProvinciaId`) VALUES
(12, 'lopez', 'juan jose', '9 de julio 810', '2664334455', 'juanlopez@mail.com', '1234', 1),
(13, 'sosa', 'maria', 'alla 123', '2664444444', 'sosamaria@mail.com', '1234', 4);
>>>>>>> 347700ac7e0f3e361b2eed4be3e4c6994a987b4a

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `encontrada`
--
ALTER TABLE `encontrada`
  ADD PRIMARY KEY (`EncontradaId`),
  ADD KEY `LocalizacionId` (`LocalizacionId`),
  ADD KEY `MascotaId` (`MascotaId`),
  ADD KEY `UsuarioId` (`UsuarioId`);

--
-- Indices de la tabla `localizacion`
--
ALTER TABLE `localizacion`
  ADD PRIMARY KEY (`LocalizacionId`);

--
-- Indices de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD PRIMARY KEY (`MascotaId`),
  ADD KEY `RecompensaId` (`RecompensaId`,`PerdidoEn`),
  ADD KEY `UsuarioId` (`UsuarioId`),
  ADD KEY `PerdidoEn` (`PerdidoEn`);

--
-- Indices de la tabla `provincia`
--
ALTER TABLE `provincia`
  ADD PRIMARY KEY (`ProvinciaId`);

--
-- Indices de la tabla `recompensa`
--
ALTER TABLE `recompensa`
  ADD PRIMARY KEY (`RecompensaId`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`UsuarioId`),
  ADD KEY `ProvinciaId` (`ProvinciaId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `encontrada`
--
ALTER TABLE `encontrada`
  MODIFY `EncontradaId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `localizacion`
--
ALTER TABLE `localizacion`
  MODIFY `LocalizacionId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  MODIFY `MascotaId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `provincia`
--
ALTER TABLE `provincia`
  MODIFY `ProvinciaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `recompensa`
--
ALTER TABLE `recompensa`
  MODIFY `RecompensaId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
<<<<<<< HEAD
  MODIFY `UsuarioId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
=======
  MODIFY `UsuarioId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
>>>>>>> 347700ac7e0f3e361b2eed4be3e4c6994a987b4a

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `encontrada`
--
ALTER TABLE `encontrada`
  ADD CONSTRAINT `encontrada_ibfk_1` FOREIGN KEY (`MascotaId`) REFERENCES `mascotas` (`MascotaId`),
  ADD CONSTRAINT `encontrada_ibfk_2` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`UsuarioId`);

--
-- Filtros para la tabla `localizacion`
--
ALTER TABLE `localizacion`
  ADD CONSTRAINT `localizacion_ibfk_1` FOREIGN KEY (`LocalizacionId`) REFERENCES `encontrada` (`LocalizacionId`);

--
-- Filtros para la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD CONSTRAINT `mascotas_ibfk_1` FOREIGN KEY (`RecompensaId`) REFERENCES `recompensa` (`RecompensaId`),
  ADD CONSTRAINT `mascotas_ibfk_2` FOREIGN KEY (`PerdidoEn`) REFERENCES `localizacion` (`LocalizacionId`),
  ADD CONSTRAINT `mascotas_ibfk_3` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`UsuarioId`);

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `usuarios_ibfk_1` FOREIGN KEY (`ProvinciaId`) REFERENCES `provincia` (`ProvinciaId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
