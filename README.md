
# CRUD Operations Using OData and SQL Server

This repository demonstrates implementing CRUD operations using OData with a code-first approach and SQL Server as the database. The project is split into two phases for managing distinct entities.

## Repository URL

[GitHub Repository](https://github.com/moclananh/PRN231_OData_SQLServer)

## Phases Overview

### **PHASE 1: Book Management**
- Manage book entities with properties like Title, Author, and other metadata.
- Supports OData queries such as filtering, selecting fields, ordering, and pagination.

### **PHASE 2: Product Management**
- Manage products and their associated categories (one-to-many relationship).
- Supports OData queries for filtering and selecting specific fields.

## Setting Up the Project

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/moclananh/PRN231_OData_SQLServer.git
   cd PRN231_OData_SQLServer
   ```

2. **Set Up Multi-Project Structure**:
   - The project is structured into modules for Phase 1 and Phase 2.
   - Ensure all dependencies are properly configured for each phase.

3. **Run the Application**:
   ```bash
   dotnet run
   ```

## OData API Samples

### **PHASE 1: Book Management**
1. **Filter by Title**:
   ```http
   GET http://localhost:5001/odata/Books?$filter=Title eq 'Enterprise Games'
   ```

2. **Select Specific Fields**:
   ```http
   GET http://localhost:5001/odata/Books?$select=Title,Author
   ```

3. **Order by Name (Descending)**:
   ```http
   GET http://localhost:50246/odata/Presses?$orderby=Name desc
   ```

4. **Pagination**:
   ```http
   GET http://localhost:50246/odata/Presses?$top=2&$skip=1
   ```

### **PHASE 2: Product Management**
1. **Select Specific Fields**:
   ```http
   GET https://localhost:5001/odata/Products?$select=Id,Name
   ```

## Notes
- Ensure the OData package is installed and configured in the project.
- Modify `Startup.cs` to include OData routing and configurations for the entities in each phase.
- Test the endpoints with tools like Postman or cURL to validate functionality.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more details.
