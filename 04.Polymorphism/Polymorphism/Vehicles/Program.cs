
using Vehicles.Core;
using Vehicles.Core.Interfaces;
using Vehicles.Factory;
using Vehicles.Factory.Interfaces;
using Vehicles.IO;
using Vehicles.IO.Interfaces;

IReader reader = new Reader();
IWriter writer = new Writer();
IFactory factory = new Factory();

IEngine engine = new Engine(reader, writer, factory);

engine.Run();