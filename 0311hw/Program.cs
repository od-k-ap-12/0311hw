using Microsoft.AspNetCore.CookiePolicy;
using System.Text;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*app.MapGet("/", () => "Hello World!");*/

app.Run(async (context) =>
{

    /*    Создать собственный терминальный компонент, используя метод Run. 
        Определить класс Person, содержащий 5 свойств.Создать коллекцию объектов класса. 
        Вернуть коллекцию объектов в виду Json.*/
    var path = context.Request.Path.ToString();
    var response = context.Response;
    var request = context.Request;
    response.ContentType = "text/html; charset=utf-8";

    List<Person> people = new List<Person> 
    { 
        new Person("name1", "surname1", 19, "email@gmail.com"), 
        new Person("name2", "surname2", 20, "email2@gmail.com"),
        new Person("name3", "surname3", 30, "email3@gmail.com"),
        new Person("name4", "surname4", 14, "email4@gmail.com"),
        new Person("name5", "surname5", 16, "email5@gmail.com"),
    };

    switch (path.ToLower())
    {
        case "/":
            {
                await response.WriteAsJsonAsync(people);

                break;
            }
        case "/getquery":
            {
                /*    await response.WriteAsJsonAsync(people);*/

                /*    Получить из строки запроса 3 параметра и вернуть их пользователю встроенными в Html страницу.*/

                //getQuery?animal=cat&age=10&breed=siamese

                string animal = request.Query["animal"];
                string age = request.Query["age"];
                string breed = request.Query["breed"];

                var st = new StringBuilder("<table>");

                st.Append("<h1>Query Result</h1>");

                st.Append($"<tr><td>{animal}</td><td>{age}</td><td>{breed}</td></tr>");

                st.Append("</table>");

                await response.WriteAsync(st.ToString());

                break;
            }
        default:
            {
                /* Реализуйте конечную точку API, которая принимает строковый параметр и возвращает длину строки.
                 Запрос к точке выглядит следующим образом:

                 https://localhost:XXXX/api/length/Hello-World*/
                string checkPath = "^(/api/length/)(.)";
                if (Regex.IsMatch(path, checkPath))
                {
                    string checkLength = path.Split('/').Last();
                    await response.WriteAsync(checkLength + " Length: " + checkLength.Length.ToString());
                }

                break;
            }
    }

});

app.Run();

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public Person(string name, string surname, int age, string email)
    {
        Name = name;
        Surname = surname;
        Age = age;
        Email = email;
    }
    public override string ToString()
    {
        return Name + " " + Surname + " " + Age + " " + Email;
    }
}
