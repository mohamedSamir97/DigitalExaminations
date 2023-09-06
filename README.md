# DigitalExaminations

DigitalExaminations is a web portal designed for managing and conducting digital exams in an educational setting. The application is built on a 3-tier architecture, emphasizing separation of concerns and modularity.

## Features

- **User Authentication**: Users can log in and out of the application.
- **User Management**: Admins can create and manage user accounts for teachers and students.
- **Group Management**: Users can be organized into groups for easier management.
- **Exam Creation**: Teachers can create and configure exams, specifying questions and answers.
- **Exam Management**: Teachers can manage exams, including editing and deleting.
- **Student Participation**: Students can participate in exams.
- **Result Viewing**: Students can view their results after completing an exam.
- **Result Reporting**: Teachers can access and review exam results.

## Technologies Used

- **.NET Core 6**: The application is built using the latest version of .NET Core.
- **Cloudscribe**: A package used for pagination and other useful utilities.
- **Repository Pattern**: Data access is implemented using the repository pattern for better separation of concerns.
- **Unit of Work**: Unit of work pattern is used for managing transactions.
- **DataTables and Bootstrap**: UI components like data tables and styling are implemented using DataTables and Bootstrap.

## Usage

To run this application locally, follow these steps:

1. Clone the repository:

   ```shell
   git clone https://github.com/mohamedSamir97/DigitalExaminations.git

2. Make DigitalExaminations.Web the default startup project

3. Run Database migration choose DigitalExaminations.DataAccess project 
 
   ```shell
   add-migration Intial
   update-database

4. Run project using Visual studio 2019 or 2022

5. create an admin account in the Users table using Sql server, Azure Studio or VS Sql  to login with 

6. navigate between nav links and explore the web app
