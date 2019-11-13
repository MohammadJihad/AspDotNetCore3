using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;
using Test.Entities.Entities;

namespace Test.DataAccess.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.LoginName);
            builder.HasOne(a => a.Country).WithOne().HasForeignKey<Country>(a => a.Id);
        }
    }
}
