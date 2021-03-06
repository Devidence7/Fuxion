﻿using Fuxion.Domain;
using Fuxion.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordinem.Calendar.Domain
{
	[TypeKey("Ordinem.Calendar.Application." + nameof(DeleteAppointmentCommand))]
	public class DeleteAppointmentCommand : Command
	{
		public Guid Id { get; set; } = Guid.NewGuid();
	}
}
