using LMS.API.Business_Layer;
using LMS.API.Data_Layer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookData, BookData>();
builder.Services.AddScoped<IBookDataExtractor, BookDataExtractor>();
builder.Services.AddScoped<IMemberData, MemberData>();
builder.Services.AddScoped<IMemberDataExtractor, MemberDataExtractor>();
builder.Services.AddScoped<ILoanData, LoanData>();
builder.Services.AddScoped<ILoanDataExtractor, LoanDataExtractor>();


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
