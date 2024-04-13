// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


//
async Task Travel()
{
    await Wait();
}

async Task Wait()
{
    await Task.CompletedTask;
}

//EC (This is an  example of synchronous programming)
//1.Make Payment
//2. Deliver order