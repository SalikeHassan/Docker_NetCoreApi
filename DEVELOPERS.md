## Prerequsite
[Download .Net6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)<br/>
[Download VS Code](https://code.visualstudio.com/download)<br/>
[Download Docker for Desktop](https://www.docker.com/products/docker-desktop/)<br/>

Verify the .Net SDK version by running command <b>dotnet --version</b> in vs code terminal
![image](https://user-images.githubusercontent.com/18566830/204452374-9557417a-76c1-482d-adce-4be6bd108092.png)

Verify the Docker version by running command <b> docker -v</b> in vs code terminal
![image](https://user-images.githubusercontent.com/18566830/204451979-d1579c03-117c-434e-be12-50d5e5195971.png)

# User API Dev Guide

## Building
Follow below steps to run the api <br>
1)Download mssql image. Go to folder <b>Zip Candidate User API Challenge C#</b> and run command <b>docker-compose up</b>
  ![image](https://user-images.githubusercontent.com/18566830/204466700-219a9e75-4cf3-4b32-9a1a-9e93e87414da.png)
2) Open new vs code terminal and go to folder <b>TestProject.ZipPay.Api</b> and run the command <b>dotnet run</b>
  ![image](https://user-images.githubusercontent.com/18566830/204467312-3a8d28d5-65fb-413b-b93b-a79503f8e611.png)
3) Open browser and go to url <b>https://localhost:7011/swagger/index.html</b>. It will open Swagger UI
   <img width="931" alt="image" src="https://user-images.githubusercontent.com/18566830/204467743-cc388a8b-3372-4e3d-8d4c-2fb9c6533dcf.png">

## Testing
Follow the below steps to run unit test case <br>
Go to any Test project folder. Ex: Open vs code terminal and go to folder <b>TestProject.ZipPay.Api.Tests</b> ,and run the command <b>dotnet test</b> 
![image](https://user-images.githubusercontent.com/18566830/204468488-af464b98-9ad0-47f7-9a83-cf37506c4073.png)
![image](https://user-images.githubusercontent.com/18566830/204469045-87197f4e-f6b9-434e-8939-91e77e0ac462.png)

Follow below steps to test the api <br>
1)<b>Create User API test:</b> Open swagger ui in browser by navigating to url <b>https://localhost:7011/swagger/index.html</b> Create new user by clicking the post button available in User section. After providing the details click on Execute button <br>
See below screenshot for reference
 ![image](https://user-images.githubusercontent.com/18566830/204472194-fd7a3743-ba6d-4c79-9606-3aabf9a817d6.png)
 
 2)



## Deploying

## Additional Information
