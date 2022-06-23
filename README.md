# OTel collector routing based on deployment environment

This repo demonstrates how to route telemetry data to different "back-ends" based on the environment where services are running (e.g. `Production`, `Staging`, etc). 

This is possible by using the
[Routing processor](https://github.com/open-telemetry/opentelemetry-collector-contrib/tree/main/processor/routingprocessor)
on the OpenTelemetry Collector. The router processor is configured to use the
[deployment.environment](https://github.com/open-telemetry/opentelemetry-specification/blob/main/specification/resource/semantic_conventions/deployment_environment.md) Resource attribute. 

For services running in `Production` the router sends the spans to `Jaeger`. For services running in
`Staging`, the router sends the spans to the collector's logger exporter (sdtout).

## Running the sample

- Run `docker-compose up` at the root of the repo
- Make a call to the `Production` API: http://localhost:30010/helloworld
  - You should see a response with: `Hello from OTel: Production`
- Make a call to the `Staging` API: http://localhost:30020/helloworld
  - You should see a response with: `Hello from OTel: Staging`
- Verify that only spans for `bis.api.Production` are present in Jaeger http://localhost:16686
- Verify that only spans for `bis.api.Staging` are printed in the Collector's console output


## Requirements

- Docker and Compose
