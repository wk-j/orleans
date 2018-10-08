using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyInterface {

    public interface IHelloArchive : Orleans.IGrainWithIntegerKey {
        Task<string> SayHello(string greeting);

        Task<IEnumerable<string>> GetGreetings();
    }
}
