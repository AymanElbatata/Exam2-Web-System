🧪 Online Exam System (Admin Panel & Exam Website)
A complete mini online examination system built using ASP.NET Core and Entity Framework Core. This project includes:

A secure Admin Panel for managing exams and users.

A responsive User Website for taking exams and viewing results.

Developed by Ayman Elbatata

🔧 Technologies Used
ASP.NET Core (C#/MVC)

Entity Framework Core (with Migrations)

ASP.NET Identity (for secure authentication)

JavaScript & AJAX

Bootstrap (for responsive UI)

SQL Server

🛠 Features
🔐 Admin Panel
Login using ASP.NET Identity.

Manage Exams:

Create, Edit, Delete Exams.

Manage Questions:

Add, Edit, Delete Questions with:

Question Title

Four Choices

One Correct Answer

Manually manage Users:

Add new users directly to the database (no self-registration).

🧑‍🎓 User Website
Login with pre-added credentials.

View and select available exams.

Take exams with multiple-choice questions.

Submit exams via AJAX (no page reload).

See real-time score and feedback:

Total Score in percentage

Number of Correct/Incorrect answers

Pass/Fail status (based on 60% threshold)

✅ Evaluation Logic
Each question = 1 point.

Score formula:

Score
=
(
Correct Answers
Total Questions
)
×
100
Score=( 
Total Questions
Correct Answers
​
 )×100
Pass if score ≥ 60%, otherwise Fail.

📁 Project Structure
pgsql
Copy
Edit
/Admin        --> Admin panel UI & logic
/User         --> Frontend for exam-taking
/Models       --> Database models
/Data         --> DbContext, Migrations
/Services     --> Business logic & Repository pattern
📌 Installation
Clone the repository:

bash
Copy
Edit
git clone https://github.com/AymanElbatata/Exam2-Web-System.git
Update your connection string in appsettings.json.

Apply database migrations:

bash
Copy
Edit
dotnet ef database update
Run the project:

bash
Copy
Edit
dotnet run
📊 Screenshots
(Add screenshots here for Admin Panel, Exam UI, and Score Page)

💼 About the Developer
Ayman Elbatata
LinkedIn
Software Developer | Web Solutions | ASP.NET Enthusiast
