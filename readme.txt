Install-Package EntityFramework

-- Swagger -- Install-Package Swashbuckle


Para crear la BD
Enable-Migrations
Add-Migration InitialCreate
Update-Database

-- Subir a GIT --
git init
git remote add origin https://github.com/tuUsuario/WebApi2CRUD.git
git add .
git commit -m "Initial commit"

-- Crear repositorio en GitHub --
gh auth login
gh repo create WebApi2CRUD --public --source=. --remote=origin
gh repo clone tuUsuario/WebApi2CRUD
git init
git commit -m "Initial commit - Web API 2 CRUD"
git branch -M main      # Crea la rama main
git push -u origin main # Sube el commit inicial
