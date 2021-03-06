// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CoteleDunarii.Data.Migrations
{
    [DbContext(typeof(CoteleDunariiContext))]
    [Migration("20210821055058_updatedModel")]
    partial class updatedModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoteleDunarii.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Km")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WaterEstimationsId")
                        .HasColumnType("int");

                    b.Property<int?>("WaterInfoId")
                        .HasColumnType("int");

                    b.HasKey("CityId");

                    b.HasIndex("WaterEstimationsId");

                    b.HasIndex("WaterInfoId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("CoteleDunarii.Models.WaterEstimations", b =>
                {
                    b.Property<int>("WaterEstimationsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Next120h")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Next24h")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Next48h")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Next72h")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Next96h")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("WaterEstimationsId");

                    b.ToTable("WaterEstimations");
                });

            modelBuilder.Entity("CoteleDunarii.Models.WaterInfo", b =>
                {
                    b.Property<int>("WaterInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Elevation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReadTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Temperature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Variation")
                        .HasColumnType("int");

                    b.HasKey("WaterInfoId");

                    b.ToTable("WaterInfo");
                });

            modelBuilder.Entity("CoteleDunarii.Models.City", b =>
                {
                    b.HasOne("CoteleDunarii.Models.WaterEstimations", "waterEstimations")
                        .WithMany()
                        .HasForeignKey("WaterEstimationsId");

                    b.HasOne("CoteleDunarii.Models.WaterInfo", "waterInfo")
                        .WithMany()
                        .HasForeignKey("WaterInfoId");
                });
#pragma warning restore 612, 618
        }
    }
}
