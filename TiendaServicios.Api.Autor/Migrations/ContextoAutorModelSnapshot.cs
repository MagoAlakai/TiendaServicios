﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TiendaServicios.Api.Autor.Persistency;

#nullable disable

namespace TiendaServicios.Api.Autor.Migrations
{
    [DbContext(typeof(ContextoAutor))]
    partial class ContextoAutorModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TiendaServicios.Api.Autor.Models.AutorLibro", b =>
                {
                    b.Property<int>("AutorLibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AutorLibroId"));

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<Guid>("AutorLibroGuid")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("AutorLibroId");

                    b.ToTable("AutorLibro");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Models.GradoAcademico", b =>
                {
                    b.Property<int>("GradoAcademicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GradoAcademicoId"));

                    b.Property<int?>("AutorLibroId")
                        .HasColumnType("integer");

                    b.Property<int?>("AutorLibroIdId")
                        .HasColumnType("integer");

                    b.Property<string>("CentroAcademico")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaGrado")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("GradoAcademicoGuid")
                        .HasColumnType("uuid");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("GradoAcademicoId");

                    b.HasIndex("AutorLibroId");

                    b.ToTable("GradoAcademico");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Models.GradoAcademico", b =>
                {
                    b.HasOne("TiendaServicios.Api.Autor.Models.AutorLibro", "AutorLibro")
                        .WithMany("ListaGradoAcademico")
                        .HasForeignKey("AutorLibroId");

                    b.Navigation("AutorLibro");
                });

            modelBuilder.Entity("TiendaServicios.Api.Autor.Models.AutorLibro", b =>
                {
                    b.Navigation("ListaGradoAcademico");
                });
#pragma warning restore 612, 618
        }
    }
}
