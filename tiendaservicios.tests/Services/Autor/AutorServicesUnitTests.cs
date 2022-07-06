namespace TiendaServicios.Tests.Services.Autor;

[TestClass]
public sealed class AutorServicesUnitTests : LogicUnitTestAbstraction
{
    private readonly JsonSerializerOptions _json_serializer_options;
    public AutorServicesUnitTests() => _json_serializer_options = new() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    private void ConsoleWriteObject(string prefix, object? obj)
    {
        string str_json = JsonSerializer.Serialize(obj, _json_serializer_options);
        Console.WriteLine($"{prefix}: {str_json}");
        _ = str_json;
        _ = prefix;
    }

    [TestMethod]
    [DynamicData(nameof(AutorServicesDataSet.GenerateAddAutor), typeof(AutorServicesDataSet), DynamicDataSourceType.Method)]
    public async void AddAutorTestFullDataSets(string dataset_name, AddAutorRequest request, bool expected_assert_result)
    {
        // AAA: (Arrange-Act-Assert)
        AddAutorCommand command = new(request, _ctx);
        Console.WriteLine($"Dataset Name: {dataset_name}");

        // Act
        CommandValueObject<AddAutorResponse> response = await command.RunCommandAsync();
        ConsoleWriteObject("Response", response);

        // Assert
        Assert.IsTrue(response.IsSuccessfull);
        Assert.AreEqual(response.IsSuccessfull, expected_assert_result);
    }
}
