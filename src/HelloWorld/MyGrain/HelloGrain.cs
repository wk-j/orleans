﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyInterface;

namespace MyGrain {

    public class HelloGrain : Orleans.Grain, IHello {
        private readonly ILogger logger;

        public HelloGrain(ILogger<HelloGrain> logger) {
            this.logger = logger;
        }

        Task<string> IHello.SayHello(string greeting) {
            logger.LogInformation($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"You said: '{greeting}', I say: Hello!");
        }
    }
}
