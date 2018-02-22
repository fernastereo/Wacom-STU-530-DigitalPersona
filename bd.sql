CREATE DATABASE `notificador` /*!40100 DEFAULT CHARACTER SET latin1 */;

CREATE TABLE `notificaciones` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idexpediente` double DEFAULT NULL,
  `idsolicitante` varchar(45) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `tipo` int(11) DEFAULT NULL COMMENT 'S: Solicitante, V: Vecino, T: Tercero Interesado',
  `resolucion` varchar(45) DEFAULT NULL,
  `fecharesolucion` date DEFAULT NULL,
  `notificado` varchar(1) DEFAULT NULL COMMENT 'S/N',
  `fechanotificado` datetime DEFAULT NULL,
  `huellatmp` longblob COMMENT 'AQUI SE GUARDA EL TEMPLATE CON LA HUELLA PARA POSIBLES VERIFICACIONES BIOMETRICAS EN EL FUTURO',
  `huellaimg` longblob COMMENT 'AQUI SE GUARDA UNA IMAGEN PLANA DE LA HUELLA PARA MOSTRARLA EN CONSULTAS',
  `firma` longblob,
  `foto` longblob,
  `codseguridad` varchar(45) DEFAULT NULL,
  `qrcode` longblob,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=42992 DEFAULT CHARSET=latin1;


CREATE TABLE `tiponotificado` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(45) DEFAULT NULL,
  `estado` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

