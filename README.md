# Employee Management System

A modern ASP.NET Core MVC application for managing employees with dynamic custom properties. This system allows organizations to create flexible employee profiles with configurable property types including dates, strings, integers, and dropdown selections.

## 🚀 Features

### Core Functionality

- **Employee Management**: Create, view, and manage employee records with unique codes and names
- **Dynamic Properties**: Define custom properties (Date, String, Integer, Dropdown) for enhanced employee profiles
- **Flexible Validation**: Configure properties as required or optional based on organizational needs
- **Responsive Design**: Modern Bootstrap-based UI that works across all devices

### Property Types Supported

- **📅 Date**: Date picker input for birthdays, hire dates, etc.
- **📝 String**: Text input for names, addresses, phone numbers, etc.
- **🔢 Integer**: Numeric input for employee IDs, years of experience, etc.
- **📋 Dropdown**: Select lists with predefined options for departments, positions, etc.

### Advanced Features

- **Professional Dashboard**: Statistics overview and quick actions
- **Smart Validation**: Required field enforcement with user-friendly error messages
- **Duplicate Prevention**: Unique constraints on employee codes and names
- **Empty State Handling**: Graceful handling when no properties or employees exist
- **Performance Optimized**: Only stores property values that have data (no null storage)

## 🏗️ Architecture

### Clean Architecture Implementation

```
├── Models/               # Entity models and database context
├── ViewModels/          # Data transfer objects for views
├── Controllers/         # MVC controllers for handling requests
├── Services/           # Business logic layer
├── Repositories/       # Data access layer with Unit of Work pattern
├── Views/             # Razor views with Bootstrap styling
└── Migrations/        # Entity Framework database migrations
```

### Technology Stack

- **Framework**: ASP.NET Core 9.0 MVC
- **Database**: SQL Server with Entity Framework Core
- **Frontend**: Bootstrap 5, FontAwesome icons, Responsive design
- **Architecture**: Repository Pattern, Service Layer, Unit of Work
- **Mapping**: AutoMapper for object mapping
- **Validation**: Data Annotations, Custom validation logic

## 📋 Requirements

- .NET 9.0 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code
- Git for version control

## ⚡ Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/ah0048/employee_task.git
cd employee_task
```

### 2. Configure Database

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeManagementDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Run Database Migrations

```bash
cd employees_system/employees_system
dotnet ef database update
```

### 4. Build and Run

```bash
dotnet build
dotnet run
```

### 5. Access the Application

Open your browser and navigate to: `https://localhost:7037`

## 🎯 Usage Guide

### Creating Custom Properties

1. Navigate to **Properties** in the main menu
2. Click **Add Property**
3. Configure:
   - **Name**: Property display name
   - **Type**: Select from Date, String, Integer, or Dropdown
   - **Required**: Toggle if the property is mandatory
   - **Options**: For dropdown types, enter comma-separated values

### Adding Employees

1. Go to **Employees** in the main menu
2. Click **Add Employee**
3. Fill in:
   - **Employee Code**: Unique identifier
   - **Employee Name**: Full name
   - **Custom Properties**: Dynamic fields based on defined properties

### Managing Data

- **Dashboard**: View statistics and quick access to common actions
- **Employee List**: Browse all employees with their property values
- **Property List**: Manage all custom properties and their configurations

## 🗄️ Database Schema

### Core Tables

- **Employees**: Basic employee information (Id, Code, Name)
- **PropertyDefinitions**: Custom property metadata (Id, Name, Type, IsRequired)
- **PropertyOptions**: Dropdown option values (Id, PropertyDefinitionId, Value)
- **EmployeeProperties**: Employee-specific property values (Id, EmployeeId, PropertyDefinitionId, Value)

### Key Features

- **Optimized Storage**: Only properties with values are stored (no null records)
- **Referential Integrity**: Foreign key constraints ensure data consistency
- **Unique Constraints**: Prevent duplicate employee codes/names and property names
- **Flexible Schema**: Easy to extend with new property types

## 🔧 Configuration

### Service Registration (Program.cs)

```csharp
// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies()
           .UseSqlServer(connectionString));

// Repository Pattern
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IPropertyDefinitionRepo, PropertyDefinitionRepo>();
builder.Services.AddScoped<IEmployeePropertyRepo, EmployeePropertyRepo>();
builder.Services.AddScoped<IPropertyOptionRepo, PropertyOptionRepo>();

// Business Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));
```

## 🎨 UI Features

### Modern Design Elements

- **Card-based Layout**: Professional appearance with shadow effects
- **Icon Integration**: FontAwesome icons for visual clarity
- **Color-coded Types**: Different colors for property types and statuses
- **Responsive Tables**: Mobile-friendly data display
- **Smart Forms**: Dynamic field generation and validation

### User Experience

- **Intuitive Navigation**: Clear menu structure and breadcrumbs
- **Helpful Placeholders**: Contextual input hints
- **Error Handling**: User-friendly error messages and validation
- **Empty States**: Encouraging messages when no data exists
- **Success Feedback**: Confirmation messages for actions

## 🧪 Testing Scenarios

### Property Creation

- ✅ Create Date property (e.g., "Date of Birth")
- ✅ Create String property (e.g., "Phone Number")
- ✅ Create Integer property (e.g., "Years of Experience")
- ✅ Create Dropdown property (e.g., "Department" with options)
- ✅ Test required vs optional properties

### Employee Management

- ✅ Add employee with only basic information
- ✅ Add employee with all property types filled
- ✅ Add employee with partial property data
- ✅ Test validation for required properties
- ✅ Test duplicate prevention
