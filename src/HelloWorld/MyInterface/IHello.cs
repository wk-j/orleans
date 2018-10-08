using System.Threading.Tasks;

namespace MyInterface {
    public interface IHello : Orleans.IGrainWithIntegerKey {
        Task<string> SayHello(string greeting);
    }
}
