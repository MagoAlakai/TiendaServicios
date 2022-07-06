namespace TiendaServicios.Api.Tests.Validators.Autor;

[TestClass]
public class AutorValidatorUnitTests
{
    private AutorModelValidator? _add_autor_model_validator;
    private AddAutorRequestValidator? _add_autor_request_validator;
    private UpdateAutorRequestValidator? _update_autor_request_validator;
    private readonly JsonSerializerOptions _json_serializer_options;

    public AutorValidatorUnitTests() => _json_serializer_options = new() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    [TestInitialize]
    public void TestInitialize()
    {
        _add_autor_model_validator = new();
        _add_autor_request_validator = new();
        _update_autor_request_validator = new();
    }
    private void ConsoleWriteObject(string prefix, object? obj)
    {
        string str_json = JsonSerializer.Serialize(obj, _json_serializer_options);
        Console.WriteLine($"{prefix}: {str_json}");
        _ = str_json;
        _ = prefix;
    }

    [TestMethod]
    [DynamicData(nameof(AutorValidatorDataSet.GenerateAddAutor), typeof(AutorValidatorDataSet), DynamicDataSourceType.Method)]
    public void AddAutorTestFullDataSets(string dataset_name, AddAutorRequest request, bool expected_assert_result)
    {
        // AAA: (Arrange-Act-Assert)
        Console.WriteLine($"Dataset Name: {dataset_name}");

        // Act
        TestValidationResult<AddAutorRequest>? result = _add_autor_request_validator?.TestValidate(request);

        ConsoleWriteObject("Result", result);

        // Assert
        Assert.IsTrue(result?.IsValid);
        Assert.AreEqual(expected_assert_result, result?.IsValid);
    }

    [TestMethod]
    [DynamicData(nameof(AutorValidatorDataSet.GenerateAddAutorWithEmptyAutorLibroGuidToFail), typeof(AutorValidatorDataSet), DynamicDataSourceType.Method)]
    public void AddAutorTestWithEmptyAutorLibroGuidToFail(string dataset_name, AddAutorRequest request, int error_count, bool expected_assert_result)
    {
        // AAA: (Arrange-Act-Assert)
        Console.WriteLine($"Dataset Name: {dataset_name}");

        // Act
        TestValidationResult<AddAutorRequest>? result = _add_autor_request_validator?.TestValidate(request);
        ArgumentNullException.ThrowIfNull(result, nameof(result));

        IEnumerable<ValidationFailure> autor_libro_guid_validation_failures = result.ShouldHaveValidationErrorFor(x => x.AutorModel.AutorLibroGuid);

        ConsoleWriteObject("Result", result);

        // Assert
        Assert.IsFalse(result?.IsValid);
        Assert.AreEqual(expected_assert_result, result?.IsValid);
        Assert.AreEqual(error_count, autor_libro_guid_validation_failures.Count());
    }

    [TestMethod]
    [DynamicData(nameof(AutorValidatorDataSet.GenerateUpdateAutor), typeof(AutorValidatorDataSet), DynamicDataSourceType.Method)]
    public void UpdateAutorTestFullDataSets(string dataset_name, UpdateAutorRequest request, bool expected_assert_result)
    {
        // AAA: (Arrange-Act-Assert)
        Console.WriteLine($"Dataset Name: {dataset_name}");

        // Act
        TestValidationResult<UpdateAutorRequest>? result = _update_autor_request_validator?.TestValidate(request);

        ConsoleWriteObject("Result", result);

        // Assert
        Assert.IsTrue(result?.IsValid);
        Assert.AreEqual(expected_assert_result, result?.IsValid);
    }

    [TestMethod]
    [DynamicData(nameof(AutorValidatorDataSet.GenerateUpdateAutorWithEmptyAutorLibroGuidToFail), typeof(AutorValidatorDataSet), DynamicDataSourceType.Method)]
    public void UpdateAutorTestWithEmptyAutorLibroGuidToFail(string dataset_name, UpdateAutorRequest request, int error_count, bool expected_assert_result)
    {
        // AAA: (Arrange-Act-Assert)
        Console.WriteLine($"Dataset Name: {dataset_name}");

        // Act
        TestValidationResult<UpdateAutorRequest>? result = _update_autor_request_validator?.TestValidate(request);
        ArgumentNullException.ThrowIfNull(result, nameof(result));

        IEnumerable<ValidationFailure> autor_libro_guid_validation_failures = result.ShouldHaveValidationErrorFor(x => x.AutorModel.AutorLibroGuid);

        ConsoleWriteObject("Result", result);

        // Assert
        Assert.IsFalse(result?.IsValid);
        Assert.AreEqual(expected_assert_result, result?.IsValid);
        Assert.AreEqual(error_count, autor_libro_guid_validation_failures.Count());
    }
}
