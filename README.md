Here is the English version of your `README.md` for GitHub:  

---

# **EnviosApp - Shipping and Provider Management System**  

## 📦 Project Description  
**EnviosApp** is a web application designed to manage air shipments, allowing administrators to handle providers, shipping zones, services, and customers. The platform facilitates the assignment of rates based on the zones defined by each provider and offers an intuitive interface for user and logistics operations management.  

## 🚀 Key Features  
- **User Management**  
  - Registration and authentication using JWT.  
  - Role-based access: *admin* and *user*.  
  - Full CRUD operations for user management (admin only).  

- **Provider and Service Management**  
  - Administration of providers and their specific services.  
  - Each provider is associated with different zones and countries.  
  - CRUD operations for providers, services, and zones.  

- **Shipment Management (Waybills)**  
  - Creation and editing of air waybills with multiple packages.  
  - Association of shipments with customers and providers.  
  - Automatic rate calculation based on zones and services.  

## 🛠️ Technologies Used  
- **Backend:** ASP.NET Core with Entity Framework and LINQ.  
- **Frontend:** HTML, CSS, JavaScript, Bootstrap.  
- **Database:** SQL Server.  
- **Authentication & Security:** JWT with role-based policies.  

## 📖 Installation and Setup  
### **1. Clone the Repository**  
```bash
git clone https://github.com/your-username/EnviosApp.git
cd EnviosApp
```

### **2. Configure the Backend**  
- Ensure that SQL Server is running.  
- Set up the database connection string in `appsettings.json`.  
- Run the migrations:  
  ```bash
  dotnet ef database update
  ```
- Start the API:  
  ```bash
  dotnet run
  ```

### **3. Configure the Frontend**  
- Open the `index.html` file in a browser.  
- If necessary, configure the API base URL in `js/config.js`.  

## 🔑 Access and Authentication  
- An **admin** user can manage providers, users, and settings.  
- A **standard user** can only create and view shipments.  

## 🛠️ Contributing  
If you want to contribute:  
1. Fork the repository.  
2. Create a new branch (`git checkout -b feature/new-feature`).  
3. Commit your changes (`git commit -m 'Added new feature'`).  
4. Push to the branch (`git push origin feature/new-feature`).  
5. Open a Pull Request.  

## 📄 License  
This project is licensed under the **MIT** License.  

---

Let me know if you need any modifications or additional details! 😊
