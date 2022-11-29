## Prerequsite
[Download .Net6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)<br/>
[Download VS Code](https://code.visualstudio.com/download)<br/>
[Download Docker for Desktop](https://www.docker.com/products/docker-desktop/)<br/>

Verify the .Net SDK version by running command <b>dotnet --version</b> in vs code terminal
![image](https://user-images.githubusercontent.com/18566830/204452374-9557417a-76c1-482d-adce-4be6bd108092.png)

Verify the Docker version by running command <b> docker -v</b> in vs code terminal
![image](https://user-images.githubusercontent.com/18566830/204451979-d1579c03-117c-434e-be12-50d5e5195971.png)

# User API Dev Guide
<b>Api schema</b><br>
1) <b>AccountDetailsResponse:</b><br>
{<br>
      id integer($int32) <br>
      accountHolderName	string
      nullable: true <br>
      email	string <br>
      nullable: true <br>
      accountNumber string
      nullable: true <br>
      accountType	string <br>
      nullable: true <br>
  }
  
2) <b>CreateAccountRequest:</b><br>
{<br>
    email*	string<br>
}

3) <b>CreateUserRequest:</b><br>
{<br>
    firstName*	string <br>
    middleName	string
    nullable: true <br>
    lastName*	string <br>
    email*	string <br>
    salary*	number($double) <br>
    expense* number($double) <br>
    gender*	string <br>
}

4) <b>UserDetailsResponse:</b><br>
{<br>
    id integer($int32)<br>
    name	string
    nullable: true <br>
    email	string
    nullable: true <br>
    salary	number($double) <br>
    expense	number($double) <br>
    gender	string <br>
    nullable: true <br>
}
## Building
<b>Follow below steps to run the api</b> <br>
1)Download mssql image. Go to folder <b>Zip Candidate User API Challenge C#</b> and run command <b>docker-compose up</b>
  ![image](https://user-images.githubusercontent.com/18566830/204466700-219a9e75-4cf3-4b32-9a1a-9e93e87414da.png)
2) Open new vs code terminal and go to folder <b>TestProject.ZipPay.Api</b> and run the command <b>dotnet run</b>
  ![image](https://user-images.githubusercontent.com/18566830/204467312-3a8d28d5-65fb-413b-b93b-a79503f8e611.png)
3) Open browser and go to url <b>https://localhost:7011/swagger/index.html</b>. It will open Swagger UI
   <img width="931" alt="image" src="https://user-images.githubusercontent.com/18566830/204467743-cc388a8b-3372-4e3d-8d4c-2fb9c6533dcf.png">

## Testing
<b>Follow the below steps to run unit test case</b> <br>
Go to any Test project folder. Ex: Open vs code terminal and go to folder <b>TestProject.ZipPay.Api.Tests</b> ,and run the command <b>dotnet test</b> 
![image](https://user-images.githubusercontent.com/18566830/204468488-af464b98-9ad0-47f7-9a83-cf37506c4073.png)
![image](https://user-images.githubusercontent.com/18566830/204469045-87197f4e-f6b9-434e-8939-91e77e0ac462.png)

<b>Follow below steps to test the User api</b> <br>
1)<b>Create user api test:</b> Open swagger ui in browser by navigating to url <b>https://localhost:7011/swagger/index.html</b>.<br> Create new user by clicking the <b>POST</b> button available in User section. After providing the details click on Execute button <br>
See below screenshot for reference
 ![image](https://user-images.githubusercontent.com/18566830/204472194-fd7a3743-ba6d-4c79-9606-3aabf9a817d6.png)
 
 2)<b>Get user by id api test:</b>Open swagger ui in browser by navigating to url <b>https://localhost:7011/swagger/index.html</b><br>
  Click on Try it out button of api end point <b>GET /v1/user/{id}</b>. After providing the user id click on Execute button <br>
  See below screenshots for reference
  ![image](https://user-images.githubusercontent.com/18566830/204474793-75d79012-415f-4145-8d2f-edec6217cdde.png)

  ![image](https://user-images.githubusercontent.com/18566830/204474565-be956b0b-d557-4aa0-a925-6ecfb288d6f9.png)

  3)<b>Get users api test:</b>Open swagger ui in browser by navigating to url <b>https://localhost:7011/swagger/index.html</b><br>
  Click on Try it out button of api end point <b>GET /v1/user/{pageNum}/{pageSize}</b>. After providing the page number and page size click on Execute button <br>
  See below screenshots for reference
  ![image](https://user-images.githubusercontent.com/18566830/204475829-756eab4b-93ef-4c3a-8632-26004d49cf5f.png)
  ![image](https://user-images.githubusercontent.com/18566830/204475968-68c0bf84-f6e1-4356-9cdc-6a6dcc18d39c.png)

<b>Follow below steps to test the Account api</b> <br>
1)<b>Create account api test:</b> Open swagger ui in browser by navigating to url <b>https://localhost:7011/swagger/index.html</b>.<br> Create new account by clicking the <b>POST /v1/account</b> button available in User section. After providing the details click on Execute button <br>
See below screenshot for reference
<img width="893" alt="image" src="https://user-images.githubusercontent.com/18566830/204498609-d24e41a9-b3d5-471c-b90a-44912872104d.png">

 2)<b>Get account by id api test:</b>Open swagger ui in browser by navigating to url <b>https://localhost:7011/swagger/index.html</b><br>
  Click on Try it out button of api end point <b>GET /v1/account/{id}</b>. After providing the account id click on Execute button <br>
  See below screenshots for reference
  ![image](https://user-images.githubusercontent.com/18566830/204499169-e162bd72-82d6-4732-bd9c-c28367a11739.png)
  ![image](https://user-images.githubusercontent.com/18566830/204500180-02c06469-8d04-4160-887d-325e7766cbaa.png)

3)<b>Get accounts api test:</b>Open swagger ui in browser by navigating to url <b>https://localhost:7011/swagger/index.html</b><br>
  Click on Try it out button of api end point <b>GET /v1/account/{pageNum}/{pageSize}</b>. After providing the page number and page size click on Execute button <br>
  See below screenshots for reference
  ![image](https://user-images.githubusercontent.com/18566830/204500776-6924959a-22be-42a3-8ae3-56e814e0d5ee.png)
  ![image](https://user-images.githubusercontent.com/18566830/204500948-22b319a2-714e-462f-8eff-bcf0ba370c1b.png)
