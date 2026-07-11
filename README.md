# 🚗 Driving License Management System (DVLD)

<p align="center">

![C#](https://img.shields.io/badge/C%23-.NET_Framework-512BD4?style=for-the-badge&logo=csharp)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![ADO.NET](https://img.shields.io/badge/ADO.NET-Data_Access-blue?style=for-the-badge)
![Windows Forms](https://img.shields.io/badge/Windows-Forms-0078D6?style=for-the-badge)
![Architecture](https://img.shields.io/badge/Architecture-Three--Tier-success?style=for-the-badge)

</p>

---

# 📖 Overview

The **Driving License Management System (DVLD)** is a desktop application developed using **C#**, **Windows Forms**, **ADO.NET**, and **SQL Server** following a **Three-Tier Architecture**.

The system streamlines the process of managing driving license services, including applicants, licenses, driving tests, users, and application workflows through a structured and maintainable architecture.

---

# ✨ Features

## 👤 People Management

- Add new people
- Update person information
- Delete records
- Search by ID or National Number
- View detailed information

---

## 📝 Driving License Applications

- New Local Driving License Application
- International Driving License Application
- License Renewal
- Replace Lost License
- Replace Damaged License
- Release Detained License

---

## 🚘 License Management

- Issue Driving License
- Renew Existing License
- Detain License
- Release Detained License
- View License History

---

## 🧪 Driving Tests

- Schedule Vision Test
- Schedule Written Test
- Schedule Practical Test
- Record Test Results

---

## 👨‍💼 User Management

- User Authentication
- User Administration
- Permissions Management

---

## 🔍 Additional Features

- Application Search
- Driver Search
- Validation and Error Handling
- Modular User Controls
- Reusable Business Logic

---

# 🏗️ Project Architecture

```
Driving-License-Management-System
│
├── DVLD_PresentationLayer
│
├── DVLD_BusinessLayer
│
├── DVLD_DataAccessLayer
│
├── Database
│
└── Driving-License-Management-System.sln
```

The application follows a **Three-Tier Architecture**:

- **Presentation Layer** – Windows Forms User Interface
- **Business Layer** – Business Logic and Validation
- **Data Access Layer** – SQL Server Data Access using ADO.NET

---

# 🛠 Technologies Used

- C#
- .NET Framework
- Windows Forms
- SQL Server
- ADO.NET
- Three-Tier Architecture
- Object-Oriented Programming (OOP)

---

# 🚀 Getting Started

## 1. Clone the repository

```bash
git clone https://github.com/anasomar20/Driving-License-Management-System.git
```

## 2. Open the Solution

Open the solution using **Visual Studio 2022** (or a compatible version).

## 3. Restore the Database

Restore the SQL Server database from the backup located in the `Database` folder (or execute the SQL script if provided).

## 4. Configure the Connection String

Update the connection string in the application configuration file to match your SQL Server instance.

## 5. Build & Run

Build the solution and press:

```
Ctrl + F5
```

---

# 📷 Screenshots

## Login Screen

![Login](images/login.png)

---

## Main Dashboard

![Dashboard](images/dashboard.png)

---

## People Management

![People](images/people.png)

---

## License Applications

![Applications](images/applications.png)

---

## Driving Tests

![Tests](images/tests.png)

---

## License Management

![Licenses](images/licenses.png)

---

# 📚 Concepts Demonstrated

- Three-Tier Architecture
- Object-Oriented Programming (OOP)
- SOLID Principles
- ADO.NET
- CRUD Operations
- Authentication & Authorization
- Windows Forms Development
- SQL Server Integration
- Layered Architecture

---

# 🔮 Future Improvements

- Entity Framework Core
- ASP.NET Core Web API
- Online Appointment Booking
- Email Notifications
- Reporting Dashboard
- PDF Report Generation
- Role-Based Access Control (RBAC)

---

# 👨‍💻 Author

**Anas Omar**

Backend Developer

- C#
- ASP.NET Core
- SQL Server
- Entity Framework Core

---

# ⭐ Support

If you found this project useful, please consider giving it a ⭐ on GitHub.
