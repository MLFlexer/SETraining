

# SETraining 🏃
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
2. type `dotnet user-secrets set  "ConnectionStrings:Khan" "$connectionsstring" `
3. Where `$connectionsstring` is our secret connectionsstring not found in VCS.

### 3. Run database with docker 🐳:
1. Open terminal.
2. `cd` to the projects root directory ⚠️ IMPORTANT IN ORDER TO LOAD `.env` file into docker-compose ⚠️.
3. Run `docker-compose up`.
4. Optional: `docker-compose up --detach` to continue using same terminal

### 4. Stop a database instance 😵:
1. Run `docker-compose down` in terminal or press trash button in Docker Desktop App
_____

## Troubleshooting
### The password for PostGreSQL is incorrect
Run the following commands:
- `docker compose down`
- `docker system prune`
- `docker volume prune`

After that you should 
- Restart the containers with step `2. Run database with docker 🐳`


## Extra 
Find total line count in whole project from bash:
- cd to root directory 
- run command `find . -name '*.cs' | xargs wc -l`

## Resources 🔗:
- [Overleaf](https://www.overleaf.com/9249462866zsfhsbjmvxmg)
- [Trello](https://trello.com/invite/b/C1tRzypF/1aef96c54dce7720d977a2b082b4ba0e/bdsa-project)
- [Discord](https://discord.gg/vGYScYvGRj)
- [Github](https://github.com/MLFlexer/BDSAProject)
