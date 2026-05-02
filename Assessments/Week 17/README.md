# 🏦 LoanSphere – Loan Management System

LoanSphere is a full-stack **Loan Management System** built using **ASP.NET Core (Microservices Architecture)** and **MVC Frontend**.
It enables users to apply for loans, while Admins and Managers review and approve them with a structured workflow.

---

## 🚀 Features

### 👤 Authentication & Authorization

* JWT-based authentication
* Role-based access control (Customer, Admin, Manager)

### 📄 Loan Management

* Apply for loans
* Upload supporting documents (PDF)
* EMI calculation & schedule generation
* Loan status tracking (Pending / Approved / Rejected)

### 🧑‍💼 Role-Based Workflow

* **Customer** → Apply & track loans
* **Admin** → First-level approval/rejection
* **Manager** → Final approval/rejection

### 📊 Dashboard

* Customer dashboard (loan tracking, EMI info)
* Admin/Manager dashboard (all loan applications)

### 📁 Profile Management

* Create & update profile (PAN, Aadhaar)
* Profile completion tracking

### 💳 EMI System

* Auto EMI calculation
* EMI payment tracking

### 📂 Document Upload

* Upload PDF during loan application
* View document in loan details (embedded viewer)

---

## 🏗️ Architecture

LoanSphere follows a **Microservices Architecture**:

```text
Frontend (ASP.NET MVC)
        ↓
----------------------------------------
| LoanAuth API       (Authentication)   |
| LoanProfile API    (User Profile)     |
| LoanManagement API (Loan + EMI Logic) |
----------------------------------------
```

---

## 🛠️ Tech Stack

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* FluentValidation
* Serilog (Logging)

### Frontend

* ASP.NET Core MVC
* Razor Views
* Bootstrap / Custom CSS

---

## 📦 Project Structure

```
LoanSphere/
│
├── LoanSphere_Frontend/
├── LoanAuth/
├── LoanProfile/
├── LoanManagement/
│
└── README.md
```

---

## ⚙️ Setup Instructions

### 1️⃣ Clone the repository

```bash
git clone https://github.com/your-username/LoanSphere.git
cd LoanSphere
```

---

### 2️⃣ Configure Database

Update connection string in:

```json
appsettings.json
```

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=LoanSphereDB;Trusted_Connection=True;"
}
```

---

### 3️⃣ Run Migrations

```bash
dotnet ef database update
```

---

### 4️⃣ Trust HTTPS Certificate

```bash
dotnet dev-certs https --trust
```

---

### 5️⃣ Run Services

Run all APIs:

```bash
dotnet run --project LoanAuth
dotnet run --project LoanProfile
dotnet run --project LoanManagement
```

Run frontend:

```bash
dotnet run --project LoanSphere_Frontend
```

---

## 🔗 API Endpoints

### 🔐 LoanAuth

* `/api/auth/register/customer`
* `/api/auth/login`

### 📄 LoanProfile

* `/api/profile/create`
* `/api/profile/{userId}`

### 💰 LoanManagement

* `/loan/create`
* `/loan/getall`
* `/loan/user/{userId}`
* `/loan/{id}/decision`
* `/loan/pay-emi/{emiId}`

---

## 📊 Logging (Serilog)

Logs are stored in:

```
LoanManagement/Logs/log-YYYY-MM-DD.txt
```

Also visible in console during runtime.

---

## 🧪 Validation

* FluentValidation (Backend)
* Client-side validation (MVC)
* Request validation middleware

---

## ⚠️ Error Handling

* Global exception handling middleware
* Standardized API responses

---

## 🔐 Security

* JWT authentication
* Role-based authorization
* Secure API communication via HTTPS

---

## 📌 Future Enhancements

* Document verification system
* Payment gateway integration
* Email/SMS notifications
* Admin analytics dashboard
* Cloud deployment (Azure/AWS)

---

## ⭐ Contributing

Pull requests are welcome. For major changes, please open an issue first.

---

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/1.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/2.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/3.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/4.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/5.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/6.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/7.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/8.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/9.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/10.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/11.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/12.png)

![img alt](https://github.com/tahmidnoor/CU-DotNet-Jan26-B4/blob/344e0020ab4f6594e0fedc086fb2f0c582c1cb98/Assessments/Week%2017/Images/13.png)