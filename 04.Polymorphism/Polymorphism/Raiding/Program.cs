
using Raiding.Core;
using Raiding.Core.Interfaces;
using Raiding.Factory;
using Raiding.Factory.Interfaces;
using Raiding.IO;
using Raiding.IO.Interfaces;

IReader reader = new Reader();
IWriter writer = new Writer();
IFactory factory = new Factory();

IEngine engine = new Engine(reader, writer, factory);

engine.Run();