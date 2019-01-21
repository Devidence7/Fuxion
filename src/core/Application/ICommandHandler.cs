﻿using Fuxion.Domain;
using System.Threading.Tasks;

namespace Fuxion.Application
{
	public interface ICommandHandler<TCommand> where TCommand : Command
	{
		Task HandleAsync(TCommand command);
	}
}
