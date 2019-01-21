﻿using System;
using System.Linq;
namespace Fuxion.Domain.Events
{
	public static class EventFeatureExtensions
	{
		public static void AddFeature<TFeature>(this Event me, Action<TFeature> initializeAction = null) where TFeature : IEventFeature, new()
		{
			if (me.HasFeature<TFeature>()) throw new EventFeatureAlreadyExistException($"Event already has '{typeof(TFeature).Name}' feature");
			var fea = Activator.CreateInstance<TFeature>();
			initializeAction?.Invoke(fea);
			me.Features.Add(fea);
		}
		public static bool HasFeature<TFeature>(this Event me) where TFeature : IEventFeature, new()
			=> me.Features.OfType<TFeature>().Any();
		public static TFeature GetFeature<TFeature>(this Event me) where TFeature : IEventFeature, new()
		{
			// TODO Cambiar por ?? cuando este en VS 2019
			var res = me.Features.OfType<TFeature>().SingleOrDefault();
			if (res == null)
				throw new EventFeatureNotFoundException($"'{typeof(TFeature).Name}' must be present in the event");
			return res;
		}
	}
}
