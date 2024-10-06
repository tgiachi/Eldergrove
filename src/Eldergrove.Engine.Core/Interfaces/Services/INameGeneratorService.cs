using Eldergrove.Engine.Core.Interfaces.Services.Base;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface INameGeneratorService : IEldergroveService
{
    void AddName(string type, string name);

    string GenerateName(string type);
}
