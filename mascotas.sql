-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 22-12-2019 a las 15:20:11
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
  `Foto` varchar(40) NOT NULL,
  `Descripcion` text NOT NULL,
  `Fecha` varchar(30) NOT NULL,
  `Lugar` varchar(40) NOT NULL,
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `encontradas`
--

INSERT INTO `encontradas` (`EncontradaId`, `Foto`, `Descripcion`, `Fecha`, `Lugar`, `UsuarioId`) VALUES
(1, 'pet_frGW6vvi.jpg', 'otra cosa ', '6/12/2019', 'aca', 13),
(2, 'pet_QlFvgENZ.jpg', 'otra cosa ', '6/12/2019', 'otro', 13),
(3, 'pet_k9lKNwxE.jpg', 'a ver que sale ', '6/12/2019', 'segunda prueba', 13),
(4, 'pet_PXl7zvGi.jpg', 'hjkkutf', '4/12/2019', 'hola', 13),
(5, 'pet_x4bPH6Lb.jpg', 'esta alla', '6/12/2019', 'las chacras', 13),
(6, 'pet_2vFeNtPF.jpg', 'sfghj', '6/12/2019', 'gghj', 13),
(7, 'pet_sGrU9Lts.jpg', ' fghjkk', '6/12/2019', 'fggh', 13),
(8, 'pet_bsSgbpkU.jpg', 'tuuu', '6/12/2019', 'ghk', 13),
(9, 'pet_hldYd5a5.jpg', 'ghklll', '6/12/2019', 'hola', 13),
(10, 'pet_AtZpO6SH.jpg', 'gjll', '6/12/2019', 'asi', 13),
(11, 'pet_XeRQLgvA.jpg', 'hhjjj\ngghhjj\nfghh', '4/12/2019', 'aaaa', 13),
(12, 'pet_jfgtdzCB.jpg', 'rtuikdfh\nftuii', '7/12/2019', 'fhkll', 13);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mascotas`
--

CREATE TABLE `mascotas` (
  `MascotaId` int(11) NOT NULL,
  `NombreMascota` varchar(40) NOT NULL,
  `Raza` varchar(30) NOT NULL,
  `Tamanio` varchar(30) NOT NULL,
  `Edad` int(11) NOT NULL,
  `Descripcion` text NOT NULL,
  `Foto` varchar(40) NOT NULL,
  `Estado` int(11) NOT NULL,
  `Fecha` varchar(30) NOT NULL,
  `RecompensaId` int(11) DEFAULT NULL,
  `Lugar` varchar(40) NOT NULL,
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `mascotas`
--

INSERT INTO `mascotas` (`MascotaId`, `NombreMascota`, `Raza`, `Tamanio`, `Edad`, `Descripcion`, `Foto`, `Estado`, `Fecha`, `RecompensaId`, `Lugar`, `UsuarioId`) VALUES
(1, 'mono', 'dogo', 'mediano ', 2, 'blanco sin señas', 'pet_X7KTWMWb.jpg', 1, '5/12/2019', 1, 'las chacras ', 13),
(2, 'tigre', 'gato siames', 'chico', 3, 'ea color negro y muy amigable', 'pet_tfVsqyph.jpg', 1, '3/12/2019', 1, 'San Luis centro', 13),
(3, 'chiquito', 'galgo', 'mediano', 4, 'es atigrado muy amigable ', 'pet_ylytSCDS.jpg', 1, '2/12/2019', 1, 'barrio San martin', 12),
(4, 'leonsio', 'gato', 'chico', 6, 'color blanco y jugueton', 'pet_OBUUuW2Q.jpg', 1, '1/12/2019', 1, 'plaza del cerro', 12),
(5, 'guancho', 'pequines', 'chico', 4, 'se perdió ...', 'pet_lzHxHJG6.jpg', 1, '3/12/2019', 1, 'las runas', 13),
(6, 'mocho', 'caniche', 'chico', 3, 'ggjkll\nfghjkk\ngghhjj\n', 'pet_gkR2YSQT.jpg', 1, '5/12/2019', 1, 'San Luis centro', 13),
(7, 'bruno', 'gatito ', 'chico', 4, 'hjkkk\nrghj\n', 'pet_7iGOrcvw.jpg', 1, '5/12/2019', 1, 'barrio cgt', 13);

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

--
-- Volcado de datos para la tabla `recompensas`
--

INSERT INTO `recompensas` (`RecompensaId`, `Monto`, `Tiempo`, `Estado`) VALUES
(1, '0', '0', 0);

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
  `ProvinciaId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`UsuarioId`, `Apellido`, `Nombre`, `Ciudad`, `Telefono`, `Email`, `Clave`, `Estado`, `ProvinciaId`) VALUES
(12, 'lopez', 'juan jose', 'Villa mercedes ', '2664334455', 'juanlopez@mail.com', '1234', 1, 18),
(13, 'sosa', 'maria jose', 'San Luis', '266444343', 'sosamaria@mail.com', '1234', 1, 18),
(15, 'Rodriguez', 'Juan', 'San Luis', '2664-000000', 'rodriguezjuan@mail.com', '1234', 1, 18),
(16, 'Perez', 'Miguel', 'San Luis', '2664000010', 'perezmiguel@mail.com', '1234', 1, 18),
(17, 'Avalos', 'Laura', 'Villa Mercedes', '2664000012', 'avaloslaura@mail.com', '1234', 1, 18);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `encontradas`
--
ALTER TABLE `encontradas`
  ADD PRIMARY KEY (`EncontradaId`),
  ADD KEY `UsuarioId` (`UsuarioId`);

--
-- Indices de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD PRIMARY KEY (`MascotaId`),
  ADD KEY `RecompensaId` (`RecompensaId`),
  ADD KEY `UsuarioId` (`UsuarioId`);

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
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`UsuarioId`),
  ADD KEY `ProvinciaId` (`ProvinciaId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `encontradas`
--
ALTER TABLE `encontradas`
  MODIFY `EncontradaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  MODIFY `MascotaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `provincias`
--
ALTER TABLE `provincias`
  MODIFY `ProvinciaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT de la tabla `recompensas`
--
ALTER TABLE `recompensas`
  MODIFY `RecompensaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `UsuarioId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `encontradas`
--
ALTER TABLE `encontradas`
  ADD CONSTRAINT `encontradas_ibfk_2` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`UsuarioId`);

--
-- Filtros para la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD CONSTRAINT `mascotas_ibfk_1` FOREIGN KEY (`RecompensaId`) REFERENCES `recompensas` (`RecompensaId`),
  ADD CONSTRAINT `mascotas_ibfk_3` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`UsuarioId`);

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `usuarios_ibfk_1` FOREIGN KEY (`ProvinciaId`) REFERENCES `provincias` (`ProvinciaId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
