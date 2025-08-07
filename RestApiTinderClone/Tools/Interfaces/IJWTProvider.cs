
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Tools.Interfaces
{
    public interface IJWTProvider
    {
         string Generate(User user);
    }
}
