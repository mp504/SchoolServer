# School Management System

This solution is a comprehensive School Management System that includes three main projects: an API, an MVC application, and an Angular application. The system supports three types of users: students, teachers, and administrators. Each user type has different levels of access and functionality.

## Projects

### 1. SchoolServer Project (API)

The SchoolServer project is a .NET 8 API that provides the backend functionality for the School Management System. It uses Entity Framework Core for data access, Identity for user authentication and authorization, and JWT for secure token-based authentication.

#### Features

- **User Authentication and Authorization**: 
  - JWT-based login and registration for students, teachers, and administrators.
  - Role-based access control to ensure that users have appropriate permissions.

- **Course Management**:
  - CRUD operations for courses.
  - Students can enroll in or unenroll from courses.
  - Teachers can also enroll in or unenroll from courses, with additional permissions to manage course content.

- **Endpoints**:
  
  - `/api/account/register`: Register a new user.
- `/api/account/login`: Login and receive a JWT token.
-  `/api/courses`: Get a list of all courses.
- `/api/courses/{id}`: Get, update, or delete a specific course.
- `/api/courses/enroll`: Enroll a student or teacher in a course.
- `/api/courses/unenroll`: Unenroll a student or teacher from a course.
- `/api/students`: Get a list of all students.
- `/api/students/{id}`: Get a specific student along with their courses.
- `/api/students`: Register a new student.
- `/api/students/{studentId}/courses/{courseId}`: Enroll a student in a course.
- `/api/students/{studentId}/courses/{courseId}`: Unenroll a student from a course.
- `/api/students/{id}`: Delete a specific student.
- `/api/teachers`: Get a list of all teachers.
- `/api/teachers/{id}`: Get a specific teacher along with their courses.
- `/api/teachers`: Register a new teacher.
- `/api/teachers/{teacherId}/courses/{courseId}`: Enroll a teacher in a course.
- `/api/teachers/{teacherId}/courses/{courseId}`: Unenroll a teacher from a course.
- `/api/teachers/{id}`: Delete a specific teacher.



### 2. MVC Project

The MVC project is a .NET 8 web application that provides a user interface for the School Management System. It uses HTML, CSS, JavaScript, and C# to create a dynamic and responsive web application.

#### Features

- **User Authentication**:
  - Login and registration pages for students, teachers, and administrators.

- **Dashboards**:
  - Student Dashboard: Allows students to view and manage their enrolled courses.
  - Teacher Dashboard: Allows teachers to view and manage their courses and students.

- **Course Management**:
  - CRUD operations for courses through the web interface.
  - Enroll and unenroll functionality for students and teachers.

### 3. Angular Project

The Angular project is a single-page application (SPA) that provides a modern and responsive user interface for the School Management System. It uses HTML, CSS, JavaScript, and TypeScript to create a dynamic and interactive web application.

#### Features

- **User Authentication**:
  - Login and registration pages for students, teachers, and administrators.

- **Dashboards**:
  - Student Dashboard: Allows students to view and manage their enrolled courses.
  - Teacher Dashboard: Allows teachers to view and manage their courses and students.

- **Course Management**:
  - CRUD operations for courses through the web interface.
  - Enroll and unenroll functionality for students and teachers.

## Technologies Used

- **Backend**:
  - .NET 8
  - Entity Framework Core
  - Identity
  - JWT

- **Frontend**:
  - HTML
  - CSS
  - JavaScript
  - TypeScript
  - Angular
  - ASP.NET MVC

## Getting Started

### Prerequisites

- .NET 8 SDK
- Node.js (for Angular project)
- SQL Server (or any other supported database)

### Installation

1. Clone the repository:


2. Navigate to the API project directory and restore dependencies:

3. Update the database:

4. Run the API:
   
5. Navigate to the MVC project directory and restore dependencies:
   
6. Run the MVC application:
   
7. Navigate to the Angular project directory and install dependencies:
   
8. Run the Angular application:
   
## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes or improvements.

## License
You can use it anywhere.

## completion 
this project is not done Yet.
