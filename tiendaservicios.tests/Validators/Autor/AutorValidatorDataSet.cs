namespace TiendaServicios.Api.Tests.Validators.Autor;

public static class AutorValidatorDataSet
{
    private static AutorModel GenerateAutor001()
    {
        AutorModel autor_model = new()
        {
            Nombre = "Mago",
            Apellido = "Txukember",
            AutorLibroId = 4,
            AutorLibroGuid = Guid.NewGuid().ToString(),
            GradoAcademicoGuid = Guid.NewGuid().ToString(),
            FechaNacimiento = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
        };

        return autor_model;
    }

    private static AddAutorRequest GenerateAddAutor001()
    {
        AutorModel? dataset_01 = GenerateAutor001();
        AddAutorRequest add_autor_request = new()
        {
            AutorModel = dataset_01
        };
        return add_autor_request;
    }
    private static UpdateAutorRequest GenerateUpdateAutorRequest001()
    {
        AutorModel autor_model = new()
        {
            Nombre = "Ma",
            Apellido = "Txukember",
            AutorLibroId = 4,
            AutorLibroGuid = Guid.NewGuid().ToString(),
            GradoAcademicoGuid = Guid.NewGuid().ToString(),
            FechaNacimiento = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow)
        };

        UpdateAutorRequest update_autor_request = new()
        {
            AutorLibroId = 4,
            AutorModel = autor_model,
        };

        return update_autor_request;
    }

    public static IEnumerable<object[]> GenerateAddAutor()
    {
        AddAutorRequest add_autor_request = GenerateAddAutor001();

        if (add_autor_request is not null)
        {
            yield return new object[]
            {
                "Added new Autor correctly",
                add_autor_request,
                true
            };
        }
    }

    public static IEnumerable<object[]> GenerateAddAutorWithEmptyAutorLibroGuidToFail()
    {
        AddAutorRequest add_autor_request = GenerateAddAutor001();
        add_autor_request.AutorModel.AutorLibroGuid = Guid.Empty.ToString();

        if (add_autor_request is not null)
        {
            yield return new object[]
            {
                "The AutorLibroGuid is missing",
                add_autor_request,
                1,
                false
            };
        }
    }

    public static IEnumerable<object[]> GenerateUpdateAutor()
    {
        UpdateAutorRequest? update_autor_request = GenerateUpdateAutorRequest001();

        if (update_autor_request is not null)
        {
            yield return new object[]
            {
                "Updated AutorLibro Name to Ma correctly",
                update_autor_request,
                true
            };
        }
    }

    public static IEnumerable<object[]> GenerateUpdateAutorWithEmptyAutorLibroGuidToFail()
    {
        UpdateAutorRequest update_autor_request = GenerateUpdateAutorRequest001();
        update_autor_request.AutorModel.AutorLibroGuid = Guid.Empty.ToString();

        if (update_autor_request is not null)
        {
            yield return new object[]
            {
                "The AutorLibroGuid is missing",
                update_autor_request,
                1,
                false
            };
        }
    }
}
