# University-Management-System
A robust, full-stack enterprise web application designed to streamline academic administration. This project demonstrates a decoupled architecture, sharing core business logic between a dynamic ASP.NET Core MVC front-end and a scalable Web API layer.

🚀 Key Features
Complete Academic Management: Full CRUD operations to manage Courses, Students, Instructors, and Enrollments.

Dual-Layer Architecture: Unified business logic serving both an interactive MVC web interface and a RESTful Web API.

Advanced Data Modeling: Robust data persistence handling complex database relationships (One-to-Many and Many-to-Many).

Dynamic UI & UX: Seamless form handling, partial views, and asynchronous updates using Ajax.

🛠️ Tech Stack & Architecture
Backend Framework: ASP.NET Core MVC / Web API (C#)

Database & ORM: SQL Server via Entity Framework Core (EF Core)

Design Patterns: Repository Pattern, Dependency Injection (DI)

Frontend Enhancements: ViewModels, Partial Views, Razor Views, Ajax, Client-side & Custom Validation

📐 Deep Dive: Implementation Details
Data Architecture (EF Core)
Designed and implemented a relational database schema utilizing Entity Framework Core code-first approach:

One-to-Many Relationships: (e.g., Department to Courses / Instructors).

Many-to-Many Relationships: Managed via explicit join tables to accurately handle Student-Course enrollments and Instructor-Course assignments.

MVC Implementation
Built a responsive and secure user interface utilizing core MVC features:

State Management: Strategic use of ViewData, ViewBag, and strongly-typed ViewModels to safely pass data between controllers and views.

Form & Validation Integrity: Implemented rigorous client-side and backend custom validation attributes to ensure data compliance before persistence.

Asynchronous UI: Used Partial Views and Ajax to refresh specific UI components without triggering full-page reloads, minimizing server overhead.

API Layer & Clean Architecture
RESTful Endpoints: Developed full GET, POST, PUT, and DELETE HTTP endpoints.

Decoupled Logic: Utilized the Repository Pattern and native Dependency Injection containers to isolate data access logic. This allowed both the MVC controllers and API controllers to share identical business logic seamlessly, eliminating code duplication.
