
# Azure VNet Integration Project

This project has web application which connects to MySql Database hosted Ubuntu server through VNet Integration. The main aim is to use VNet integration to connect with MySql Server database using Private Ip Address of the Virtual Machine hosting MySql Server.

- **Azure Services Used:**
  - Azure Virtual Machine
  - Azure Web App
  - Azure Virtual Network
  
- **Steps**:
   1. **Azure Virtual Machine**:
        - Create Virtual Machine under Resource Group `vnetintdemo-rg-eus-01`      
        - Under Basics tab details, enter Virtual machine name `vnetintdemo-vm-eus2-01` and choose `Ubuntu Server 24.04 LTS - x64 Gen2` for the Image.
        - Under Basics tab details, choose `Standard_B2s - 2 vcpus, 4 GiB memory` for size and select authentication type as password and then enter Username & password.
        - Leave the other field with default value and click `Review + Create` button.
        - Once validation passed then click `create`button.

   2. **Install MySql server in Virtual machine**:
        - Use commad prompt and login into VM using following commad `ssh [UserName]@[publicipaddress]`.
        - Install MySql server using command `sudo apt install mysql-server` and change the root user password.
        - Login into MySql server using root username and passowrd.
        - Create `appdb` and then create `Course` table and then insert sample records in the table.
        - Create new user `appuser` with password and grant permission to access the database.
        - Open MySql server configuration file using command `sudo nano /etc/mysql/mysql.conf.d/mysqld.cnf` and change the binding address to `vnetintdemo-vm-eus2-01` VM private IP address and then restart the MySql server.        

   3. **Azure Web App**:
        - Create Azure Web App under Resource Group `vnetintdemo-rg-eus-01`      
        - Under Basics tab details, enter web app name `vnetintdemo-web-eus2-01` and choose `.Net 8 (LTS)` for the Runtime Stack  .
        - Under Basics tab details, choose `Windows` for Operating System and select Region value as same as Virtual Mahine Region in order to perform VNet Integration.
        - Under Basics tab details, choose `Basic B1` app service plan which supports VNet Integration.
        - Leave the other field with default value  and click `Review + Create` button.
        - Once validation passed then click `create`button.

   4. **Sample MVC Application Development**:
        - Create MVC Application using `Visual studio 2019` and select `.Net 8` as version.
        - Add `Course` model class and Install `MySql.Data` Nuget package to perform operation with MySql Database.
        - Add required code in `HomeController` Index method to retrive the list of courses from MySql Database table `Course`.
        - Add simple razor html logic  in `Index.html` file to loop through list of courses and display in the table.
        - Add connection string in `appsettings.json` to connect with Mysql server.
        - Following is the connection string `Server=10.0.0.4;UserID=***;Password=***;Database=appdb`
        - Above connection string is using Private IP Address of the `vnetintdemo-vm-eus2-01` VM.
        - Create publish profile to connect with web app `vnetintdemo-web-eus2-01` and publish the application.
        - By launching web app url in browser will show the error due to connectivity issue with MySql server.

   5. **Azure Virtual Network**:
        - Goto Virtual Network `vnetintdemo-vm-eus2-01-vnet` resource created while creating Virtual Machine.
        - Goto `Settings-->Subnet`. Add new subnet named `websubnet` and by leaving the other fields with default value.

   6. **Azure Web App - VNet Integration**:
        - Goto Web app `vnetintdemo-web-eus2-01` resource.
        - Goto `Settings-->Networking`. Add VNet Integration by selecting Virtual Network as `vnetintdemo-vm-eus2-01-vnet` and Subnet as `websubnet`.

   7. **Test the Web App**:
        - Open new browser window and paste the web app url `https://vnetintdemo-web-eus2-01.azurewebsites.net/` and hit enter.
        - List of courses created in MySql Server will get displayed in the home page.
