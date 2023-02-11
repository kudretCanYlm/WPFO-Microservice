﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using WPFO.Ordering.Domain.Common;
using WPFO.Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WPFO.OrderIng.Infrastructure.Persistence
{
	public class OrderContext:DbContext
	{
		public DbSet<Order> Orders { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries<EntityBase>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.Now;
						entry.Entity.CreatedBy = "swn";
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedDate = DateTime.Now;
						entry.Entity.LastModifiedBy = "swn";
						break;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}