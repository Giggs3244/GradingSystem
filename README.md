# GradingSystem
ASP.NET Web Application - Web API - AngularJS - SQL Server - Token Based Authentication - CORS Support - ASP.NET Identity system - Owin middleware

# Instrucciones

* Abrir la solución en visual studio (v. 2015 o mayor)
* Cambiar el connectionStrings de nombre AuthContext de acuerdo a los datos de conexión de SQL Server propio, esto se realiza en el archivo Web.config
* Compilar el proyecto
* Si es la primera vez esperar a que se descarguen todas las librerías.
* Ejecutar el proyecto
* Abrir en el navegador la ruta: http://localhost:65313/
* Esta ruta puede cambiar, revisar el puerto asignado por IIS Express.
* Para registrar un usuario: http://localhost:65313/#/signup

* Ejecutar los siguientes queries del archivo: queries.sql
* Es importante para crear el stored procedure (para poder registrar en la tabla enrollments) y populate las tablas courses, tutors y students.

**NOTA:** Toda la configuración anterior es la que se utiliza por defecto, es decir, va a funcionar si los ambientes tienen la configuración por defecto, si no funciona usted debe buscar que la configuración corresponda a la que usted tiene en SQL Server y Visual Studio.

# File queries.sql

**Insert into Students**

INSERT INTO School.dbo.Students (School.dbo.Students.FirstName, School.dbo.Students.LastName, School.dbo.Students.Email) VALUES ('Stevens', 'Sandoval', 'stevens@gmail.com');

INSERT INTO School.dbo.Students (School.dbo.Students.FirstName, School.dbo.Students.LastName, School.dbo.Students.Email) VALUES ('Juan', 'Zapata', 'juan@hotmail.com');

INSERT INTO School.dbo.Students (School.dbo.Students.FirstName, School.dbo.Students.LastName, School.dbo.Students.Email) VALUES ('Mateo', 'Quintero', 'mateo@hotmail.com');

**Insert into Tutors**

INSERT INTO School.dbo.Tutors(School.dbo.Tutors.FirstName, School.dbo.Tutors.LastName, School.dbo.Tutors.Email) VALUES ('John', 'Quintero', 'john@hotmail.com');

INSERT INTO School.dbo.Tutors(School.dbo.Tutors.FirstName, School.dbo.Tutors.LastName, School.dbo.Tutors.Email) VALUES ('John', 'Ospina', 'john.ospina@hotmail.com');

INSERT INTO School.dbo.Tutors(School.dbo.Tutors.FirstName, School.dbo.Tutors.LastName, School.dbo.Tutors.Email) VALUES ('Julieth', 'Gomez', 'julieth@hotmail.com');

**Insert into Courses**

INSERT INTO School.dbo.Courses(School.dbo.Courses.Name, School.dbo.Courses.Duration, School.dbo.Courses.Description, School.dbo.Courses.TutorID) VALUES ('Matemáticas', 60, 'Curso básico de matemáticas', 1);

INSERT INTO School.dbo.Courses(School.dbo.Courses.Name, School.dbo.Courses.Duration, School.dbo.Courses.Description, School.dbo.Courses.TutorID) VALUES ('Física', 80, 'Curso básico de física', 2);

INSERT INTO School.dbo.Courses(School.dbo.Courses.Name, School.dbo.Courses.Duration, School.dbo.Courses.Description, School.dbo.Courses.TutorID) VALUES ('Artística', 100, 'Curso básico de artística', 3);

**Create Stored Procedure**

IF ( OBJECT_ID('dbo.sp_enrollment_insert') IS NOT NULL ) 

   DROP PROCEDURE dbo.sp_enrollment_insert
   
GO

CREATE PROCEDURE dbo.sp_enrollment_insert

       @student_id  INT,
       
	   @course_id   INT,
     
	   @description NVARCHAR(1000)       
     
AS 

BEGIN 

    SET NOCOUNT ON
    
    INSERT INTO School.dbo.Enrollments
    
		(
    
			School.dbo.Enrollments.StudentID, 
      
			School.dbo.Enrollments.CourseID, 
      
			School.dbo.Enrollments.Description
      
		) 
    
    VALUES 
    
        ( 
        
            @student_id,
            
            @course_id,
            
            @description
            
        ) 
        
END 

GO

**Para probar el SP**

DECLARE @student_id INT

DECLARE @course_id INT

DECLARE @description NVARCHAR(1000)

EXEC sp_enrollment_insert @student_id = 1, @course_id = 1, @description = 'Una descripción'
