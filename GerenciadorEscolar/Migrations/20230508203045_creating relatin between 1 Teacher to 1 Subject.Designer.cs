﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolManager.Data;

#nullable disable

namespace GerenciadorEscolar.Migrations
{
    [DbContext(typeof(SchoolManagerContext))]
    [Migration("20230508203045_creating relatin between 1 Teacher to 1 Subject")]
    partial class creatingrelatinbetween1Teacherto1Subject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GerenciadorEscolar.Models.ClassModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Section")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Shift")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("GerenciadorEscolar.Models.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ClassModelId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClassModelId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("GerenciadorEscolar.Models.SubjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("GerenciadorEscolar.Models.TeacherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("SubjectModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectModelId")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("GerenciadorEscolar.Models.StudentModel", b =>
                {
                    b.HasOne("GerenciadorEscolar.Models.ClassModel", "ClassModel")
                        .WithOne("StudentModel")
                        .HasForeignKey("GerenciadorEscolar.Models.StudentModel", "ClassModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassModel");
                });

            modelBuilder.Entity("GerenciadorEscolar.Models.TeacherModel", b =>
                {
                    b.HasOne("GerenciadorEscolar.Models.SubjectModel", "SubjectModel")
                        .WithOne("TeacherModel")
                        .HasForeignKey("GerenciadorEscolar.Models.TeacherModel", "SubjectModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubjectModel");
                });

            modelBuilder.Entity("GerenciadorEscolar.Models.ClassModel", b =>
                {
                    b.Navigation("StudentModel")
                        .IsRequired();
                });

            modelBuilder.Entity("GerenciadorEscolar.Models.SubjectModel", b =>
                {
                    b.Navigation("TeacherModel")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}