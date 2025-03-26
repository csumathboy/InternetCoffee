# Coffee Machine API

## Overview
This API simulates an internet-connected coffee machine that allows users to brew coffee with specific conditions. It is built using .NET Core, Clean Architecture principles, and MediatR.

## Features
- Brew Coffee Endpoint: Calls to `/brew-coffee` return a message with the prepared time.
- Service Availability: Every fifth request returns `503 Service Unavailable`.
- April Fools' Day Special: Requests on April 1st return `418 I'm a teapot`.
- Caching Mechanism: Uses `IMemoryCache` to track request counts.


## Authors
csumathboy
