using System;
using Registering;
using scyna;

namespace registering_test;

public class TestBase : IDisposable
{
    public TestBase()
    {
        Engine.Init("http://127.0.0.1:8081", "scyna_test", "123456");
        Endpoint.Register(Path.REGISTER_USER_URL, new RegisterUserHandler());
    }

    public void Dispose()
    {
        Engine.Release();
    }
}
