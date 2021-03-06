﻿using Fuxion.Application.Events;
using Fuxion.Domain.Events;
using Fuxion.Domain;
using Newtonsoft.Json.Linq;
using System;

namespace Fuxion.Application.Events
{
	public static class EventSourcingEventFeatureExtensions
	{
		public static bool HasEventSourcing(this Event me)
			=> me.HasFeature<EventSourcingEventFeature>();
		public static EventSourcingEventFeature EventSourcing(this Event me)
			=> me.GetFeature<EventSourcingEventFeature>();
		public static void AddEventSourcing(this Event me, int targetVersion, Guid correlationId, DateTime eventCommittedTimestamp, int classVersion)
			=> me.AddFeature<EventSourcingEventFeature>(esef =>
			{
				esef.TargetVersion = targetVersion;
				esef.CorrelationId = correlationId;
				esef.EventCommittedTimestamp = eventCommittedTimestamp;
				esef.ClassVersion = classVersion;
			});
		public static Event Replay(this Event me) => me.Transform<Event>(e => e.EventSourcing().IsReplay = true);
		public static EventSourcingPod ToEventSourcingPod(this Event me) => new EventSourcingPod(me);
	}
}
