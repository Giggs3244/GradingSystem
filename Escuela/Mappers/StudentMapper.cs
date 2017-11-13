using Escuela.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Escuela.Mappers
{
    public class StudentMapper : EntityTypeConfiguration<Student>
    {
        public StudentMapper() 
        {
            this.ToTable("Students");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.Email).IsRequired();
            this.Property(s => s.Email).HasMaxLength(255);
            this.Property(s => s.Email).IsUnicode(false);

            this.Property(s => s.FirstName).IsRequired();
            this.Property(s => s.FirstName).HasMaxLength(50);

            this.Property(s => s.LastName).IsRequired();
            this.Property(s => s.LastName).HasMaxLength(50);

        }
    }
}