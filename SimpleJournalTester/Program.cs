using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SimpleJournal;
using SimpleJournalTester;

//await EntitiesGenerator.GenerateStudents(100);
//return;

await using var db = new AppDbContext(true);
var result = await db.FindStudentByName("Ал");

//Console.WriteLine(JsonSerializer.Serialize(result));