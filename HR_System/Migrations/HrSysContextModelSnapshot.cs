﻿// <auto-generated />
using System;
using HR_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HR_System.Migrations
{
    [DbContext(typeof(HrSysContext))]
    partial class HrSysContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HR_System.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("admin_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("AdminName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("admin_name");

                    b.Property<string>("AdminPass")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("admin_pass");

                    b.HasKey("AdminId");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.AttDep", b =>
                {
                    b.Property<int>("AttId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("att_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttId"), 1L, 1);

                    b.Property<TimeSpan>("Attendance")
                        .HasColumnType("time")
                        .HasColumnName("attendance");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<TimeSpan>("Departure")
                        .HasColumnType("time")
                        .HasColumnName("departure");

                    b.Property<int>("EmpId")
                        .HasColumnType("int")
                        .HasColumnName("emp_id");

                    b.Property<float>("workedHours")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("real")
                        .HasComputedColumnSql("DatePart(HOUR ,[departure] ) + DatePart(MINUTE ,[departure])/60.0 -  DatePart(HOUR ,[attendance] ) + DatePart(MINUTE ,[attendance] )/60.0");

                    b.HasKey("AttId");

                    b.HasIndex("EmpId");

                    b.ToTable("Att_dep", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.Crud", b =>
                {
                    b.Property<int>("CrudId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("crud_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CrudId"), 1L, 1);

                    b.Property<bool>("Add")
                        .HasColumnType("bit")
                        .HasColumnName("add");

                    b.Property<bool>("Delete")
                        .HasColumnType("bit")
                        .HasColumnName("delete");

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<int>("PageId")
                        .HasColumnType("int")
                        .HasColumnName("page_id");

                    b.Property<bool>("Read")
                        .HasColumnType("bit")
                        .HasColumnName("read");

                    b.Property<bool>("Update")
                        .HasColumnType("bit")
                        .HasColumnName("update");

                    b.HasKey("CrudId");

                    b.HasIndex("GroupId");

                    b.HasIndex("PageId");

                    b.ToTable("CRUD", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("dept_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeptId"), 1L, 1);

                    b.Property<string>("DeptName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("dept_name");

                    b.HasKey("DeptId");

                    b.ToTable("Department", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.Employee", b =>
                {
                    b.Property<int>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("emp_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("address");

                    b.Property<TimeSpan>("AttTime")
                        .HasColumnType("time")
                        .HasColumnName("att_time");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("date")
                        .HasColumnName("birthdate");

                    b.Property<TimeSpan>("DepartureTime")
                        .HasColumnType("time")
                        .HasColumnName("departure_time");

                    b.Property<int?>("DeptId")
                        .HasColumnType("int")
                        .HasColumnName("dept_id");

                    b.Property<string>("EmpName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("emp_name");

                    b.Property<int>("FixedSalary")
                        .HasColumnType("int")
                        .HasColumnName("fixed_salary");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("gender");

                    b.Property<DateTime>("Hiredate")
                        .HasColumnType("date")
                        .HasColumnName("hiredate");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("national_id");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nationality");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("phone");

                    b.HasKey("EmpId");

                    b.HasIndex("DeptId");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"), 1L, 1);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("group_name");

                    b.HasKey("GroupId");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.Page", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("page_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PageId"), 1L, 1);

                    b.Property<string>("PageName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("page_name");

                    b.HasKey("PageId");

                    b.ToTable("Page", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.Setting", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("setting_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SettingId"), 1L, 1);

                    b.Property<string>("Dayoff1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("dayoff_1");

                    b.Property<string>("Dayoff2")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("dayoff_2");

                    b.Property<float>("MinusPerhour")
                        .HasColumnType("real")
                        .HasColumnName("minus_perhour");

                    b.Property<float>("PlusPerhour")
                        .HasColumnType("real")
                        .HasColumnName("plus_perhour");

                    b.HasKey("SettingId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("HR_System.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("email");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.HasIndex("GroupId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.Vacation", b =>
                {
                    b.Property<int>("VacId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("vac_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VacId"), 1L, 1);

                    b.Property<DateTime>("VacationDate")
                        .HasColumnType("date")
                        .HasColumnName("vacation_date");

                    b.Property<string>("VacationName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("vacation_name");

                    b.HasKey("VacId");

                    b.ToTable("Vacation", (string)null);
                });

            modelBuilder.Entity("HR_System.Models.AttDep", b =>
                {
                    b.HasOne("HR_System.Models.Employee", "Emp")
                        .WithMany("AttDeps")
                        .HasForeignKey("EmpId")
                        .IsRequired()
                        .HasConstraintName("FK_Att_dep_Employee");

                    b.Navigation("Emp");
                });

            modelBuilder.Entity("HR_System.Models.Crud", b =>
                {
                    b.HasOne("HR_System.Models.Group", "Group")
                        .WithMany("Cruds")
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK_CRUD_Group");

                    b.HasOne("HR_System.Models.Page", "Page")
                        .WithMany("Cruds")
                        .HasForeignKey("PageId")
                        .IsRequired()
                        .HasConstraintName("FK_CRUD_Page");

                    b.Navigation("Group");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("HR_System.Models.Employee", b =>
                {
                    b.HasOne("HR_System.Models.Department", "Dept")
                        .WithMany("Employees")
                        .HasForeignKey("DeptId")
                        .HasConstraintName("FK_Employee_Department");

                    b.Navigation("Dept");
                });

            modelBuilder.Entity("HR_System.Models.User", b =>
                {
                    b.HasOne("HR_System.Models.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_User_Group");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("HR_System.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HR_System.Models.Employee", b =>
                {
                    b.Navigation("AttDeps");
                });

            modelBuilder.Entity("HR_System.Models.Group", b =>
                {
                    b.Navigation("Cruds");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("HR_System.Models.Page", b =>
                {
                    b.Navigation("Cruds");
                });
#pragma warning restore 612, 618
        }
    }
}
