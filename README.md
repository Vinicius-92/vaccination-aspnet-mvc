## Software para controle de vacinação


#### Tecnologias utilizadas / Used Technologies
* .NET Core 3.1
* ASPNET MVC
* MySql
* Bootstrap
* Identity
* EntityFramework

Comandos para rodar a aplicação / Commands to run application:

```bash
    * Clonar repositório / Clone repository
    git clone https://git.gft.com/vsav/vaccination-mvc.git
    
    * Entrar na pasta / Move to directory
    cd vaccination-mvc

    * Baixar pacotes .NET / Download .NET packages
    dotnet restore

    * Adicionar schema do banco de dados / Add database schema
    * Criar usuário admin / Create admin user
    dotnet-ef migrations add CreatingDatabaseSchema

    * Enviar schema para banco de dados e popular o mesmo
    * Send schema to database and insert data
    dotnet-ef database update

    * Rodar aplicação / Run Application
    dotnet watch run

    * Acessar em / Access in
    https://localhost:5001/
```

## Admin user:

### **Login:** admin@admin.com

#### **Password:** Gftadmin#2021

UML:

![Screenshot](https://github.com/Vinicius-92/vaccination-aspnet-mvc/blob/main/Images/UML.jpg?raw=true)


PT-br:

Nesse projeto foi desenvolvido um sistema completo para controle de pessoas, vaccinas, pontos de vacinação e lotes de vacina.
Registros com relação no banco de dados não podem ser excluídos para manter a integridade dos mesmos, registros de vacinação não podem ser excluídos por definição do projeto.

EN-us:

In this project was developed a full application for control of people, vaccines, vaccination points and vaccination batches.
Records with relation in the database can't be deleted to mantain database integrity.
Records of vaccination can't be deleted by project desing.


![Screenshot](https://github.com/Vinicius-92/vaccination-aspnet-mvc/blob/main/Images/Index.jpg?raw=true)
![Screenshot](https://github.com/Vinicius-92/vaccination-aspnet-mvc/blob/main/Images/Person.jpg?raw=true)
![Screenshot](https://github.com/Vinicius-92/vaccination-aspnet-mvc/blob/main/Images/Record.jpg?raw=true)


