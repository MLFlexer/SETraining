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
    docker rm SETraining
    docker compose down
        
    Write-Host "Clearing user-secrets"
    dotnet user-secrets clear --project $project
    
    $password = New-Guid
    $port = "5433"
    Write-Host "Setting postgres user secret"
    $database = "SETraining"
    
    dotnet user-secrets init --project $project
    $connectionString = "Host=localhost;Port=$port;Username=postgres;Password=$password;Database=$database"
    dotnet user-secrets set "ConnectionStrings:$database" "$connectionString" --project $project
    
    Write-Host "Starting postgres"
    docker run --name SETraining -p 5433:5432 -e POSTGRES_PASSWORD=$password -d postgres 
    
    
    
}


if ($Azure) {

    docker compose up -d
    $AccountName = "setrainingupload"
    Write-Host "Configuring Azure Connection String"
    dotnet user-secrets set "ConnectionStrings:AzureBlob" "DefaultEndpointsProtocol=https;AccountName=$AccountName;AccountKey=$AzureKey;EndpointSuffix=core.windows.net" --project $project
}

Write-Host "Updating Database"
dotnet ef database update --project $project

Write-Host "Starting SETraining App"
dotnet run --project $project