var builder = WebApplication.CreateBuilder(args);

//Register the http client with designated configuration
builder.Services.AddHttpClient("CurrencyConverter", client =>
{
    //replace the Key and host
    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "Your key");
    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "Your host");

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
