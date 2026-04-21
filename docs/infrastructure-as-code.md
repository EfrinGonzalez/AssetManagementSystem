# Infrastructure as Code

Bicep assets:
- `infra/main.bicep`
- `infra/main.parameters.json`

Deploy command:
`az deployment group create --resource-group <rg> --template-file infra/main.bicep --parameters @infra/main.parameters.json`
