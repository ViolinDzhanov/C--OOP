using BorderControl.Core;
using BorderControl.Core.Interfaces;
using BorderControl.IO;
using BorderControl.IO.Interfaces;

IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter());

engine.Run();