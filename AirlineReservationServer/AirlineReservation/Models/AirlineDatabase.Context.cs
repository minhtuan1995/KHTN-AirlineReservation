﻿

//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace AirlineReservation.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class AirlineDatabaseEntities : DbContext
{
    public AirlineDatabaseEntities()
        : base("name=AirlineDatabaseEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public DbSet<AirPlane> AirPlanes { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<Seat> Seats { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<AirPort> AirPorts { get; set; }

}

}

