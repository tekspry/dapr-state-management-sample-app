dapr run `
    --app-id product `
    --app-port 5016 `
    --dapr-http-port 3501 `
    --components-path ../dapr/components `
    dotnet run