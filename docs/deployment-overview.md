# Deployment Overview

```mermaid
flowchart LR
Dev[Developer]-->CI[Azure DevOps Pipeline]
CI-->ACR[Azure Container Registry]
CI-->Bicep[Bicep Deployment]
Bicep-->ACA[Azure Container Apps]
ACA-->LAW[Log Analytics]
```
