﻿using IsTableBusy.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsTableBusy.EntityFramework
{
    public class Context : DbContext
    {
        internal static string ConnectionString { get; set; }

        public Context() :  base(ConnectionString)
        {
            if (ConnectionString == null)
            {
                throw new NullReferenceException("ConnectionString should be set.");
            }
        }   

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Place> Places { get; set; }

        public DbSet<Table> Tables { get; set; }
    }
}
