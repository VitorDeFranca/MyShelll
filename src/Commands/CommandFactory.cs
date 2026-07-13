using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCrafters.Shell.src.Commands
{
    public static class CommandFactory
    {
        private static readonly Dictionary<string, ICommandHandler> _handlers = new();

        static CommandFactory()
        {
            RegisterBuiltIn(new EchoCommandHandler());
            RegisterBuiltIn(new TypeCommandHandler());
            RegisterBuiltIn(new PwdCommandHandler());
            RegisterBuiltIn(new CdCommandHandler());
            RegisterBuiltIn(new ExitCommandHandler());
        }

        public static void RegisterBuiltIn(ICommandHandler handler)
        {
            _handlers[handler.CommandName] = handler;
        }

        public static bool IsShellBuiltIn(string commandName)
        {
            return _handlers.ContainsKey(commandName);
        }

        public static ICommandHandler? GetHandler(string commandName)
        {
            if (_handlers.TryGetValue(commandName, out var handler))
                return handler;

            return new ExternalCommandHandler(commandName);
        }
    }
}
