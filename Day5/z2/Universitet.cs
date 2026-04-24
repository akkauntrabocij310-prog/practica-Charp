using System;
using System.Collections.Generic;
class Professor
{
    public string Name { get; set; }
    public Professor(string name) => Name = name;
}
class Student
{
    public string Name { get; set; }
    public Student(string name) => Name = name;
}
class Department
{
    public string Title { get; set; }
    public Department(string title) => Title = title;
}
class University
{
    public string Name { get; set; }
    private Department _department;
    private Student[] _students;
    public Professor Teacher { get; set; }
    public University(string name, string deptName, Student[] students)
    {
        Name = name;
        _department = new Department(deptName);
        _students = students;  
    }
    public void ShowStudents()
    {
        Console.WriteLine($"Университет: {Name} (Факультет: {_department.Title})");
        Console.WriteLine($"Преподаватель: {(Teacher != null ? Teacher.Name : "Не назначен")}");
        Console.WriteLine("Список студентов:");
        foreach (var student in _students)
        {
            Console.WriteLine($"- {student.Name}");
        }
        Console.WriteLine();
    }
}