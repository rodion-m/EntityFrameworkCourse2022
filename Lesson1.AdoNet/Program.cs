// https://docs.microsoft.com/ru-ru/dotnet/standard/data/sqlite/

//ЕДИНСТВЕННЫЙ nuget пакет: Microsoft.Data.Sqlite

using System.Text;
using System.Data;
using Microsoft.Data.Sqlite;

Console.OutputEncoding = Encoding.UTF8;

var connectionString = "Data Source=hello.db"; //+ данные авторизации
using (IDbConnection connection = new SqliteConnection(connectionString))
{
    connection.Open();
    var id = 124;
    //CreateUsersTable(connection);
    //CreateUser(connection, id, "Александр");

    using IDbCommand command = connection.CreateCommand();
    command.CommandText =
        @$"
        SELECT *
        FROM users
        WHERE id = {id}
    ";
    
    using (IDataReader reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var name = reader.GetString(0);
            Console.WriteLine($"Hello, {name}!");
        }
    }
}

void CreateUser(IDbConnection connection, int id, string name)
{
    using var command = connection.CreateCommand();
    command.CommandText =
        @$"
            insert into users
            values ({id}, '{name}', 0)
        ";
    command.ExecuteNonQuery();
}

void CreateUsersTable(IDbConnection connection)
{
    using var command = connection.CreateCommand();
    command.CommandText =
        @"
        create table users
        (
            id int primary key, 
            name text,
            date_of_birth
        )
    ";
    command.ExecuteNonQuery();
}