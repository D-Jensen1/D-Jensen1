using RestConsole;

HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5000/api/openapi") };
Awlt db = new(client);
var result = await db.CustomerGETAsync(
    null, 
    null, 
    null, 
    null, 
    null,
    20,
    null);

foreach (var customer in result.Value)
{
    Console.WriteLine(customer);
}