


##Run the system for demonstration purposes

The following command is only provided in order to show the system to examiners. It contains a key which should never be in any for of VCS.
1. Clone the directory.
2. Run the following command from `root`directory..
```markdown
./StartMe.ps1 -Docker -Azure EKA5Ko6vL6oAKFZvGFkPC3oQAJ9vqdCl+19Tudu9aKx5UXDs4eWHKPmF9Ob1h5kZ2XZ1qT6uMMyQsTbEgSkTTQ== 
```

-----

#### The following was used during development of the system. Disregard it for demonstration purposes.
# SETraining 🏃
[![.NET](https://github.com/MLFlexer/SETraining/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/MLFlexer/SETraining/actions/workflows/dotnet.yml)
---------

A repository to help users navigate the confusing web of ever emerging technologies.

________

## Get started 

#### Note:
- Step `1.` and `2.` are only run on first setup of the project

### 1. Create your local .env for docker-compose
1. Create `.env` file in Project root directory.
2. Populate the file with the following line: `POSTGRES_PASSWORD=$secret`.
3. Change the `$secret` keyword above with our super secret GUID that is not in VCS.

### 2. Set connections string in user secrets 
1. cd into `/Server` folder.
2. type `dotnet user-secrets set "ConnectionStrings:SETraining" "$connectionsstring"`
3. Where `$connectionsstring` is our secret connectionsstring not found in VCS.

### 3. Run database with docker 🐳:
1. Open terminal.
2. `cd` to the projects root directory ⚠️ IMPORTANT IN ORDER TO LOAD `.env` file into docker-compose ⚠️.
3. Run `docker compose up`.
4. Optional: `docker compose up --detach` to continue using same terminal

### 4. Stop a database instance 😵:
1. Run `docker compose down` in terminal or press trash button in Docker Desktop App
_____

## Troubleshooting

## Clear the Database and create it again empty (when doing migrations)
- Open query tool in PGAdmin
- Run `DROP SCHEMA public CASCADE;
CREATE SCHEMA public;`

### The password for PostGreSQL is incorrect
Run the following commands:
- `docker compose down`
- `docker system prune`
- `docker volume prune`

After that you should 
- Restart the containers with step `3. Run database with docker 🐳`


## Testing

Note: only looks at one project for the badge. TODO: need to aggregate test percentages.
Make sure to install Coverlet and XUnit in test projects with dotnet CLI
- `dotnet add package xunit --version 2.4.2-pre.12`
- `dotnet add package coverlet.msbuild --version 3.1.0`

A badge with code coverage is automatically built upon each push to main via `coverall.oi`

### How Do I get Aggregated test results? 
- `cd` to root directory and run the following command: 
- `dotnet test SETraining.sln --logger:trx --results-directory ../TestResults \
  "/p:CollectCoverage=true" \
  "/p:CoverletOutput=../TestResults/" \
  "/p:MergeWith=../TestResults/coverlet.json" \
  "/p:CoverletOutputFormat=\"json,cobertura\"" `

## Extra 
Find total line count in whole project from bash:
- cd to root directory 
- run command `find . -name '*.cs' | xargs wc -l`

## Resources 🔗:
- [Overleaf](https://www.overleaf.com/9249462866zsfhsbjmvxmg)
- [Trello](https://trello.com/invite/b/C1tRzypF/1aef96c54dce7720d977a2b082b4ba0e/bdsa-project)
- [Discord](https://discord.gg/vGYScYvGRj)
- [Github](https://github.com/MLFlexer/BDSAProject)
- [Figma](https://www.figma.com/file/JwxyhxTZZtYT2hkQBury1o/UI-BDSA)
