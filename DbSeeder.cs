using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projekt.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        // Sprawdzamy, czy w bazie są już jakieś klasy lub uczniowie
        if (db.Classes.Any() || db.Students.Any())
        {
            return; // Baza już zawiera dane, nie dodajemy ponownie
        }

        // 1. Tworzenie Klas
        var classes = new List<SchoolClass>
        {
            new() { Name = "1A" },
            new() { Name = "1B" },
            new() { Name = "2A" },
            new() { Name = "2B" },
            new() { Name = "3A" }
        };

        //db.Classes.AddRange(classes);
        //db.SaveChanges(); // Zapisujemy, aby klasy otrzymały swoje ID z bazy

        // 2. Przygotowanie danych do generowania uczniów
        var firstNames = new[]
        {
            "Jan", "Anna", "Piotr", "Maria", "Krzysztof", "Katarzyna", "Michał", "Agnieszka",
            "Paweł", "Ewa", "Tomasz", "Zofia", "Jakub", "Julia", "Mateusz", "Maja",
            "Szymon", "Oliwia", "Filip", "Alicja", "Kacper", "Wiktoria", "Adam", "Natalia", "Maciej"
        };

        var lastNames = new[]
        {
            "Nowak", "Kowalski", "Wiśniewski", "Wójcik", "Kowalczyk", "Kamiński", "Lewandowski",
            "Zieliński", "Szymański", "Woźniak", "Dąbrowski", "Kozłowski", "Jankowski", "Mazur",
            "Wojciechowski", "Kwiatkowski", "Krawczyk", "Kaczmarek", "Piotrowski", "Grabowski",
            "Zając", "Pawłowski", "Michalski", "Król", "Wieczorek"
        };

        var random = new Random(12345); // Stałe ziarno (seed), aby dane były powtarzalne przy każdym czyszczeniu bazy
        var students = new List<Student>();

        // 3. Generowanie 25 uczniów i przypisywanie ich do klas
        for (int i = 0; i < 25; i++)
        {
            // Przypisujemy uczniów po kolei do dostępnych klas (po 5 uczniów na klasę)
            var assignedClass = classes[i % classes.Count];

            var student = new Student
            {
                Name = firstNames[random.Next(0, firstNames.Length)],
                Surname = lastNames[random.Next(0, lastNames.Length)],
                IsInClass = true            // Domyślnie wszyscy są w klasie
            };

            assignedClass.Students.Add(student);
            students.Add(student);
        }

        db.Classes.AddRange(classes);
        db.Students.AddRange(students);
        db.SaveChanges();
    }
}