﻿using Fuxion.Application.Factories;
using Fuxion.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Fuxion.Application
{
	public abstract class Factory<TAggregate> where TAggregate : Aggregate, new()
	{
		public Factory(IServiceProvider serviceProvider) => Features = serviceProvider.GetServices<IFactoryFeature<TAggregate>>();

		internal IEnumerable<IFactoryFeature<TAggregate>> Features { get; }
		public TAggregate Create(Guid id)
		{
			var agg = new TAggregate
			{
				Id = id
			};
			foreach (var feature in Features)
				feature.Create(agg);
			return agg;
		}
	}
}