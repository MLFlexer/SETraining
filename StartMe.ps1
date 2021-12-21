[CmdletBinding()]
param (
    [switch]
    $Docker,
    [switch]
    $Azure,
    [string]
    $AzureKey
    
)

$project = "Server"

if($Docker) {
    
    Write-Host "Init cleanup"
    docker rm --force SETrainingDB
    docker rm --force SETrainingStorage
        
    Write-Host "Clearing user-secrets"
    dotnet user-secrets clear --project $project
    
    $password = New-Guid
    $port = "5433"
    Write-Host "Setting postgres user secret"
    $database = "SETrainingDB"
    
    dotnet user-secrets init --project $project
    $connectionString = "Host=localhost;Port=$port;Username=postgres;Password=$password;Database=$database"
    dotnet user-secrets set "ConnectionStrings:$database" "$connectionString" --project $project
    
    Write-Host "Starting postgres"
    docker run --name SETrainingDB -p 5433:5432 -e POSTGRES_PASSWORD=$password -d postgres 
    
    
    
}


if ($Azure) {

    docker run --name SETrainingStorage -d --restart unless-stopped -p 10000:10000 -p 10001:10001 -p 10002:10002 mcr.microsoft.com/azure-storage/azurite
    $AccountName = "setrainingupload"
    Write-Host "Configuring Azure Connection String"
    dotnet user-secrets set "ConnectionStrings:AzureBlob" "DefaultEndpointsProtocol=https;AccountName=$AccountName;AccountKey=$AzureKey;EndpointSuffix=core.windows.net" --project $project
}

Write-Host "Updating Database"
dotnet ef migrations remove --project $project
dotnet ef migrations add init --project $project
dotnet ef database update --project $project

Write-Host "Starting SETraining App"
dotnet run --project $project