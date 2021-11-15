

# SETraining 🏃
A repository to help users navigate the confusing web of ever emerging technologies.
________
## Get started

### 1. Create your local .env
1. Create `.env` file in Project root directory.
2. Populate the file with the following line: `POSTGRES_PASSWORD=$secret`.
3. Change the `$secret` keyword above with our super secret GUID that is not in VCS.

### 2. Run database with docker 🐳:
1. Open terminal.
2. `cd` to the projects root directory.
3. Run `docker-compose up`.
4. Optional: `docker-compose up --detach` to continue using same terminal

### 3. Stop a database instance 😵:
1. Run `docker-compose down` in terminal
_____

## Troubleshooting
### The password for PostGreSQL is incorrect
Run the following commands:
- `docker compose down`
- `docker system prune`
- `docker volume prune

After that you should 
- Restart the containers with step `2. Run database with docker 🐳`

### User doesn't exist
- Fuck, ask Jens

## Resources 🔗:
- [Overleaf](https://www.overleaf.com/9249462866zsfhsbjmvxmg)
- [Trello](https://trello.com/invite/b/C1tRzypF/1aef96c54dce7720d977a2b082b4ba0e/bdsa-project)
- [Discord](https://discord.gg/vGYScYvGRj)
- [Github](https://github.com/MLFlexer/BDSAProject)
