# Creating new Dapr subscriber

Create sample project
```bash
mkdir dapr-subscriber
cd dapr-subscriber
dotnet new webapi  
dotnet add package Dapr.AspNetCore
mkdir components
```

## Add redis pub/sub component

Add components/pubsub.yaml

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: pubsub
spec:
  type: pubsub.redis
  version: v1
  metadata:
  - name: redisHost
    value: localhost:6379
  - name: redisPassword
    value: ""

```

## Configure Dapr

Configure Dapr in startup

Add support the requests with Content-Type `application/cloudevents+json` in `Startup.cs`

```csharp
app.UseCloudEvents();
```

Add Dapr subscribe handler in `Startup.cs`

```
endpoints.MapSubscribeHandler();
```

Remove https redirection

## Sample Controller
Add sample controller
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dapr_subscriber.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {

        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Topic("pubsub", "hello-world")]
        public ActionResult HelloWorld()
        {
            _logger.LogWarning("Hello world");
            return Ok();
        }
    }
}

```

# Run the app

Run the application 
```bash
dapr run --app-id sample --app-port 5000 --components-path ./components -- dotnet run
```

# Publish to Sample Controller

Publish a message to the application
```bash
dapr publish --pubsub pubsub --publish-app-id sample -t hello-world
```