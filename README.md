GymApp - Fitness Center Management System

GymApp es un sistema de gestión para gimnasios desarrollado en C# con .NET utilizando Visual Studio 2022 Community.
La aplicación fue construida siguiendo una arquitectura multicapa, con el objetivo de separar responsabilidades, mejorar la mantenibilidad del código y facilitar la escalabilidad del sistema.

El proyecto está compuesto por diferentes capas que gestionan la lógica del negocio, el acceso a datos, la transferencia de datos y la presentación mediante una aplicación web basada en ASP.NET MVC.

Tecnologías Utilizadas

C#

.NET

ASP.NET MVC

JavaScript

Visual Studio 2022 Community

Arquitectura multicapa (N-Layer Architecture)

Arquitectura del Proyecto

El sistema está dividido en cinco proyectos principales, cada uno con responsabilidades específicas.

API

La API gestiona la comunicación entre el cliente y la lógica del sistema mediante controladores.

Responsabilidades principales:

Manejo de solicitudes HTTP

Exposición de endpoints para operaciones del sistema

Comunicación con la capa de lógica de negocio

Los controladores reciben las solicitudes y delegan el procesamiento a la capa de Business Logic.

Business Logic

Esta capa contiene la lógica central del sistema y las reglas de negocio.

Incluye:

Clases de gestión (Management)

Validaciones de datos

Procesamiento de operaciones del sistema

Coordinación entre la API y la capa de acceso a datos

Su función es garantizar que las operaciones cumplan con las reglas del negocio antes de interactuar con la base de datos.

DTO (Data Transfer Objects)

Los DTO se utilizan para transportar datos entre las diferentes capas del sistema.

Ventajas:

Encapsulación de la información

Evita exponer directamente los modelos internos

Mejora la seguridad y la organización del código

Ejemplos de DTO:

UserDTO
MembershipDTO
WorkoutDTO
Data Access

La capa de acceso a datos se encarga de interactuar con la base de datos.

Componentes principales:

DAO (Data Access Objects)

Clases responsables de ejecutar las operaciones CRUD.

Operaciones implementadas:

Create

Read

Update

Delete

Mapper

El mapper convierte los datos entre:

Entidades del sistema

Objetos utilizados en la base de datos

Esto permite mantener independencia entre las capas.

MVC Application

La aplicación web se desarrolló utilizando el patrón Modelo Vista Controlador (MVC).

Estructura principal:

Controllers

Gestionan la comunicación entre las vistas y la lógica del sistema.

Models

Representan los datos que se utilizan en la aplicación.

Views

Contienen las interfaces de usuario que interactúan con el sistema.

JavaScript

Se utilizaron scripts de JavaScript en las vistas para mejorar la interacción del usuario y la experiencia en la interfaz.

Características del Sistema

Arquitectura multicapa organizada

Separación clara entre lógica de negocio y acceso a datos

API para comunicación entre componentes

Implementación completa de operaciones CRUD

Interfaz web basada en ASP.NET MVC

Integración de JavaScript en la interfaz

Estructura del Proyecto
GymApp
│
├── GymApp.API
│   └── Controllers
│
├── GymApp.BusinessLogic
│   └── Management
│
├── GymApp.DTO
│
├── GymApp.DataAccess
│   ├── DAO
│   └── Mapper
│
└── GymApp.MVC
    ├── Controllers
    ├── Models
    ├── Views
    └── Scripts
Objetivo del Proyecto

El objetivo de GymApp es demostrar la implementación de una arquitectura empresarial en .NET, separando claramente las responsabilidades del sistema en diferentes capas para lograr aplicaciones más organizadas, mantenibles y escalables.

Autor

Jefferson Gomez
